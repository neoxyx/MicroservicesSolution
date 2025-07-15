using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using UserService.Models.DTOs;
using Xunit;

namespace UserService.IntegrationTests
{
    public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public AuthControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Configuraciones de mock para servicios externos si es necesario
                });
            });
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenValidUser()
        {
            // Arrange
            var newUser = new UserCreateDto
            {
                Username = "testuser_" + Guid.NewGuid().ToString()[..8],
                Email = $"test_{Guid.NewGuid().ToString()[..8]}@example.com",
                Password = "Test123!",
                FirstName = "Test",
                LastName = "User"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", newUser);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<UserResponseDto>();
            Assert.NotNull(result);
            Assert.Equal(newUser.Username, result.Username);
        }
    }
}