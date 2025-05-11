using Microsoft.AspNetCore.Identity;

namespace DigitalWalletManagement.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<User> Users { get; set; } = [];
    }
}
