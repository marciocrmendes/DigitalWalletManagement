using DigitalWalletManagement.Commons.Enums;

namespace DigitalWalletManagement.Features.Wallets.AddTransaction
{
    public class AddTransactionRequest
    {
        public required Guid WalletId { get; set; }
        public required decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; } = TransactionTypeEnum.Deposit;
        public string? Description { get; set; }
    }
}