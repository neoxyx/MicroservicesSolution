using UserService.Models.DTOs;
using UserService.Models.Entities;

namespace UserService.Extensions
{
    public static class MappingExtensions
    {
        public static UserResponseDto ToUserResponseDto(this User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public static User ToUserEntity(this UserCreateDto userCreateDto, IPasswordHasher passwordHasher)
        {
            return new User
            {
                Username = userCreateDto.Username,
                Email = userCreateDto.Email,
                PasswordHash = passwordHasher.HashPassword(userCreateDto.Password),
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Role = Enum.Parse<UserRole>(userCreateDto.Role)
            };
        }
    }
}