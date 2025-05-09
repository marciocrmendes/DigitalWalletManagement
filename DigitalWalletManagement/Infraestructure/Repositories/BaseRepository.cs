using DigitalWalletManagement.Entities;
using DigitalWalletManagement.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletManagement.Infraestructure.Repositories
{
    public abstract class BaseRepository<TEntity>(AppDbContext context) where TEntity : BaseEntity
    {
        public DbSet<TEntity> Repository => context.Set<TEntity>();

        public IQueryable<TEntity> GetAll()
        {
            return Repository.AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Repository
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Repository.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Repository.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Repository.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
