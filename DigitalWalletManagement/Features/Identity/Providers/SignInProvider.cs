using DigitalWalletManagement.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DigitalWalletManagement.Features.Identity.Providers
{
    public sealed class SignInProvider(UserManager<User> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<User> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<User>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<User> confirmation) : SignInManager<User>(userManager, 
            contextAccessor, 
            claimsFactory, 
            optionsAccessor, 
            logger, 
            schemes, 
            confirmation)
    {
    }
}
