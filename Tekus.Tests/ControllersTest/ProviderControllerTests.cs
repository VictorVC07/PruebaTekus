using Microsoft.AspNetCore.Mvc;
using Moq;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;
using Tekus.WebApi.Controllers;
using Xunit;

namespace Tekus.Tests.ControllersTest
{
    public class ProviderControllerTests
    {
        private readonly Mock<IProviderService> _mockProviderService;
        private readonly ProviderController _controller;

        public ProviderControllerTests()
        {
            _mockProviderService = new Mock<IProviderService>();
            _controller = new ProviderController(_mockProviderService.Object);
        }



        [Fact]
        public async Task GetProviders_ReturnsOkResult_WithListOfProviders()
        {
            // Arrange
            var providerDtos = new List<ProviderDto>
            {
                new ProviderDto { Id = 1, Name = "Provider 1", Nit = "1234567890", Mail = "provider1@example.com" },
                new ProviderDto { Id = 2, Name = "Provider 2", Nit = "0987654321", Mail = "provider2@example.com" }
            };
            _mockProviderService.Setup(service => service.ListAsyncProviders()).ReturnsAsync(providerDtos);

            // Act
            var result = await _controller.GetProviders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ProviderDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
        [Fact]
        public async Task GetProviderById_ReturnsOkResult_WithProvider()
        {
            // Arrange
            var providerDto = new ProviderDto { Id = 1, Name = "Provider 1", Nit = "1234567890", Mail = "provider1@example.com" };
            _mockProviderService.Setup(service => service.GetProviderByIdAsync(1)).ReturnsAsync(providerDto);

            // Act
            var result = await _controller.GetProviderById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProviderDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetProviderById_ReturnsNotFound_WhenProviderDoesNotExist()
        {
            // Arrange
            _mockProviderService.Setup(service => service.GetProviderByIdAsync(1)).ReturnsAsync((ProviderDto)null);

            // Act
            var result = await _controller.GetProviderById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Contains("not found", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task GetProviderCountByCountry_ReturnsOkResult_WithProviderCounts()
        {
            // Arrange
            var providerCounts = new Dictionary<string, int>
            {
                { "Country1", 10 },
                { "Country2", 5 }
            };
            _mockProviderService.Setup(service => service.GetProviderCountByCountryAsync()).ReturnsAsync(providerCounts);

            // Act
            var result = await _controller.GetProviderCountByCountry();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Dictionary<string, int>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Equal(10, returnValue["Country1"]);
            Assert.Equal(5, returnValue["Country2"]);
        }

    }
}
