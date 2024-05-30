using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Data;

namespace Tekus.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Country> GetByIdAsync(int id)
        {
            return await _context.Countries
                .Include(p => p.ProviderHasServices)
                    .ThenInclude(ps => ps.Services)
                .FirstOrDefaultAsync(p => p.idcountry == id);
        }
    }
}
