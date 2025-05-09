using DigitalWalletManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWalletManagement.Infraestructure.Mappers
{
    public sealed class WalletTransactionTypeConfiguration : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder.ToTable("WalletTransactions");

            builder.HasKey(wt => wt.Id);
            builder.Property(wt => wt.Id).ValueGeneratedOnAdd();

            builder.Property(wt => wt.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(wt => wt.TransactionType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(wt => wt.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(wt => wt.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(wt => wt.WalletId);
        }
    }
}
