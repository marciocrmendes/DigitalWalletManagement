namespace DigitalWalletManagement.Features.Wallets.CreateWallet
{
    public class CreateWalletResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}