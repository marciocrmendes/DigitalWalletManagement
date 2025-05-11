using DigitalWalletManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace DigitalWalletManagement.Infraestructure.Repositories
{
    public sealed class RoleRepository(IRoleStore<Role> store,
        IEnumerable<IRoleValidator<Role>> roleValidators, 
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, 
        ILogger<RoleManager<Role>> logger) : 
        RoleManager<Role>(store, roleValidators, keyNormalizer, errors, logger)
    {
    }
}
