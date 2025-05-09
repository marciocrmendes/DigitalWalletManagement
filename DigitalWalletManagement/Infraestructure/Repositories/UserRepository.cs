using DigitalWalletManagement.Entities;
using DigitalWalletManagement.Infraestructure.Context;

namespace DigitalWalletManagement.Infraestructure.Repositories
{
    public sealed class UserRepository(AppDbContext context) : BaseRepository<User>(context)
    {
    }
}
