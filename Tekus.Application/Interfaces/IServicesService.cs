

using Tekus.Application.Dtos;

namespace Tekus.Application.Interfaces
{
    public interface IServicesService
    {
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
    }
}
