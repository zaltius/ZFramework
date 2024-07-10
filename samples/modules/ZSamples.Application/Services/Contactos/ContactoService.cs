using AutoMapper;
using Microsoft.Extensions.Logging;
using ZFramework.Application.Services;
using ZFramework.Domain.UnitOfWork;
using ZSample.Application.Services.Contactos.Dtos;
using ZSample.Domain.Entities.Contactos;

namespace ZSample.Application.Services.Contactos
{
    public class ContactoService : ApplicationService<Contacto, Guid, IContactoRepository, ContactoCreationDto, ContactoUpdateDto, ContactoReadingDto>, IContactoService
    {
        public ContactoService(
            ILogger<ContactoService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IContactoRepository repository)
            : base(logger, mapper, unitOfWork, repository)
        {
        }
    }
}