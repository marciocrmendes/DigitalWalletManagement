using DigitalWalletManagement.Entities.Interfaces;

namespace DigitalWalletManagement.Entities
{
    public abstract class BaseEntity : IIdentifiableEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
