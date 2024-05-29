using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces
{
    public interface IServicesRepository
    {
        Task<IEnumerable<Services>> GetAllAsync();

    }

}
