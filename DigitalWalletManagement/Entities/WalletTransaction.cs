using DigitalWalletManagement.Commons;

namespace DigitalWalletManagement.Entities
{
    public class WalletTransaction : BaseEntity
    {
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public AvailableCurrencyEnum Currency { get; set; } = AvailableCurrencyEnum.BRL;
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public TransactionTypeEnum TransactionType { get; set; } = TransactionTypeEnum.Deposit;

        public Wallet Wallet { get; set; } = null!;

        public void UpdateTransaction(decimal newAmount, TransactionTypeEnum newTransactionType)
        {
            Amount = newAmount;
            TransactionType = newTransactionType;
            Update();
        }
    }
}
