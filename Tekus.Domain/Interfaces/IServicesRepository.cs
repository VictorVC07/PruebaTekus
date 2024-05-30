using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces
{
    public interface IServicesRepository
    {
        Task AddAsync(Services service);
        Task<IEnumerable<Services>> GetAllAsync();
        Task<Services> GetByIdAsync(int id);
        Task UpdateAsync(Services service);


    }

}
