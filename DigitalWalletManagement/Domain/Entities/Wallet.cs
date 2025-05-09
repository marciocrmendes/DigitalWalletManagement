using DigitalWalletManagement.Commons;

namespace DigitalWalletManagement.Domain.Entities
{
    public class Wallet : Entity
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
