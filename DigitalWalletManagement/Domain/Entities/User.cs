namespace DigitalWalletManagement.Domain.Entities
{
    public class User : Entity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        public List<Wallet> Wallets { get; set; } = [];
    }
}
