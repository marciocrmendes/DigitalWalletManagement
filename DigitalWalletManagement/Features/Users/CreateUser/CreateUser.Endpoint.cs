using FastEndpoints;

namespace DigitalWalletManagement.Features.Users.CreateUser
{
    public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
    {
        public override void Configure()
        {
            Post("/api/user/create");
            AllowAnonymous();
            Description(x => x
                .WithName("Create User")
                .Produces<CreateUserResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError));
            Validator<CreateUserValidator>();
        }

        public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {


            await SendAsync(new()
            {
                Name = request.Name,
                Email = request.Email,
            }, cancellation: cancellationToken);
        }
    }
}
