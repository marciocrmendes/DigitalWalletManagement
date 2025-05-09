namespace DigitalWalletManagement.Entities
{
    public abstract class BaseEntity
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
