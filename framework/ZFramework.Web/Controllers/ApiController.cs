using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ZFramework.Application.Services;
using ZFramework.Application.Services.Dtos;
using ZFramework.Common.Filtering;

namespace ZFramework.Web.Controllers
{
    /// <summary>
    /// Controller which provides basic funcionality such as logging.
    /// </summary>
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        /// <summary>
        /// Represents a type used to perform logging.
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Creates an instance of this class with all needed dependencies.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging in this controller.</param>
        protected ApiController(ILogger logger)
        {
            Logger = logger ?? NullLogger.Instance;
        }
    }

    /// <summary>
    /// Controller which provides an implementation of <see cref="IApplicationService{TPrimaryKey, TCreationInputDto, TUpdateInputDto, TEntityReadingDto}"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TApplicationService"></typeparam>
    /// <typeparam name="TCreationInputDto"></typeparam>
    /// <typeparam name="TUpdateInputDto"></typeparam>
    /// <typeparam name="TEntityReadingDto"></typeparam>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController<TPrimaryKey, TApplicationService, TCreationInputDto, TUpdateInputDto, TEntityReadingDto> : ApiController
        where TApplicationService : IApplicationService<TPrimaryKey, TCreationInputDto, TUpdateInputDto, TEntityReadingDto>
        where TCreationInputDto : IEntityDto
        where TUpdateInputDto : IEntityDto<TPrimaryKey>
        where TEntityReadingDto : IEntityDto
    {
        /// <summary>
        /// Represents a type used to perform this controller's business logic such as repository interaction.
        /// </summary>
        protected readonly TApplicationService Service;

        public ApiController(TApplicationService service, ILogger logger)
            : base(logger)
        {
            Service = service;
        }

        /// <summary>
        /// Deletes an entity by its primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to delete.</param>
        [HttpDelete("{id}")]
        public virtual void DeleteById(TPrimaryKey id)
        {
            Service.DeleteById(id);
        }

        /// <summary>
        /// Gets all entities mapped to <see cref="TEntityReadingDto"/>.
        /// </summary>
        /// <param name="filteringOptions">Optional parameter to filter, sort and page the results.</param>
        /// <returns>The requested data, indicating paging-related info, if any.</returns>
        [HttpGet]
        public virtual IPagedEnumerable<TEntityReadingDto> GetAll([FromQuery] FilteringOptions filteringOptions)
        {
            return Service.GetAll(filteringOptions);
        }

        /// <summary>
        /// Gets an entity by its primary key mapped to <see cref="TEntityReadingDto"/>.
        /// </summary>
        /// <param name="id">Primary key of the requested entity.</param>
        /// <returns>An instance of <see cref="TEntityReadingDto"/> representing the requested entity or null if it does not exist.</returns>
        [HttpGet("{id}")]
        public virtual TEntityReadingDto GetById(TPrimaryKey id)
        {
            return Service.GetById(id);
        }

        /// <summary>
        /// Creates and inserts a new entity.
        /// </summary>
        /// <param name="creationDto">Representation of the entity to create.</param>
        /// <returns>An instance of <see cref="TEntityReadingDto"/> representing the newly created entity.</returns>
        [HttpPost]
        public virtual TEntityReadingDto Insert([FromBody] TCreationInputDto creationDto)
        {
            return Service.Insert(creationDto);
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="updateDto">Representation of the entity to update.</param>
        /// <returns>An instance of <see cref="TEntityReadingDto"/> representing the newly updated entity.</returns>
        [HttpPut]
        public virtual TEntityReadingDto Update([FromBody] TUpdateInputDto updateDto)
        {
            return Service.Update(updateDto);
        }
    }
}