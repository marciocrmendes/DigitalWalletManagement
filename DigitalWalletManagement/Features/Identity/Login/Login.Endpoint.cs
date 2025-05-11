using DigitalWalletManagement.Commons.Options;
using DigitalWalletManagement.Entities;
using DigitalWalletManagement.Features.Identity.Providers;
using DigitalWalletManagement.Infraestructure.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DigitalWalletManagement.Features.Identity.Login
{
    public sealed class LoginEndpoint(UserRepository userRepository,
        SignInManager<User> signInManager,
        IOptions<JwtSettings> jwtSettings,
        TokenProvider tokenProvider) : Endpoint<LoginRequest, LoginResponse>
    {
        public override void Configure()
        {
            Post("/api/identity/login");
            AllowAnonymous();
            Description(x => x
                .WithTags("Identity")
                .WithName("Login")
                .Produces<LoginResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized));
            Validator<LoginValidator>();
        }

        public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindByEmailAsync(request.Email);            
            if (user == null)
            {
                ThrowError("Invalid email or password");
                return;
            }

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);            
            if (!signInResult.Succeeded)
            {
                ThrowError("Invalid email or password");
                return;
            }

            var token = await tokenProvider.GenerateJwtTokenAsync(user);
            var expiresAt = DateTime.UtcNow.AddMinutes(jwtSettings.Value.TokenExpirationInMinutes);

            await SendAsync(new LoginResponse
            {
                Token = token,
                ExpiresAt = expiresAt,
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email!
            }, cancellation: cancellationToken);
        }        
    }
}