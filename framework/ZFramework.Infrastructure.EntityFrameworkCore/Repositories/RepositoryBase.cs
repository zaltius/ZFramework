using Microsoft.EntityFrameworkCore;
using ZFramework.Common.Filtering;
using ZFramework.Domain.Entities;
using ZFramework.Domain.Entities.Auditing;
using ZFramework.Domain.Entities.Authentication;
using ZFramework.Domain.EntityFrameworkCore;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class to implement <see cref="IRepository{TEntity,TPrimaryKey}"/>.
    /// It implements some methods in most simple way.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    /// <typeparam name="TDbContext">DbContext where the entity is defined.</typeparam>
    public class RepositoryBase<TEntity, TPrimaryKey, TDbContext> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        /// <summary>
        /// Used to create custom queries agains the database using LINQ.
        /// </summary>
        protected virtual DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        /// <summary>
        /// Creates an instance of this class with all needed dependencies.
        /// </summary>
        /// <param name="dbContext">DbContext to interact with.</param>
        public RepositoryBase(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public virtual void DeleteById(TPrimaryKey id)
        {
            var entity = GetById(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        /// <inheritdoc/>
        public virtual IPagedEnumerable<TEntity> GetAll(IFilteringOptions? filteringOptions = null)
        {
            return DbSet.PageResult(filteringOptions);
        }

        /// <inheritdoc/>
        public virtual TEntity? GetById(TPrimaryKey id)
        {
            return DbSet.Find(id);
        }

        /// <inheritdoc/>
        public virtual TEntity Insert(TEntity entity)
        {
            var result = DbSet.Add(entity).Entity;

            return result;
        }

        /// <inheritdoc/>
        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);

            var result = DbSet.Update(entity).Entity;

            return result;
        }

        /// <summary>
        /// Checks if an entity is being tracked by the context and forces its track if it is not.
        /// </summary>
        /// <param name="entity">Entity to track.</param>
        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);

            if (entry != null)
            {
                return;
            }

            DbSet.Attach(entity);
        }

        protected void Delete(TEntity entity)
        {
            AttachIfNot(entity);

            DbSet.Remove(entity);
        }
    }
}