using DigitalWalletManagement.Entities;
using DigitalWalletManagement.Infraestructure.Context;

namespace DigitalWalletManagement.Infraestructure.Repositories
{
    public sealed class WalletRepository(AppDbContext context) : BaseRepository<Wallet>(context)
    {
    }
}
