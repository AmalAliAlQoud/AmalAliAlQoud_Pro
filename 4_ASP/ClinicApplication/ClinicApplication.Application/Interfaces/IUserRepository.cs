using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Interfaces
{
    public interface IUserRepository
    {
        //##########################################
        User? GetByUsername(string username);


        User? GetByEmail(string email);
        void Add(User user);
        //##########################################
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string uuid);
        Task AddUserAsync(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string uuid);
    }
}
