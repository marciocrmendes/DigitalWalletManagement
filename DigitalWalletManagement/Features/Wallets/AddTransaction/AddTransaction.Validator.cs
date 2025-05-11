using FastEndpoints;
using FluentValidation;

namespace DigitalWalletManagement.Features.Wallets.AddTransaction
{
    public class AddTransactionValidator : Validator<AddTransactionRequest>
    {
        public AddTransactionValidator()
        {
            RuleFor(x => x.WalletId)
                .NotEmpty()
                .WithMessage("Wallet ID is required.");
            
            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("Amount is required.")
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.");
            
            RuleFor(x => x.TransactionType)
                .IsInEnum()
                .WithMessage("Invalid transaction type.");
            
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");
        }
    }
}