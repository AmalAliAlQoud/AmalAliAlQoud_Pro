using ClinicApplication.Application.Dtos;
using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Services.Base
{
    public interface IUserService
    {

        //####################################
        User? Login(LoginDto loginDto);

        User? Register(RegisterDto registerDto);
        //####################################
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(string uuid);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string uuid);
    }
}
