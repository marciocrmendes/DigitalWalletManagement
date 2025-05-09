using FastEndpoints;
using FluentValidation;

namespace DigitalWalletManagement.Features.Users.CreateUser
{
    public class CreateUserValidator : Validator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email address is required.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(12)
                .WithMessage("Password must be at least 6 characters long.");
        }
    }
}
