using UserService.Models.DTOs;
using UserService.Models.Entities;

namespace UserService.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserById(int id);
        Task<IEnumerable<UserResponseDto>> GetAllUsers();
        Task<bool> DeleteUser(int id);
        Task<UserResponseDto> ChangeUserRole(int id, string newRole);
    }
}