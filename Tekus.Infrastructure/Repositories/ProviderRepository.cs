using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Data;

namespace Tekus.Infrastructure.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly DataContext _context;

        public ProviderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await _context.Providers.ToListAsync();
        }

        public async Task<Provider> GetByIdAsync(int id)
        {
            return await _context.Providers
                .Include(p => p.ProviderHasServices)
                    .ThenInclude(ps => ps.Services)
                .FirstOrDefaultAsync(p => p.idprovider == id);
        }

        public async Task AddAsync(Provider proveedor)
        {
            await _context.Providers.AddAsync(proveedor);
            await _context.SaveChangesAsync();
        }

        public async Task<IDictionary<string, int>> GetProviderCountByCountryAsync()
        {
            return await _context.Providers_has_Services
                .GroupBy(ps => ps.Country.country)
                .Select(g => new { Country = g.Key, Count = g.Select(ps => ps.Providers_idprovider).Distinct().Count() })
                .ToDictionaryAsync(x => x.Country, x => x.Count);
        }

    }
}
