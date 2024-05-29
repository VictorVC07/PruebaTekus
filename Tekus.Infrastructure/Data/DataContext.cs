using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProviderHasServices> Providers_has_Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Provider>().HasKey(p => p.idprovider);
            modelBuilder.Entity<Services>().HasKey(s => s.idservice);
            modelBuilder.Entity<Country>().HasKey(p => p.idcountry);

            modelBuilder.Entity<ProviderHasServices>()
                .HasKey(ps => new { ps.Providers_idprovider, ps.Services_idservice, ps.Country_idcountry });

            modelBuilder.Entity<ProviderHasServices>()
                .HasOne(ps => ps.Provider)
                .WithMany(p => p.ProviderHasServices)
                .HasForeignKey(ps => ps.Providers_idprovider)
                .OnDelete(DeleteBehavior.Cascade); // Configuración de eliminación en cascada

            modelBuilder.Entity<ProviderHasServices>()
                .HasOne(ps => ps.Services)
                .WithMany(s => s.ProviderHasServices)
                .HasForeignKey(ps => ps.Services_idservice)
                .OnDelete(DeleteBehavior.Cascade); // Configuración de eliminación en cascada

            modelBuilder.Entity<ProviderHasServices>()
                .HasOne(ps => ps.Country)
                .WithMany(p => p.ProviderHasServices)
                .HasForeignKey(ps => ps.Country_idcountry)
                .OnDelete(DeleteBehavior.Cascade);// Configurar comportamiento de eliminación en cascada
        }
    }
}
