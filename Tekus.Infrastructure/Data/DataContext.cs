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


            modelBuilder.Entity<ProviderHasServices>()
                .Property(e => e.time_value).IsRequired();

            // Seed Data for Providers
            modelBuilder.Entity<Provider>().HasData(
                new Provider { idprovider = 1, nit = "900123456-1", name = "Tecnisoft", mail = "tecnisoft@gmail.com" },
                new Provider { idprovider = 2, nit = "900234567-2", name = "ServiTech", mail = "servitech@yahoo.com" },
                new Provider { idprovider = 3, nit = "900345678-3", name = "Soluciones IT", mail = "solucionesit@hotmail.com" },
                new Provider { idprovider = 4, nit = "900456789-4", name = "DigitalWare", mail = "digitalware@gmail.com" },
                new Provider { idprovider = 5, nit = "900567890-5", name = "TechPro", mail = "techpro@yahoo.com" },
                new Provider { idprovider = 6, nit = "900678901-6", name = "InfoTech", mail = "infotech@hotmail.com" },
                new Provider { idprovider = 7, nit = "900789012-7", name = "CyberSoft", mail = "cybersoft@gmail.com" },
                new Provider { idprovider = 8, nit = "900890123-8", name = "CompuNet", mail = "compunet@yahoo.com" },
                new Provider { idprovider = 9, nit = "900901234-9", name = "NetSolutions", mail = "netsolutions@hotmail.com" },
                new Provider { idprovider = 10, nit = "900012345-0", name = "SoftTech", mail = "softtech@gmail.com" }
            );

            // Seed Data for Services
            modelBuilder.Entity<Services>().HasData(
                new Services { idservice = 1, service = "Mantenimiento de PC" },
                new Services { idservice = 2, service = "Instalación de Software" },
                new Services { idservice = 3, service = "Soporte Técnico Remoto" },
                new Services { idservice = 4, service = "Consultoría IT" },
                new Services { idservice = 5, service = "Desarrollo de Software" },
                new Services { idservice = 6, service = "Seguridad Informática" },
                new Services { idservice = 7, service = "Hosting y Dominios" },
                new Services { idservice = 8, service = "Diseño Web" },
                new Services { idservice = 9, service = "Redes y Telecomunicaciones" },
                new Services { idservice = 10, service = "Backup y Recuperación" }
            );

            // Seed Data for Countries
            modelBuilder.Entity<Country>().HasData(
                new Country { idcountry = 1, country = "Colombia" },
                new Country { idcountry = 2, country = "United States" },
                new Country { idcountry = 3, country = "Canada" },
                new Country { idcountry = 4, country = "Mexico" },
                new Country { idcountry = 5, country = "Brazil" },
                new Country { idcountry = 6, country = "Argentina" },
                new Country { idcountry = 7, country = "Chile" },
                new Country { idcountry = 8, country = "Peru" },
                new Country { idcountry = 9, country = "Venezuela" },
                new Country { idcountry = 10, country = "Uruguay" }
            );

            // Seed Data for ProviderHasServices
            modelBuilder.Entity<ProviderHasServices>().HasData(
                new ProviderHasServices { Providers_idprovider = 1, Services_idservice = 1, Country_idcountry = 1, time_value = 100000 },
                new ProviderHasServices { Providers_idprovider = 1, Services_idservice = 2, Country_idcountry = 2, time_value = 150000 },
                new ProviderHasServices { Providers_idprovider = 2, Services_idservice = 3, Country_idcountry = 3, time_value = 200000 },
                new ProviderHasServices { Providers_idprovider = 3, Services_idservice = 4, Country_idcountry = 4, time_value = 250000 },
                new ProviderHasServices { Providers_idprovider = 4, Services_idservice = 5, Country_idcountry = 5, time_value = 300000 },
                new ProviderHasServices { Providers_idprovider = 5, Services_idservice = 6, Country_idcountry = 6, time_value = 350000 },
                new ProviderHasServices { Providers_idprovider = 6, Services_idservice = 7, Country_idcountry = 7, time_value = 400000 },
                new ProviderHasServices { Providers_idprovider = 7, Services_idservice = 8, Country_idcountry = 8, time_value = 450000 },
                new ProviderHasServices { Providers_idprovider = 8, Services_idservice = 9, Country_idcountry = 9, time_value = 500000 },
                new ProviderHasServices { Providers_idprovider = 9, Services_idservice = 10, Country_idcountry = 10, time_value = 550000 }
            );
        }
    }
}
