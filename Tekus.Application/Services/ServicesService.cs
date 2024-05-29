using AutoMapper;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;
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

    }
}
