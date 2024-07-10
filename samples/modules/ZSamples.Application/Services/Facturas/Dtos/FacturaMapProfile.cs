using AutoMapper;
using ZSample.Domain.Entities.Facturas;

namespace ZSample.Application.Services.Facturas.Dtos
{
    public sealed class FacturaMapProfile : Profile
    {
        public FacturaMapProfile()
        {
            CreateMap<Factura, FacturaReadingDto>()
                .ForMember(src => src.ClienteRazonSocial, opt => opt.MapFrom(dst => dst.Cliente.RazonSocial));

            CreateMap<FacturaCreationDto, Factura>();

            CreateMap<FacturaUpdateDto, Factura>();
        }
    }
}