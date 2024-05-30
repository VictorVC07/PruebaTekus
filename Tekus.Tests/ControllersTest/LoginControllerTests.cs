using Microsoft.AspNetCore.Mvc;
using Moq;
using Tekus.Application.Dtos;
using Tekus.Application.Interfaces;
using Tekus.WebApi.Controllers;
using Xunit;

namespace Tekus.Tests.ControllersTest
{
    public class LoginControllerTests
    {
        private readonly Mock<ILoginService> _mockLoginService;
        private readonly LoginController _controller;

        private const string ValidUsername = "tekus";
        private const string ValidPassword = "1234";
        private const string ValidToken = "12345ASDFJ123456";
        private const string InvalidPassword = "lskdas";

        public LoginControllerTests()
        {
            _mockLoginService = new Mock<ILoginService>();
            _controller = new LoginController(_mockLoginService.Object);
        }


        [Fact]
        public async Task Login_ReturnsOkResult_WithToken()
        {
            // Arrange
            var userDto = new UserDto { user = ValidUsername, password = ValidPassword };

            _mockLoginService.Setup(servicio => servicio.Login(It.IsAny<UserDto>()))
                           .ReturnsAsync(ValidToken);

            // Act
            var result = await _controller.Login(userDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, (okResult as OkObjectResult).StatusCode);
            var returnValue = okResult.Value;

            var tokenProperty = returnValue.GetType().GetProperty("Token");
            Assert.NotNull(tokenProperty);
            var tokenValue = tokenProperty.GetValue(returnValue, null);

            Assert.NotNull(tokenValue);
            Assert.Equal(ValidToken, tokenValue);

        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenInvalidCredentials()
        {
            // Arrange
            var userDto = new UserDto { user = ValidUsername, password = InvalidPassword };
            _mockLoginService.Setup(service => service.Login(userDto))
                             .ReturnsAsync((string)null);

            // Act
            var result = await _controller.Login(userDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, (unauthorizedResult as UnauthorizedResult).StatusCode);
        }
    }
}