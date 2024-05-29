﻿using AutoMapper;
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
            //Mapping for Provider and ProviderDto
            CreateMap<Provider, ProviderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.idprovider))  
                .ForMember(dest => dest.Nit, opt => opt.MapFrom(src => src.nit))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.mail))
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.ProviderHasServices.Select(phs => new ProviderCountryDto
                {
                    Id = phs.Country.idcountry,
                    Country = phs.Country.country,
                    ValueTime = phs.time_value
                }).ToList()))
                .ReverseMap();

            // Mapping for Services and ServicesDto
            CreateMap<Domain.Entities.Services, ServiceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.idservice))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.service))
                .ForMember(dest => dest.Providers, opt => opt.MapFrom(src => src.ProviderHasServices
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
                    }).ToList()))
                .ReverseMap();


            // Mapping for Country and CountryDto
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.idcountry))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
                .ReverseMap();
        }
    }
}
