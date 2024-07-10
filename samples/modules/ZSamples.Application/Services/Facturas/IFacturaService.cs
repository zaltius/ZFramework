using ZFramework.Application.Services;
using ZSample.Application.Services.Facturas.Dtos;


namespace ZSample.Application.Services.Facturas
{
    public interface IFacturaService : IApplicationService<Guid, FacturaCreationDto, FacturaUpdateDto, FacturaReadingDto>
    {
    }
}