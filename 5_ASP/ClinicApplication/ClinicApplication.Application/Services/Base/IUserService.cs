using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Services.Base
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(string uuid);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string uuid);
    }
}
