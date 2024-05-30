using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Infrastructure.Data;
using Tekus.Infrastructure.Repositories;
using Xunit;

namespace Tekus.Tests.RepositoryTest
{
    public class CountryRepositoryTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly DataContext _context;
        private readonly CountryRepository _repository;

        public CountryRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseTekus")
                .Options;
            _context = new DataContext(_options);
            _repository = new CountryRepository(_context);

            // Seed the database
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var service1 = new Services { idservice = 1, service = "Mantenimiento de pc" };
            var service2 = new Services { idservice = 2, service = "Redes" };

            _context.Services.AddRange(service1, service2);

            var provider1 = new Provider { idprovider = 1, nit = "1234567890", name = "Tecnisoft", mail = "tecnisoft@gmail.com" };
            var provider2 = new Provider { idprovider = 2, nit = "0987654321", name = "ServiTech", mail = "servitech@yahoo.com" };

            _context.Providers.AddRange(provider1, provider2);

            var country1 = new Country { idcountry = 1, country = "Colombia", ProviderHasServices = new List<ProviderHasServices>() };
            var country2 = new Country { idcountry = 2, country = "Argentina", ProviderHasServices = new List<ProviderHasServices>() };

            _context.Countries.AddRange(country1, country2);

            var providerHasService1 = new ProviderHasServices { Providers_idprovider = 1, Services_idservice = 1, Country_idcountry = 1, Provider = provider1, Country = country1, Services = service1 };
            var providerHasService2 = new ProviderHasServices { Providers_idprovider = 2, Services_idservice = 2, Country_idcountry = 2, Provider = provider2, Country = country2, Services = service2 };

            _context.Providers_has_Services.AddRange(providerHasService1, providerHasService2);

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCountry_WhenCountryExists()
        {
            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.idcountry);
            Assert.Equal("Colombia", result.country);
            Assert.NotNull(result.ProviderHasServices);
            Assert.Single(result.ProviderHasServices);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenCountryDoesNotExist()
        {
            // Act
            var result = await _repository.GetByIdAsync(3);

            // Assert
            Assert.Null(result);
        }
    }
}
