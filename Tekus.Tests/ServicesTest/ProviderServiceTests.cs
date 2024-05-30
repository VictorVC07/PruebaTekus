
using AutoMapper;
using Moq;
using Tekus.Application.Dtos;
using Tekus.Application.Services;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Xunit;

namespace Tekus.Tests.ServicesTest
{
    public class ProviderServiceTests
    {
        private readonly Mock<IProviderRepository> _mockProviderRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProviderService _providerService;

        public ProviderServiceTests()
        {
            _mockProviderRepository = new Mock<IProviderRepository>();
            _mockMapper = new Mock<IMapper>();
            _providerService = new ProviderService(_mockProviderRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ListAsyncProviders_ReturnsListOfProviderDtos()
        {
            // Arrange
            var providers = new List<Provider>
            {
                new Provider { idprovider = 1, name = "Provider 1", nit = "1234567890", mail = "provider1@example.com" },
                new Provider { idprovider = 2, name = "Provider 2", nit = "0987654321", mail = "provider2@example.com" }
            };
            var providerDtos = new List<ProviderDto>
            {
                new ProviderDto { Id = 1, Name = "Provider 1", Nit = "1234567890", Mail = "provider1@example.com" },
                new ProviderDto { Id = 2, Name = "Provider 2", Nit = "0987654321", Mail = "provider2@example.com" }
            };

            _mockProviderRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(providers);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProviderDto>>(It.IsAny<IEnumerable<Provider>>())).Returns(providerDtos);

            // Act
            var result = await _providerService.ListAsyncProviders();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProviderDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetProviderByIdAsync_ReturnsProviderDto()
        {
            // Arrange
            var provider = new Provider { idprovider = 1, name = "Provider 1",nit = "1234567890", mail = "provider1@example.com" };
            var providerDto = new ProviderDto { Id = 1, Name = "Provider 1", Nit = "1234567890", Mail = "provider1@example.com" };

            _mockProviderRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(provider);
            _mockMapper.Setup(m => m.Map<ProviderDto>(It.IsAny<Provider>())).Returns(providerDto);

            // Act
            var result = await _providerService.GetProviderByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProviderDto>(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetProviderCountByCountryAsync_ReturnsDictionary()
        {
            // Arrange
            var providerCounts = new Dictionary<string, int>
            {
                { "Mexico", 10 },
                { "Colombia", 5 }
            };

            _mockProviderRepository.Setup(repo => repo.GetProviderCountByCountryAsync()).ReturnsAsync(providerCounts);

            // Act
            var result = await _providerService.GetProviderCountByCountryAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Dictionary<string, int>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(10, result["Mexico"]);
            Assert.Equal(5, result["Colombia"]);
        }
    }
}
