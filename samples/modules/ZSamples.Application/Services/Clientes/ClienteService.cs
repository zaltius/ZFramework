using AutoMapper;
using Microsoft.Extensions.Logging;
using ZFramework.Application.Services;
using ZFramework.Common;
using ZFramework.Common.Filtering;
using ZFramework.Domain.Entities.Auditing;
using ZFramework.Domain.UnitOfWork;
using ZSample.Application.Services.Clientes.Dtos;
using ZSample.Application.Services.Direcciones.Dtos;
using ZSample.Domain.Entities.Clientes;
using ZSample.Domain.Entities.ClientesDirecciones;
using ZSample.Domain.Entities.Contactos;
using ZSample.Domain.Entities.Direcciones;

namespace ZSample.Application.Services.Clientes
{
    public class ClienteService : ApplicationService<Cliente, Guid, IClienteRepository, ClienteCreationDto, ClienteUpdateDto, ClienteReadingDto>, IClienteService
    {
        private readonly IContactoRepository _contactoRepository;

        private readonly DireccionDomainService _direccionDomainService;

        private readonly IDireccionRepository _direccionRepository;

        private readonly ClienteDomainService _domainService;

        public ClienteService(
            ILogger<ClienteService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IClienteRepository repository,
            ClienteDomainService domainService,
            DireccionDomainService direccionDomainService,
            IDireccionRepository direccionRepository,
            IContactoRepository contactoRepository)
            : base(logger, mapper, unitOfWork, repository)
        {
            _domainService = domainService;
            _direccionDomainService = direccionDomainService;
            _direccionRepository = direccionRepository;
            _contactoRepository = contactoRepository;
        }

        public IPagedEnumerable<ClienteReadingDto> GetHavingFacturasAndDirecciones(IFilteringOptions? filteringOptions = null)
        {
            var clientes = Repository.GetHavingFacturasAndDirecciones(filteringOptions);

            return clientes.Map(Mapper.Map<ClienteReadingDto>);
        }

        public ClienteReadingDto Insert(ClienteCreationDto creationDto, DireccionCreationDto direccionCreationDto)
        {
            var cliente = Insert(creationDto);

            var direccion = _direccionDomainService.Create(direccionCreationDto.Calle, direccionCreationDto.Descripcion, direccionCreationDto.Poblacion, direccionCreationDto.CodigoPostal, direccionCreationDto.TipoDireccion);

            direccion.Clientes.Add(new ClienteDireccion() { ClienteId = cliente.Id });

            _direccionRepository.Insert(direccion);

            var contacto = _contactoRepository.GetInitialByClienteId(cliente.Id);

            if (contacto != null)
            {
                contacto.Direccion = direccion.ToString();

                _contactoRepository.Update(contacto);
            }

            UnitOfWork.Commit();

            return Mapper.Map<ClienteReadingDto>(cliente);
        }

        public override ClienteReadingDto Insert(ClienteCreationDto entityDto)
        {
            var cliente = _domainService.Create(entityDto.Cif, entityDto.RazonSocial);

            cliente.FechaConstitucion = entityDto.FechaConstitucion;

            Repository.Insert(cliente);

            return Mapper.Map<ClienteReadingDto>(cliente);
        }

        public override ClienteReadingDto Update(ClienteUpdateDto updateDto)
        {
            var cliente = Repository.GetById(updateDto.Id);

            Check.NotNullEntity(cliente, updateDto.Id);

            cliente!.CheckConcurrencyUpdate(updateDto);

            cliente!.SetRazonSocial(updateDto.RazonSocial);

            cliente.SetCif(updateDto.Cif);

            Mapper.Map(updateDto, cliente);

            Repository.Update(cliente);

            UnitOfWork.Commit();

            cliente = Repository.GetById(cliente.Id);

            return Mapper.Map<ClienteReadingDto>(cliente);
        }
    }
}