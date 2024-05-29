using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces
{
    public interface IProviderRepository
    {
        Task<IEnumerable<Provider>> GetAllAsync();
        Task<Provider> GetByIdAsync(int id);
        Task AddAsync(Provider provider);
        Task<IDictionary<string, int>> GetProviderCountByCountryAsync();
    }
}
