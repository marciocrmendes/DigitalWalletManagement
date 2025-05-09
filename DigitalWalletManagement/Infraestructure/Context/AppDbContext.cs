using DigitalWalletManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletManagement.Infraestructure.Context
{
    public sealed class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Wallet> Wallets { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<WalletTransaction> WalletTransactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(AppAssembly.Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
