namespace DigitalWalletManagement.Entities.Interfaces
{
    public interface IAuditableEntity
    {
        Guid CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        Guid? UpdatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }

        public void Update();
    }
}
