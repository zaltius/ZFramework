using Microsoft.EntityFrameworkCore;
using ZFramework.Domain.UnitOfWork;

namespace ZFramework.Infrastructure.EntityFrameworkCore.UnitOfWork
{
    /// <summary>
    /// Unit of Work for Entity Framework Core.
    /// </summary>
    /// <typeparam name="TDbContext">DbContext enveloped by this Unit of Work.</typeparam>
    public class EfCoreUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        /// <summary>
        /// Context interact with.
        /// </summary>
        protected readonly TDbContext DbContext;

        /// <summary>
        /// Creates an instance of this class with all needed dependencies.
        /// </summary>
        /// <param name="dbContext">Context interact with.</param>
        public EfCoreUnitOfWork(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Commits all pending changes to the database.
        /// </summary>
        public virtual void Commit()
        {
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Asynchronously commits all pending changes to the database.
        /// </summary>
        public virtual async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Releases the resources used by the Unit of Work.
        /// </summary>
        public virtual void Dispose()
        {
            DbContext.Dispose();
        }

        /// <summary>
        /// Asynchronously releases the resources used by the Unit of Work.
        /// </summary>
        public virtual async ValueTask DisposeAsync()
        {
            await DbContext.DisposeAsync();
        }

        /// <summary>
        /// Rollbacks all pending changes from the database.
        /// Should be called when changes must be rolled back manually. After an exception during a <see cref="Commit"/>, all changes
        /// will be cancelled automatically because the saving process will not end.
        /// </summary>
        public virtual void Rollback()
        {
            DbContext.ChangeTracker.Entries().Where(x => x.State != EntityState.Added).ToList().ForEach(x => x.Reload());
            DbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList().ForEach(x => x.State = EntityState.Detached);
        }
    }
}