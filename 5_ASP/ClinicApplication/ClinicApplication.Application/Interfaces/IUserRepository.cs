using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string uuid);
        Task AddUserAsync(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string uuid);
    }
}
