using UserService.Models.DTOs;

namespace UserService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseDto> Register(UserCreateDto userCreateDto);
        Task<string> Login(LoginDto loginDto);
        Task<string> GeneratePasswordResetToken(string email);
    }
}