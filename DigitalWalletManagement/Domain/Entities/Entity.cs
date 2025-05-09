namespace DigitalWalletManagement.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
