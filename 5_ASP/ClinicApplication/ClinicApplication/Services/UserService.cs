using ClinicApplication.Models;
using ClinicApplication.Repositories.Base;
using ClinicApplication.Services.Base;

namespace ClinicApplication.Services
{
    public class UserService: IUserService
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }

        public async Task AddUser(User user)
        {
            // throw new NotImplementedException();

            await _unitOfWork.UserRepository.AddUserAsync(user);
        }

        public async Task DeleteUser(string uuid)
        {
            // throw new NotImplementedException();
            var user = await _unitOfWork.UserRepository.GetUserById(uuid);
            if (user != null)
            {
                await _unitOfWork.UserRepository.DeleteUser(uuid);

            }
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            //throw new NotImplementedException();
            IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllUsers();
            return (users);
        }

        public async  Task<User> GetUserById(string uuid)
        {
            //throw new NotImplementedException();
            return await _unitOfWork.UserRepository.GetUserById(uuid);

        }

        public async Task UpdateUser(User user)
        {
            await _unitOfWork.UserRepository.UpdateUser(user);
        }
    }
}
