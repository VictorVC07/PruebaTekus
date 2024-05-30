using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> GetByIdAsync(int id);
    }
}
