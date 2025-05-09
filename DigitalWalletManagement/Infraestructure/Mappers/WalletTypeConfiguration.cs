using DigitalWalletManagement.Commons.Enums;
using DigitalWalletManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalWalletManagement.Infraestructure.Mappers
{
    public sealed class WalletTypeConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets");

            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedOnAdd();

            builder.Property(w => w.Balance)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(w => w.Currency)
                .IsRequired()
                .HasMaxLength(3)
                .HasConversion(new EnumToStringConverter<AvailableCurrencyEnum>());

            builder.Property(w => w.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasMany(w => w.Transactions)
                .WithOne(t => t.Wallet)
                .HasForeignKey(t => t.WalletId);
        }
    }
}
