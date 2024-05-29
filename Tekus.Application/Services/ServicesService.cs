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
        private readonly IMapper _mapper;


        public ServicesService(IServicesRepository servicesRepository, IMapper mapper)
        {
            _servicesRepository = servicesRepository;
            _mapper = mapper;
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
