using Microsoft.AspNetCore.Mvc;
using ZFramework.Common.Filtering;
using ZFramework.Web.Controllers;
using ZSample.Application.Services.Clientes;
using ZSample.Application.Services.Clientes.Dtos;
using ZSample.Application.Services.Direcciones.Dtos;
using System.Reflection;

namespace ZSample.Host.Controllers.Clientes
{
    /// <summary>
    /// Controlado para recibir las peticiones referentes a los clientes.
    /// </summary>
    public class ClienteController : ApiController<Guid, IClienteService, ClienteCreationDto, ClienteUpdateDto, ClienteReadingDto>
    {
        public ClienteController(
            IClienteService service,
            ILogger<ClienteController> logger)
            : base(service, logger)
        {
        }

        /// <summary>
        /// Ejemplo de filtrado "complejo" de entidades relacionadas con filtrado, ordenación y paginación opcionales.
        /// </summary>
        /// <returns>DTO de lectura de la entidad cliente.</returns>
        [HttpGet("having-facturas-and-direcciones")]
        public IPagedEnumerable<ClienteReadingDto> GetHavingFacturasAndDirecciones([FromQuery] FilteringOptions? filteringOptions = null)
        {
            Logger.LogInformation($"Prueba de {MethodBase.GetCurrentMethod().Name}");

            return Service.GetHavingFacturasAndDirecciones(filteringOptions);
        }

        /// <summary>
        /// Crea e inserta un nuevo cliente con una dirección fiscal.
        /// </summary>
        /// <param name="entityDto">DTO de creación para la entidad cliente.</param>
        /// <param name="direccioCreationDto">DTO De creación para la entidad dirección.</param>
        /// <returns>DTO de lectura de la entidad cliente.</returns>
        [HttpPost("insert-with-direccion")]
        public ClienteReadingDto Insert([FromQuery] ClienteCreationDto entityDto, [FromQuery] DireccionCreationDto direccioCreationDto)
        {
            Logger.LogInformation("Esto es una prueba de insert with dirección");

            return Service.Insert(entityDto, direccioCreationDto);
        }
    }
}