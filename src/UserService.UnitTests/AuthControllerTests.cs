using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using UserService.Controllers;
using UserService.Models.DTOs;
using UserService.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace UserService.UnitTests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ILogger<AuthController>> _loggerMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loggerMock = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_authServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var userDto = new UserCreateDto
            {
                Username = "testuser",
                Password = "Test123!",
                Email = "test@example.com"
            };

            var expectedResult = new UserResponseDto
            {
                Username = "testuser",
                Email = "test@example.com",
                Role = "admin"
            };

            _authServiceMock
                .Setup(s => s.Register(userDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Register(userDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actual = Assert.IsType<UserResponseDto>(okResult.Value);
            Assert.Equal(expectedResult.Username, actual.Username);
            Assert.Equal(expectedResult.Email, actual.Email);
            Assert.Equal(expectedResult.Role, actual.Role);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenInvalidOperationExceptionThrown()
        {
            // Arrange
            var userDto = new UserCreateDto();
            _authServiceMock
                .Setup(s => s.Register(It.IsAny<UserCreateDto>()))
                .ThrowsAsync(new InvalidOperationException("Invalid data"));

            // Act
            var result = await _controller.Register(userDto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Invalid data", badRequest.Value.ToString());
        }

        [Fact]
        public async Task Register_ReturnsServerError_WhenUnhandledExceptionThrown()
        {
            // Arrange
            var userDto = new UserCreateDto();
            _authServiceMock
                .Setup(s => s.Register(It.IsAny<UserCreateDto>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.Register(userDto);

            // Assert
            var serverError = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, serverError.StatusCode);
        }
    }
}
