using Microsoft.EntityFrameworkCore;
using UserService.Models.DTOs;
using UserService.Models.Entities;
using UserService.Repositories.Interfaces;
using UserService.Services.Interfaces;
using UserService.Extensions;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<UserResponseDto> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return user.ToUserResponseDto();
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users.Select(u => u.ToUserResponseDto());
        }

        public async Task<bool> DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
            _logger.LogInformation("User {UserId} deleted successfully", id);
            return true;
        }

        public async Task<UserResponseDto> ChangeUserRole(int id, string newRole)
        {
            if (!Enum.TryParse<UserRole>(newRole, out var role))
                throw new ArgumentException("Invalid role specified");

            var user = await _userRepository.GetUserById(id);
            user.Role = role;

            await _userRepository.UpdateUser(user);
            _logger.LogInformation("Role changed to {NewRole} for user {UserId}", newRole, id);

            return user.ToUserResponseDto();
        }
    }
}