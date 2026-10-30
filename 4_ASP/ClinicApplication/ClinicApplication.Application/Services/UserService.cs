using BCrypt.Net;
using ClinicApplication.Application.Dtos;
using ClinicApplication.Application.Interfaces.Base;
using ClinicApplication.Application.Services.Base;
using ClinicApplication.Domain.Models;



namespace ClinicApplication.Application.Services
{
    public class UserService: IUserService
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }
        public async Task<IEnumerable<User>> GetAllUser()
        {
            //throw new NotImplementedException();
            IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllUsers();
            return (users);
        }

        //###########################################
        public User? Login(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepository.GetByUsername(loginDto.Username);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }


        public User? Register(RegisterDto registerDto)
        {
            var existingUserByUsername = _unitOfWork.UserRepository.GetByUsername(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return null;
            }
            var existingUserByEmail = _unitOfWork.UserRepository.GetByEmail(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return null;
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new User
            {
                Name = registerDto.Name,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword
            };
            _unitOfWork.UserRepository.Add(newUser);

            return newUser;
        }
        //###########################################

        public async Task UpdateUser(User user)
        {
            await _unitOfWork.UserRepository.UpdateUser(user);
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

       

        public async  Task<User> GetUserById(string uuid)
        {
            //throw new NotImplementedException();
            return await _unitOfWork.UserRepository.GetUserById(uuid);

        }

       

       
    }
}
