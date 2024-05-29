using Tekus.Application.Dtos;

namespace Tekus.Application.Interfaces
{
    public interface IProviderService
    {
        Task<IEnumerable<ProviderDto>> ListAsyncProviders();
        Task<ProviderDto> GetProviderByIdAsync(int id);
        Task<ProviderDto> CreateProviderAsync(ProviderDto providerDto);
        Task<IDictionary<string, int>> GetProviderCountByCountryAsync();

    }
}
