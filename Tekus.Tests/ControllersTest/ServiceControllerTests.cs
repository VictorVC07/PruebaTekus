using Microsoft.AspNetCore.Mvc;
using Moq;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;
using Tekus.WebApi.Controllers;
using Xunit;

namespace Tekus.Tests.ControllersTest
{
    public class ServiceControllerTests
    {
        private readonly Mock<IServicesService> _mockServicesService;
        private readonly ServiceController _controller;

        public ServiceControllerTests()
        {
            _mockServicesService = new Mock<IServicesService>();
            _controller = new ServiceController(_mockServicesService.Object);
        }

        [Fact]
        public async Task ListServices_ReturnsOkResult_WithListOfServices()
        {
            // Arrange
            var serviceDtos = new List<ServiceDto>
            {
                new ServiceDto { Id = 1, Name = "Service 1" },
                new ServiceDto { Id = 2, Name = "Service 2" }
            };
            _mockServicesService.Setup(service => service.GetAllServicesAsync()).ReturnsAsync(serviceDtos);

            // Act
            var result = await _controller.ListServices();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ServiceDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task CreateService_ReturnsCreatedAtActionResult_WithCreatedService()
        {
            // Arrange
            var serviceDto = new ServiceDto { Name = "New Service"};
            var createdServiceDto = new ServiceDto { Id = 3, Name = "New Service"};
            _mockServicesService.Setup(service => service.CreateServiceAsync(serviceDto)).ReturnsAsync(createdServiceDto);

            // Act
            var result = await _controller.CreateService(serviceDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<ServiceDto>(createdAtActionResult.Value);
            Assert.Equal(3, returnValue.Id);
            Assert.Equal("New Service", returnValue.Name);
        }

        [Fact]
        public async Task CreateService_ReturnsBadRequest_WhenServiceDtoIsNull()
        {
            // Act
            var result = await _controller.CreateService(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedService()
        {
            // Arrange
            var serviceDto = new ServiceDto { Id = 1, Name = "Updated Service"};
            _mockServicesService.Setup(service => service.UpdateServiceAsync(serviceDto)).ReturnsAsync(serviceDto);

            // Act
            var result = await _controller.Update(serviceDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ServiceDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Updated Service", returnValue.Name);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenServiceDoesNotExist()
        {
            // Arrange
            var serviceDto = new ServiceDto { Id = 1, Name = "Updated Service"};
            _mockServicesService.Setup(service => service.UpdateServiceAsync(serviceDto)).ThrowsAsync(new KeyNotFoundException("Service not found"));

            // Act
            var result = await _controller.Update(serviceDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Service not found", notFoundResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenServiceDtoIsNull()
        {
            // Act
            var result = await _controller.Update(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetServiceCountByCountry_ReturnsOkResult_WithServiceCounts()
        {
            // Arrange
            var serviceCounts = new Dictionary<string, int>
            {
                { "Country1", 10 },
                { "Country2", 5 }
            };
            _mockServicesService.Setup(service => service.GetServiceCountByCountryAsync()).ReturnsAsync(serviceCounts);

            // Act
            var result = await _controller.GetServiceCountByCountry();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Dictionary<string, int>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Equal(10, returnValue["Country1"]);
            Assert.Equal(5, returnValue["Country2"]);
        }
    }
}
