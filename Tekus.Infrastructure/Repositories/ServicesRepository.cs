using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Data;

namespace Tekus.Infrastructure.Repositories
{
    public class ServicesRepository : IServicesRepository
    {

        private readonly DataContext _context;

        public ServicesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Services>> GetAllAsync()
        {
            return await _context.Services
                .Include(s => s.ProviderHasServices)
                    .ThenInclude(ps => ps.Provider)
                .Include(s => s.ProviderHasServices)
                    .ThenInclude(ps => ps.Country)
                .ToListAsync();
        }

        public async Task AddAsync(Services service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task<Services> GetByIdAsync(int id)
        {
            return await _context.Services
                .Include(s => s.ProviderHasServices)
                .ThenInclude(ps => ps.Provider)
                .Include(s => s.ProviderHasServices)
                .ThenInclude(ps => ps.Country)
                .FirstOrDefaultAsync(s => s.idservice == id);
        }

        public async Task UpdateAsync(Services service)
        {
            _context.Entry(service).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
