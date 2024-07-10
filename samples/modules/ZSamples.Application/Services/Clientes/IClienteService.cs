using ZFramework.Application.Services;
using ZFramework.Common.Filtering;
using ZSample.Application.Services.Clientes.Dtos;
using ZSample.Application.Services.Direcciones.Dtos;

namespace ZSample.Application.Services.Clientes
{
    public interface IClienteService : IApplicationService<Guid, ClienteCreationDto, ClienteUpdateDto, ClienteReadingDto>
    {
        IPagedEnumerable<ClienteReadingDto> GetHavingFacturasAndDirecciones(IFilteringOptions? filteringOptions = null);

        ClienteReadingDto Insert(ClienteCreationDto entityDto, DireccionCreationDto direccionCreationDto);
    }
}