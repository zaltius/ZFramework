using AutoMapper;
using ZSample.Domain.Entities.Clientes;
using ZSample.Domain.Entities.TiposDirecciones;

namespace ZSample.Application.Services.Clientes.Dtos
{
    public sealed class ClienteMapProfile : Profile
    {
        public ClienteMapProfile()
        {
            CreateMap<Cliente, ClienteReadingDto>()
                .ForMember(src => src.DireccionFiscal, opt => opt.MapFrom(dst => dst.Direcciones.FirstOrDefault(d => d.Direccion.TipoDireccion == TipoDireccionEnum.Fiscal).Direccion))
                .ForMember(src => src.ImporteFacturasCurrentYear, opt => opt.MapFrom(dst => dst.Facturas.Where(f => f.FechaEntrada.Year.Equals(DateTime.Now.Year)).Select(f => f.Total).Sum()));

            CreateMap<ClienteCreationDto, Cliente>();

            CreateMap<ClienteUpdateDto, Cliente>();
        }
    }
}