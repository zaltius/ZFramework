using AutoMapper;
using Microsoft.Extensions.Logging;
using ZFramework.Application.Services;
using ZFramework.Domain.UnitOfWork;
using ZSample.Application.Services.ClientesDirecciones.Dtos;
using ZSample.Domain.Entities.ClientesDirecciones;

namespace ZSample.Application.Services.ClientesDirecciones
{
    public class ClienteDireccionService : ApplicationService<ClienteDireccion, Guid, IClienteDireccionRepository, ClienteDireccionCreationDto, ClienteDireccionUpdateDto, ClienteDireccionReadingDto>, IClienteDireccionService
    {
        public ClienteDireccionService(
            ILogger<ClienteDireccionService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IClienteDireccionRepository repository)
            : base(logger, mapper, unitOfWork, repository)
        {
        }
    }
}