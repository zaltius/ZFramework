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
        /// Ejemplo de filtrado "complejo" de entidades relacionadas con filtrado, ordenaci�n y paginaci�n opcionales.
        /// </summary>
        /// <returns>DTO de lectura de la entidad cliente.</returns>
        [HttpGet("having-facturas-and-direcciones")]
        public IPagedEnumerable<ClienteReadingDto> GetHavingFacturasAndDirecciones([FromQuery] FilteringOptions? filteringOptions = null)
        {
            Logger.LogInformation($"Prueba de {MethodBase.GetCurrentMethod().Name}");

            return Service.GetHavingFacturasAndDirecciones(filteringOptions);
        }

        /// <summary>
        /// Crea e inserta un nuevo cliente con una direcci�n fiscal.
        /// </summary>
        /// <param name="entityDto">DTO de creaci�n para la entidad cliente.</param>
        /// <param name="direccioCreationDto">DTO De creaci�n para la entidad direcci�n.</param>
        /// <returns>DTO de lectura de la entidad cliente.</returns>
        [HttpPost("insert-with-direccion")]
        public ClienteReadingDto Insert([FromQuery] ClienteCreationDto entityDto, [FromQuery] DireccionCreationDto direccioCreationDto)
        {
            Logger.LogInformation("Esto es una prueba de insert with direcci�n");

            return Service.Insert(entityDto, direccioCreationDto);
        }
    }
}