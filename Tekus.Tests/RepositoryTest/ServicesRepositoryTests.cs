using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Infrastructure.Data;
using Tekus.Infrastructure.Repositories;
using Xunit;

namespace Tekus.Tests.RepositoryTest
{
    public class ServicesRepositoryTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly DataContext _context;
        private readonly ServicesRepository _repository;

        public ServicesRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseTekus")
                .Options;
            _context = new DataContext(_options);
            _repository = new ServicesRepository(_context);

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

            var service1 = new Services { idservice = 1, service = "Mantenimiento de pc" };
            var service2 = new Services { idservice = 2, service = "Redes" };

            _context.Services.AddRange(service1, service2);

            var providerHasService1 = new ProviderHasServices { Providers_idprovider = 1, Services_idservice = 1, Country_idcountry = 1, Provider = provider1, Country = country1 };
            var providerHasService2 = new ProviderHasServices { Providers_idprovider = 2, Services_idservice = 2, Country_idcountry = 2, Provider = provider2, Country = country2 };

            _context.Providers_has_Services.AddRange(providerHasService1, providerHasService2);

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllServices()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsService_WhenServiceExists()
        {
            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.idservice);
            Assert.Equal("Mantenimiento de pc", result.service);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenServiceDoesNotExist()
        {
            // Act
            var result = await _repository.GetByIdAsync(3);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_AddsService()
        {
            // Arrange
            var newService = new Services { idservice = 3, service = "Desarrollo de software" };

            // Act
            await _repository.AddAsync(newService);
            var result = await _repository.GetByIdAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.idservice);
            Assert.Equal("Desarrollo de software", result.service);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesService()
        {
            // Arrange
            var service = await _repository.GetByIdAsync(1);
            service.service = "Updated Service";

            // Act
            await _repository.UpdateAsync(service);
            var updatedService = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(updatedService);
            Assert.Equal("Updated Service", updatedService.service);
        }
    }
}
