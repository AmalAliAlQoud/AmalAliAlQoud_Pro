using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Repositories
{
    public class UserRepository: IUserRepository
    {

        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddUserAsync(User user)
        {
            // throw new NotImplementedException();

            _db.Users.Add(user);
            return _db.SaveChangesAsync();
        }

        public Task DeleteUser(string uuid)
        {
            // throw new NotImplementedException();
            var user = _db.Users.FirstOrDefault(e => e.Uuid == uuid);
            if (user != null)
            {
                _db.Users.Remove(user);
                return _db.SaveChangesAsync();
            }


            return Task.CompletedTask;
        }

        public  async Task<IEnumerable<User>> GetAllUsers()
        {
            //   throw new NotImplementedException();
            IEnumerable<User> users = await _db.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserById(string uuid)
        {
            //  throw new NotImplementedException();
            var user = await _db.Users.FirstOrDefaultAsync(e => e.Uuid == uuid);
            return user;
        }

        public Task UpdateUser(User user)
        {
            //throw new NotImplementedException();
            _db.Users.Update(user);
            return _db.SaveChangesAsync();
        }
    }
}
