using AutoMapper;
using Microsoft.Extensions.Logging;
using ZFramework.Application.Services;
using ZFramework.Domain.UnitOfWork;
using ZSample.Application.Services.Facturas.Dtos;
using ZSample.Domain.Entities.Facturas;

namespace ZSample.Application.Services.Facturas
{
    public class FacturaService : ApplicationService<Factura, Guid, IFacturaRepository, FacturaCreationDto, FacturaUpdateDto, FacturaReadingDto>, IFacturaService
    {
        private readonly FacturaDomainService _domainService;

        public FacturaService(
            ILogger<FacturaService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IFacturaRepository repository,
            FacturaDomainService domainService)
            : base(logger, mapper, unitOfWork, repository)
        {
            _domainService = domainService;
        }

        public override FacturaReadingDto Insert(FacturaCreationDto creationDto)
        {
            var factura = _domainService.Create(creationDto.ClienteId, creationDto.Numero, creationDto.Serie, creationDto.BaseImponible, creationDto.PctIva);

            factura = Repository.Insert(factura);

            return Mapper.Map<FacturaReadingDto>(factura);
        }
    }
}