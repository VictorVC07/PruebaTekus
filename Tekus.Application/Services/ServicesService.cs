using AutoMapper;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;


        public ServicesService(IServicesRepository servicesRepository, IMapper mapper,
            IProviderRepository providerRepository, ICountryRepository countryRepository)
        {
            _servicesRepository = servicesRepository;
            _mapper = mapper;
            _providerRepository = providerRepository;
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var services = await _servicesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto)
        {
            var service = _mapper.Map<Domain.Entities.Services>(serviceDto);

            foreach (var providerDto in serviceDto.Providers)
            {
                var provider = _mapper.Map<Provider>(providerDto);

                foreach (var countryDto in providerDto.Countries)
                {
                    var country = _mapper.Map<Country>(countryDto);
                    service.ProviderHasServices.Add(new ProviderHasServices
                    {
                        Provider = provider,
                        Country = country,
                        time_value = countryDto.ValueTime
                    });
                }
            }

            await _servicesRepository.AddAsync(service);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> UpdateServiceAsync(ServiceDto serviceDto)
        {
            var service = await _servicesRepository.GetByIdAsync(serviceDto.Id);
            if (service == null)
            {
                throw new KeyNotFoundException($"Service with ID {serviceDto.Id} not found.");
            }

            service.service = serviceDto.Name;
            service.ProviderHasServices.Clear();

            foreach (var providerDto in serviceDto.Providers)
            {
                var provider = await _providerRepository.GetByIdAsync(providerDto.Id);
                if (provider == null)
                {
                    provider = new Provider
                    {
                        idprovider = providerDto.Id,
                        nit = providerDto.Nit,
                        name = providerDto.Name,
                        mail = providerDto.Mail
                    };
                }
                else
                {
                    provider.nit = providerDto.Nit;
                    provider.name = providerDto.Name;
                    provider.mail = providerDto.Mail;
                }


                foreach (var countryDto in providerDto.Countries)
                {
                    var country = await _countryRepository.GetByIdAsync(countryDto.Id);
                    if (country == null)
                    {
                        throw new KeyNotFoundException($"Country with ID {countryDto.Id} not found.");
                    }

                   
                    var providerHasService = new ProviderHasServices
                    {
                        Provider = provider,
                        Country = country,
                        time_value = countryDto.ValueTime,
                        Providers_idprovider = provider.idprovider,
                        Services_idservice = service.idservice,
                        Country_idcountry = country.idcountry
                    };

                    service.ProviderHasServices.Add(providerHasService);

                    
                }

            }
            await _servicesRepository.UpdateAsync(service);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<IDictionary<string, int>> GetServiceCountByCountryAsync()
        {
            var services = await _servicesRepository.GetAllAsync();
            return services
                .SelectMany(s => s.ProviderHasServices)
                .GroupBy(ps => ps.Country.country)
                .ToDictionary(g => g.Key, g => g.Count());
        }

    }
}
