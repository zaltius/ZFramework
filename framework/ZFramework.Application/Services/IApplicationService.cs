using ZFramework.Application.Services.Dtos;
using ZFramework.Common.Filtering;

namespace ZFramework.Application.Services
{
    /// <summary>
    /// Contract that represents an application service. Every application service should implement this simple interface.
    /// </summary>
    public interface IApplicationService
    {
    }

    /// <summary>
    /// Contract that represents a read-only application service.
    /// </summary>
    /// <typeparam name="TEntityReadingDto">Represents an <see cref="IEntityDto"/> used for read-only data representation.</typeparam>
    public interface IApplicationService<TEntityReadingDto> : IApplicationService  where TEntityReadingDto : IEntityDto
    {
        /// <summary>
        /// Gets all entities mapped to <typeparamref name="TEntityReadingDto"/>.
        /// </summary>
        /// <param name="filteringOptions">Optional parameter to filter, sort and page the results.</param>
        /// <returns>The requested data, indicating paging-related info, if any.</returns>
        IPagedEnumerable<TEntityReadingDto> GetAll(IFilteringOptions? filteringOptions = null);
    }

    /// <summary>
    /// Contract that represents a read-only application service with Primary Key.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Represents the type of the primary key of the entities related to this service.</typeparam>
    /// <typeparam name="TEntityReadingDto">Represents an <see cref="IEntityDto"/> used for read-only data representation.</typeparam>
    public interface IApplicationService<in TPrimaryKey, TEntityReadingDto>
        : IApplicationService<TEntityReadingDto>
        where TEntityReadingDto : IEntityDto
    {
        /// <summary>
        /// Gets an entity by its primary key mapped to <typeparamref name="TEntityReadingDto"/>.
        /// </summary>
        /// <param name="id">Primary key of the requested entity.</param>
        /// <returns>An instance of <typeparamref name="TEntityReadingDto"/> representing the requested entity or null if it does not exist.</returns>
        TEntityReadingDto GetById(TPrimaryKey id);
    }

    /// <summary>
    /// Contract that represents a creation-only application service.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Represents the type of the primary key of the entities related to this service.</typeparam>
    /// <typeparam name="TCreationInputDto">Represents an <see cref="IEntityDto"/> used to create entities.</typeparam>
    /// <typeparam name="TEntityReadingDto">Represents an <see cref="IEntityDto"/> used for read-only data representation.</typeparam>
    public interface IApplicationService<TPrimaryKey, in TCreationInputDto, TEntityReadingDto>
        : IApplicationService<TPrimaryKey, TEntityReadingDto>
        where TCreationInputDto : IEntityDto
        where TEntityReadingDto : IEntityDto
    {
        /// <summary>
        /// Creates and inserts a new entity.
        /// </summary>
        /// <param name="creationDto">Representation of the entity to create.</param>
        /// <returns>An instance of <typeparamref name="TEntityReadingDto"/> representing the newly created entity.</returns>
        TEntityReadingDto Insert(TCreationInputDto creationDto);
    }

    /// <summary>
    /// Contract that represents a CRUD application service.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Represents the type of the primary key of the entities related to this service.</typeparam>
    /// <typeparam name="TCreationInputDto">Represents an <see cref="IEntityDto"/> used to create entities.</typeparam>
    /// <typeparam name="TUpdateInputDto">Represents an <see cref="IEntityDto"/> used to update entities.</typeparam>
    /// <typeparam name="TEntityReadingDto">Represents an <see cref="IEntityDto"/> used for read-only data representation.</typeparam>
    public interface IApplicationService<TPrimaryKey, in TCreationInputDto, in TUpdateInputDto, TEntityReadingDto>
        : IApplicationService<TPrimaryKey, TCreationInputDto, TEntityReadingDto>
        where TCreationInputDto : IEntityDto
        where TUpdateInputDto : IEntityDto<TPrimaryKey>
        where TEntityReadingDto : IEntityDto
    {
        /// <summary>
        /// Deletes an entity by its primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to delete.</param>
        void DeleteById(TPrimaryKey id);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="updateDto">Representation of the entity to update.</param>
        /// <returns>An instance of <typeparamref name="TEntityReadingDto"/> representing the newly updated entity.</returns>
        TEntityReadingDto Update(TUpdateInputDto updateDto);
    }
}