using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Application.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Provider, ProviderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.idprovider))  // Mapea explícitamente el Id
                .ForMember(dest => dest.Nit, opt => opt.MapFrom(src => src.nit))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.mail))
                .ReverseMap();
        }
    }
}
