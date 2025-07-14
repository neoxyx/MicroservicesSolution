using UserService.Models.Entities;

namespace UserService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<IEnumerable<User>> GetAllUsers();
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User> GetUserByEmail(string email);
    }
}