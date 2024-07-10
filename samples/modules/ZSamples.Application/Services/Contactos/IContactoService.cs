using ZFramework.Application.Services;
using ZSample.Application.Services.Contactos.Dtos;

namespace ZSample.Application.Services.Contactos
{
    public interface IContactoService : IApplicationService<Guid, ContactoCreationDto, ContactoUpdateDto, ContactoReadingDto>
    {
    }
}