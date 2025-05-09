namespace DigitalWalletManagement.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        public List<Wallet> Wallets { get; set; } = [];
    }
}
