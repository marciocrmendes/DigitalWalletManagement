using DigitalWalletManagement.Commons.Enums;

namespace DigitalWalletManagement.Features.Wallets.CreateWallet
{
    public class CreateWalletRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal InitialBalance { get; set; } = 0;
        public AvailableCurrencyEnum Currency { get; set; } = AvailableCurrencyEnum.BRL;
    }
}