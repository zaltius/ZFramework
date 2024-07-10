using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ZFramework.Application.Services.Dtos;
using ZFramework.Common;
using ZFramework.Common.Filtering;
using ZFramework.Domain.Entities;
using ZFramework.Domain.Entities.Auditing;
using ZFramework.Domain.EntityFrameworkCore;
using ZFramework.Domain.UnitOfWork;

namespace ZFramework.Application.Services
{
    /// <summary>
    /// Base application service. Every application service should inherit this simple class.
    /// </summary>
    public abstract class ApplicationService : IApplicationService
    {
        /// <summary>
        /// Represents a type used to perform logging.
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Represents a type used to perform mapping.
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        /// Represents a type to perform transactional actions.
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Creates an instance of this class with all needed dependencies.
        /// </summary>
        /// <param name="mapper">Represents a type used to perform mapping in this service.</param>
        /// <param name="unitOfWork">Represents a type to perform transactional actions in this service.</param>
        public ApplicationService(ILogger logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            Logger = logger ?? NullLogger.Instance;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
    }

    /// <summary>
    /// Service which provides an abstraction of <typeparamref name="TRepository"/>
    /// </summary>
    /// <typeparam name="TEntity">Represents an <see cref="IEntity{TPrimaryKey}"/> used for data representation.</typeparam>
    /// <typeparam name="TPrimaryKey">Represents the type of the primary key of <typeparamref name="TRepository"/>-related entities.</typeparam>
    /// <typeparam name="TRepository">Represents a repository which implements <see cref="IRepository{TEntity, TPrimaryKey}"/></typeparam>
    public abstract class ApplicationService<TEntity, TPrimaryKey, TRepository>
        : ApplicationService
        where TEntity : class, IEntity<TPrimaryKey>
        where TRepository : IRepository<TEntity, TPrimaryKey>
    {
        /// <summary>
        /// Represents a type to perform repository operations.
        /// </summary>
        protected readonly TRepository Repository;

        /// <summary>
        /// Creates an instance of this class with all needed dependencies.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging in this service.</param>
        /// <param name="mapper">Represents a type used to perform mapping in this service.</param>
        /// <param name="unitOfWork">Represents a type to perform transactional actions in this service.</param>
        /// <param name="repository">Represents a type to perform repository operations in this service.</param>
        protected ApplicationService(ILogger logger, IMapper mapper, IUnitOfWork unitOfWork, TRepository repository) : base(logger, mapper, unitOfWork)
        {
            Repository = repository;
        }
    }

    /// <summary>
    /// Service which provides an abstraction of <typeparamref name="TRepository"/>
    /// </summary>
    /// <typeparam name="TEntity">Represents an <see cref="IEntity{TPrimaryKey}"/> used for data representation.</typeparam>
    /// <typeparam name="TPrimaryKey">Represents the type of the primary key of <typeparamref name="TRepository"/>-related entities.</typeparam>
    /// <typeparam name="TRepository">Represents a repository which implements <see cref="IRepository{TEntity, TPrimaryKey}"/></typeparam>
    /// <typeparam name="TEntityReadingDto">Represents an <see cref="IEntityDto"/> used for read-only data representation.</typeparam>
    public abstract class ApplicationService<TEntity, TPrimaryKey, TRepository, TEntityReadingDto>
        : ApplicationService<TEntity, TPrimaryKey, TRepository>
        , IApplicationService<TPrimaryKey, TEntityReadingDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TRepository : IRepository<TEntity, TPrimaryKey>
        where TEntityReadingDto : class, IEntityDto
    {
        /// <summary>
        /// Creates an instance of this class with all needed dependencies.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging in this service.</param>
        /// <param name="mapper">Represents a type used to perform mapping in this service.</param>
        /// <param name="unitOfWork">Represents a type to perform transactional actions in this service.</param>
        /// <param name="repository">Represents a type to perform repository operations in this service.</param>
        protected ApplicationService(ILogger logger, IMapper mapper, IUnitOfWork unitOfWork, TRepository repository)
            : base(logger, mapper, unitOfWork, repository)
        {
        }

        /// <summary>
        /// Gets all entities mapped to <typeparamref name="TEntityReadingDto"/>.
        /// </summary>
        /// <param name="filteringOptions">Optional parameter to filter, sort and page the results.</param>
        /// <returns>The requested data, indicating paging-related info, if any.</returns>
        public virtual IPagedEnumerable<TEntityReadingDto> GetAll(IFilteringOptions? filteringOptions = null)
        {
            var result = Repository.GetAll(filteringOptions);

            return new PagedEnumerable<TEntityReadingDto>()
            {
                TotalCount = result.TotalCount,
                CurrentPage = result.CurrentPage,
                Items = result.Items.Select(Mapper.Map<TEntityReadingDto>).ToList()
            };
        }

        /// <summary>
        /// Gets an entity by its primary key mapped to <typeparamref name="TEntityReadingDto"/>.
        /// </summary>
        /// <param name="id">Primary key of the requested entity.</param>
        /// <returns>An instance of <typeparamref name="TEntityReadingDto"/> representing the requested entity or null if it does not exist.</returns>
        public virtual TEntityReadingDto GetById(TPrimaryKey id)
        {
            var result = Repository.GetById(id);

            return Mapper.Map<TEntityReadingDto>(result);
        }
    }

    /// <summary>
    /// Service which provides an abstraction of <typeparamref name="TRepository"/>
    /// </summary>
    /// <typeparam name="TEntity">Represents an <see cref="IEntity{TPrimaryKey}"/> used for data representation.</typeparam>
    /// <typeparam name="TPrimaryKey">Represents the type of the primary key of <typeparamref name="TRepository"/>-related entities.</typeparam>
    /// <typeparam name="TRepository">Represents a repository which implements <see cref="IRepository{TEntity, TPrimaryKey}"/></typeparam>
    /// <typeparam name="TCreationInputDto">Represents an <see cref="IEntityDto"/> used to create entities.</typeparam>
    /// <typeparam name="TEntityReadingDto">Represents an <see cref="IEntityDto"/> used for read-only data representation.</typeparam>
    public abstract class ApplicationService<TEntity, TPrimaryKey, TRepository, TCreationInputDto, TEntityReadingDto>
        : ApplicationService<TEntity, TPrimaryKey, TRepository, TEntityReadingDto>
        , IApplicationService<TPrimaryKey, TCreationInputDto, TEntityReadingDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TRepository : IRepository<TEntity, TPrimaryKey>
        where TCreationInputDto : class, IEntityDto
        where TEntityReadingDto : class, IEntityDto
    {
        /// <summary>
        /// Creates an instance of this class with all needed dependencies.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging in this service.</param>
        /// <param name="mapper">Represents a type used to perform mapping in this service.</param>
        /// <param name="unitOfWork">Represents a type to perform transactional actions in this service.</param>
        /// <param name="repository">Represents a type to perform repository operations in this service.</param>
        protected ApplicationService(ILogger logger, IMapper mapper, IUnitOfWork unitOfWork, TRepository repository)
            : base(logger, mapper, unitOfWork, repository)
        {
        }

        /// <summary>
        /// Creates and inserts a new entity.
        /// </summary>
        /// <param name="entityDto">Representation of the entity to create.</param>
        /// <returns>An instance of <typeparamref name="TEntityReadingDto"/> representing the newly created entity.</returns>
        public virtual TEntityReadingDto Insert(TCreationInputDto entityDto)
        {
            var entity = Mapper.Map<TEntity>(entityDto);

            entity = Repository.Insert(entity);

            UnitOfWork.Commit();

            return Mapper.Map<TEntityReadingDto>(entity);
        }
    }

    /// <summary>
    /// Service which provides an abstraction of <typeparamref name="TRepository"/>
    /// </summary>
    /// <typeparam name="TEntity">Represents an <see cref="IEntity{TPrimaryKey}"/> used for data representation.</typeparam>
    /// <typeparam name="TPrimaryKey">Represents the type of the primary key of <typeparamref name="TRepository"/>-related entities.</typeparam>
    /// <typeparam name="TRepository">Represents a repository which implements <see cref="IRepository{TEntity, TPrimaryKey}"/></typeparam>
    /// <typeparam name="TCreationInputDto">Represents an <see cref="IEntityDto"/> used to create entities.</typeparam>
    /// <typeparam name="TUpdateInputDto">Represents an <see cref="IEntityDto"/> used to update entities.</typeparam>
    /// <typeparam name="TEntityReadingDto">Represents an <see cref="IEntityDto"/> used for read-only data representation.</typeparam>
    public abstract class ApplicationService<TEntity, TPrimaryKey, TRepository, TCreationInputDto, TUpdateInputDto, TEntityReadingDto>
        : ApplicationService<TEntity, TPrimaryKey, TRepository, TCreationInputDto, TEntityReadingDto>
        , IApplicationService<TPrimaryKey, TCreationInputDto, TUpdateInputDto, TEntityReadingDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TRepository : IRepository<TEntity, TPrimaryKey>
        where TCreationInputDto : class, IEntityDto
        where TUpdateInputDto : class, IEntityDto<TPrimaryKey>
        where TEntityReadingDto : class, IEntityDto
    {
        /// <summary>
        /// Creates an instance of this class with all needed dependencies.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging in this service.</param>
        /// <param name="mapper">Represents a type used to perform mapping in this service.</param>
        /// <param name="unitOfWork">Represents a type to perform transactional actions in this service.</param>
        /// <param name="repository">Represents a type to perform repository operations in this service.</param>
        protected ApplicationService(ILogger logger, IMapper mapper, IUnitOfWork unitOfWork, TRepository repository)
            : base(logger, mapper, unitOfWork, repository)
        {
        }

        /// <summary>
        /// Deletes an entity by its primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to delete.</param>
        public virtual void DeleteById(TPrimaryKey id)
        {
            Repository.DeleteById(id);

            UnitOfWork.Commit();
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entityDto">Representation of the entity to update.</param>
        /// <returns>An instance of <typeparamref name="TEntityReadingDto"/> representing the newly updated entity.</returns>
        public virtual TEntityReadingDto Update(TUpdateInputDto updateDto)
        {
            var entity = Repository.GetById(updateDto.Id);

            Check.NotNullEntity(entity, updateDto.Id);

            if (entity is IModificationAudited auditedEntity &&
                updateDto is IModificationAudited auditedEntityDto)
            {
                auditedEntity.CheckConcurrencyUpdate(auditedEntityDto);
            }

            Mapper.Map(updateDto, entity);

            Repository.Update(entity!);

            UnitOfWork.Commit();

            entity = Repository.GetById(entity!.Id);

            return Mapper.Map<TEntityReadingDto>(entity);
        }
    }
}