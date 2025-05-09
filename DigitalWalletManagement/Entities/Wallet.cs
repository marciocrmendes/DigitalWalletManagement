using DigitalWalletManagement.Commons;

namespace DigitalWalletManagement.Entities
{
    public class Wallet : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Balance { get; set; }
        public AvailableCurrencyEnum Currency { get; set; } = AvailableCurrencyEnum.BRL;

        public User User { get; set; } = null!;

        public List<WalletTransaction> Transactions { get; set; } = [];
    }
}
