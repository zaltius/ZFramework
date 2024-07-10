using ZFramework.Application.Services;
using ZSample.Application.Services.Direcciones.Dtos;

namespace ZSample.Application.Services.Direcciones
{
    public interface IDireccionService : IApplicationService<Guid, DireccionCreationDto, DireccionUpdateDto, DireccionReadingDto>
    {
    }
}