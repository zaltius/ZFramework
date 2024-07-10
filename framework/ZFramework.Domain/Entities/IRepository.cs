using ZFramework.Common.Filtering;
using ZFramework.Domain.Entities;

namespace ZFramework.Domain.EntityFrameworkCore
{
    public interface IRepository
    {
    }

    /// <summary>
    /// This interface is implemented by all repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository
        where TEntity : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Deletes an entity with given primary key.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void DeleteById(TPrimaryKey id);

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <param name="filteringOptions">Optional parameter to filter, sort and page the results.</param>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IPagedEnumerable<TEntity> GetAll(IFilteringOptions? filteringOptions = null);

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        TEntity? GetById(TPrimaryKey id);

        /// <summary>
        /// Creates and inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        TEntity Update(TEntity entity);
    }
}