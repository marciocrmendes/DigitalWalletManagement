using FastEndpoints;
using FluentValidation;

namespace DigitalWalletManagement.Features.Wallets.CreateWallet
{
    public class CreateWalletValidator : Validator<CreateWalletRequest>
    {
        public CreateWalletValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Wallet name is required.")
                .MaximumLength(100)
                .WithMessage("Wallet name cannot exceed 100 characters.");
            
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");
            
            RuleFor(x => x.InitialBalance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Initial balance cannot be negative.");
        }
    }
}