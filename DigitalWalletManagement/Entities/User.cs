using DigitalWalletManagement.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DigitalWalletManagement.Entities
{
    public class User : IdentityUser<Guid>, IIdentifiableEntity, IAuditableEntity
    {
        public User()
        {
        }

        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public virtual ICollection<Role> Roles { get; set; } = [];
        public virtual ICollection<Wallet> Wallets { get; set; } = [];
    }
}
