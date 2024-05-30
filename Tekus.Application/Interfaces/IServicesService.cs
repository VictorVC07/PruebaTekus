

using Tekus.Application.Dtos;

namespace Tekus.Application.Interfaces
{
    public interface IServicesService
    {
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync();

        Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto);
        Task<IDictionary<string, int>> GetServiceCountByCountryAsync();
        Task<ServiceDto> UpdateServiceAsync(ServiceDto serviceDto);
    }
}
