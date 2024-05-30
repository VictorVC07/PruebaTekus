using Microsoft.Extensions.Configuration;
using Moq;
using Tekus.Application.Dtos;
using Tekus.Application.Services;
using Xunit;

namespace Tekus.Tests.ServicesTest
{
    public class LoginServiceTests
    {

        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly LoginService _loginService;

        public LoginServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(config => config["Jwt:secretPassword"]).Returns("example+secret+key+with+more+of+32+chars");
            _mockConfiguration.Setup(config => config["Jwt:Issuer"]).Returns("issuer_example");
            _mockConfiguration.Setup(config => config["Jwt:Audience"]).Returns("audience_example");

            _loginService = new LoginService(_mockConfiguration.Object);
        }

        [Fact]
        public async Task Login_ReturnsToken_WhenCredentialsAreValid()
        {
            // Arrange
            var userDto = new UserDto { user = "tekus", password = "12345" };

            // Act
            var result = await _loginService.Login(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task Login_ReturnsNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var userDto = new UserDto { user = "tekus", password = "wrong_password" };

            // Act
            var result = await _loginService.Login(userDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GenerateJWT_ReturnsToken_WithValidConfiguration()
        {
            // Act
            var result = _loginService.GenerateJWT();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

    }
}
