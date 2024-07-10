using ZFramework.Application.Services;
using ZSample.Application.Services.ClientesDirecciones.Dtos;


namespace ZSample.Application.Services.ClientesDirecciones
{
    public interface IClienteDireccionService : IApplicationService<Guid, ClienteDireccionCreationDto, ClienteDireccionUpdateDto, ClienteDireccionReadingDto>
    {
    }
}