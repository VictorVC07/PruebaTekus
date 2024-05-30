
using AutoMapper;
using Moq;
using Tekus.Application.Dtos;
using Tekus.Application.Services;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Xunit;

namespace Tekus.Tests.ServicesTest
{
    public class ServicesServiceTests
    {
        private readonly Mock<IServicesRepository> _mockServicesRepository;
        private readonly Mock<IProviderRepository> _mockProviderRepository;
        private readonly Mock<ICountryRepository> _mockCountryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ServicesService _servicesService;

        public ServicesServiceTests()
        {
            _mockServicesRepository = new Mock<IServicesRepository>();
            _mockProviderRepository = new Mock<IProviderRepository>();
            _mockCountryRepository = new Mock<ICountryRepository>();
            _mockMapper = new Mock<IMapper>();
            _servicesService = new ServicesService(
                _mockServicesRepository.Object,
                _mockMapper.Object,
                _mockProviderRepository.Object,
                _mockCountryRepository.Object
            );
        }

        [Fact]
        public async Task GetAllServicesAsync_ReturnsListOfServiceDtos()
        {
            // Arrange
            var services = new List<Services>
            {
                new Services { idservice = 1, service = "Mantenimiento de pc" },
                new Services { idservice = 2, service = "Desarrollo de software" }
            };
            var serviceDtos = new List<ServiceDto>
            {
                new ServiceDto { Id = 1, Name = "Mantenimiento de pc" },
                new ServiceDto { Id = 2, Name = "Desarrollo de software " }
            };

            _mockServicesRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(services);
            _mockMapper.Setup(m => m.Map<IEnumerable<ServiceDto>>(It.IsAny<IEnumerable<Services>>())).Returns(serviceDtos);

            // Act
            var result = await _servicesService.GetAllServicesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ServiceDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateServiceAsync_ReturnsCreatedServiceDto()
        {
            // Arrange
            var serviceDto = new ServiceDto { Name = "Redes", Providers = new List<ProviderDto>() };
            var service = new Services { idservice = 3, service = "Redes" };
            var createdServiceDto = new ServiceDto { Id = 3, Name = "Redes" };

            _mockMapper.Setup(m => m.Map<Services>(serviceDto)).Returns(service);
            _mockMapper.Setup(m => m.Map<ServiceDto>(service)).Returns(createdServiceDto);

            _mockServicesRepository.Setup(repo => repo.AddAsync(It.IsAny<Services>())).Returns(Task.CompletedTask);

            // Act
            var result = await _servicesService.CreateServiceAsync(serviceDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ServiceDto>(result);
            Assert.Equal(3, result.Id);
            Assert.Equal("Redes", result.Name);
        }

        [Fact]
        public async Task UpdateServiceAsync_ReturnsUpdatedServiceDto()
        {
            // Arrange
            var serviceDto = new ServiceDto { Id = 1, Name = "Updated Service", Providers = new List<ProviderDto>() };
            var service = new Services { idservice = 1, service = "Service 1", ProviderHasServices = new List<ProviderHasServices>() };

            _mockServicesRepository.Setup(repo => repo.GetByIdAsync(serviceDto.Id)).ReturnsAsync(service);
            _mockServicesRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Services>())).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<ServiceDto>(service)).Returns(serviceDto);

            // Act
            var result = await _servicesService.UpdateServiceAsync(serviceDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ServiceDto>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Updated Service", result.Name);
        }

        [Fact]
        public async Task UpdateServiceAsync_ThrowsKeyNotFoundException_WhenServiceDoesNotExist()
        {
            // Arrange
            var serviceDto = new ServiceDto { Id = 1, Name = "Updated Service", Providers = new List<ProviderDto>() };

            _mockServicesRepository.Setup(repo => repo.GetByIdAsync(serviceDto.Id)).ReturnsAsync((Services)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _servicesService.UpdateServiceAsync(serviceDto));
        }

        [Fact]
        public async Task GetServiceCountByCountryAsync_ReturnsDictionary()
        {
            // Arrange
            var services = new List<Services>
            {
                new Services
                {
                    idservice = 1,
                    service = "Redes",
                    ProviderHasServices = new List<ProviderHasServices>
                    {
                        new ProviderHasServices { Country = new Country { country = "Colombia" } },
                        new ProviderHasServices { Country = new Country { country = "Argentina" } }
                    }
                },
                new Services
                {
                    idservice = 2,
                    service = "Mantenimiento de pc",
                    ProviderHasServices = new List<ProviderHasServices>
                    {
                        new ProviderHasServices { Country = new Country { country = "Colombia" } }
                    }
                }
            };

            var expectedCounts = new Dictionary<string, int>
            {
                { "Colombia", 2 },
                { "Argentina", 1 }
            };

            _mockServicesRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(services);

            // Act
            var result = await _servicesService.GetServiceCountByCountryAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Dictionary<string, int>>(result);
            Assert.Equal(expectedCounts["Colombia"], result["Colombia"]);
            Assert.Equal(expectedCounts["Argentina"], result["Argentina"]);
        }
    }
}
