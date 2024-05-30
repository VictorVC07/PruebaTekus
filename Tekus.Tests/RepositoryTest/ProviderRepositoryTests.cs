
using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Infrastructure.Data;
using Tekus.Infrastructure.Repositories;
using Xunit;

namespace Tekus.Tests.RepositoryTest
{
    public class ProviderRepositoryTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly DataContext _context;
        private readonly ProviderRepository _repository;


        public ProviderRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseTekus")
                .Options;
            _context = new DataContext(_options);
            _repository = new ProviderRepository(_context);

            // Seed the database
            SeedDatabase();
        }
        private void SeedDatabase()
        {
            var country1 = new Country { idcountry = 1, country = "Colombia" };
            var country2 = new Country { idcountry = 2, country = "Argentina" };

            _context.Countries.AddRange(country1, country2);

            var provider1 = new Provider { idprovider = 1, nit = "1234567890", name = "Tecnisoft", mail = "tecnisoft@gmail.com" };
            var provider2 = new Provider { idprovider = 2, nit = "0987654321", name = "ServiTech", mail = "servitech@yahoo.com" };

            _context.Providers.AddRange(provider1, provider2);

            var providerHasService1 = new ProviderHasServices { Providers_idprovider = 1, Services_idservice = 1, Country_idcountry = 1, Country = country1 };
            var providerHasService2 = new ProviderHasServices { Providers_idprovider = 2, Services_idservice = 2, Country_idcountry = 2, Country = country2 };
            var providerHasService3 = new ProviderHasServices { Providers_idprovider = 1, Services_idservice = 3, Country_idcountry = 1, Country = country1 };

            _context.Providers_has_Services.AddRange(providerHasService1, providerHasService2, providerHasService3);

            _context.SaveChanges();
        }


        [Fact]
        public async Task GetAllAsync_ReturnsAllProviders()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsProvider_WhenProviderExists()
        {
            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.idprovider);
            Assert.Equal("Tecnisoft", result.name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenProviderDoesNotExist()
        {
            // Act
            var result = await _repository.GetByIdAsync(3);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetProviderCountByCountryAsync_ReturnsCorrectCounts()
        {
            // Act
            var result = await _repository.GetProviderCountByCountryAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result["Colombia"]);
            Assert.Equal(1, result["Argentina"]);
        }

    }
}
