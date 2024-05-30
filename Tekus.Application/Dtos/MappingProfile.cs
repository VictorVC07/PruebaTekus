using AutoMapper;
using Tekus.Domain.Entities;

namespace Tekus.Application.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapping for Provider and ProviderDto
            CreateMap<Provider, ProviderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.idprovider))  
                .ForMember(dest => dest.Nit, opt => opt.MapFrom(src => src.nit))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.mail))
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.ProviderHasServices != null ? src.ProviderHasServices.Select(phs => new ProviderCountryDto
                {
                    Id = phs.Country.idcountry,
                    Country = phs.Country.country,
                    ValueTime = phs.time_value
                }).ToList() : new List<ProviderCountryDto>()))
                .ReverseMap();

            // Mapping for Services and ServicesDto
            CreateMap<Domain.Entities.Services, ServiceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.idservice))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.service))
                .ForMember(dest => dest.Providers, opt => opt.MapFrom(src => src.ProviderHasServices != null ? src.ProviderHasServices
                    .GroupBy(phs => phs.Provider)
                    .Select(g => new ProviderDto
                    {
                        Id = g.Key.idprovider,
                        Nit = g.Key.nit,
                        Name = g.Key.name,
                        Mail = g.Key.mail,
                        Countries = g.Select(phs => new ProviderCountryDto
                        {
                            Id = phs.Country.idcountry,
                            Country = phs.Country.country,
                            ValueTime = phs.time_value
                        }).ToList()
                    }).ToList() : new List<ProviderDto>()))
                .ReverseMap();


            // Mapping for Country and CountryDto
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.idcountry))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
                .ReverseMap();

            // Mapping for ProviderCountryDto and Country
            CreateMap<ProviderCountryDto, Country>()
                .ForMember(dest => dest.idcountry, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.country, opt => opt.MapFrom(src => src.Country))
                .ReverseMap();
        }
    }
}
