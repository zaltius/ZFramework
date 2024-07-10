using AutoMapper;
using Microsoft.Extensions.Logging;
using ZFramework.Application.Services;
using ZFramework.Domain.UnitOfWork;
using ZSample.Application.Services.Direcciones.Dtos;
using ZSample.Domain.Entities.Direcciones;

namespace ZSample.Application.Services.Direcciones
{
    public class DireccionService : ApplicationService<Direccion, Guid, IDireccionRepository, DireccionCreationDto, DireccionUpdateDto, DireccionReadingDto>, IDireccionService
    {
        private readonly DireccionDomainService _domainService;

        public DireccionService(
            ILogger<DireccionService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IDireccionRepository repository,
            DireccionDomainService domainService)
            : base(logger, mapper, unitOfWork, repository)
        {
            _domainService = domainService;
        }

        public override DireccionReadingDto Insert(DireccionCreationDto entityDto)
        {
            var direccion = _domainService.Create(entityDto.Calle, entityDto.Descripcion, entityDto.Poblacion, entityDto.CodigoPostal, entityDto.TipoDireccion);

            direccion = Repository.Insert(direccion);

            Logger.LogInformation("Esto es una prueba de insert custom de dirección");

            return Mapper.Map<DireccionReadingDto>(direccion);
        }
    }
}