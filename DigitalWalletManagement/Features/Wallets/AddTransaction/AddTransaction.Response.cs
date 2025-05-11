using DigitalWalletManagement.Commons.Enums;

namespace DigitalWalletManagement.Features.Wallets.AddTransaction
{
    public class AddTransactionResponse
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public decimal NewBalance { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}