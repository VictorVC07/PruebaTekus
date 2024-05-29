using AutoMapper;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;


namespace Tekus.Application.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;


        public ProviderService(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProviderDto>> ListAsyncProviders()
        {
            var provider = await _providerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProviderDto>>(provider);
        }

        public async Task<ProviderDto> GetProviderByIdAsync(int id)
        {
            var provider = await _providerRepository.GetByIdAsync(id);
            return _mapper.Map<ProviderDto>(provider);
        }

        public async Task<ProviderDto> CreateProviderAsync(ProviderDto providerDto)
        {
            var provider = _mapper.Map<Provider>(providerDto);
            await _providerRepository.AddAsync(provider);
            return _mapper.Map<ProviderDto>(provider);
        }

        public async Task<IDictionary<string, int>> GetProviderCountByCountryAsync()
        {
            return await _providerRepository.GetProviderCountByCountryAsync();
        }
    }
}
