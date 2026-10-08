using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Repositories
{
    public class SpecialtyRepository : ISpecialityRepository
    {

        private readonly AppDbContext _db;
        public SpecialtyRepository(AppDbContext db)
        {
            _db = db;

        }
        public Task AddSpecialtyAsync(Specialty Sp)
        {
            _db.Specialties.Add(Sp);
            return _db.SaveChangesAsync();
        }

        public Task DeleteSpecialtyAsync(string uuid)
        {
            var sp = _db.Specialties.FirstOrDefault(e => e.Uuid == uuid);
            if (sp != null)
            {
                _db.Specialties.Remove(sp);
                return _db.SaveChangesAsync();
            }


            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Specialty>> GetAllSpecialtyAsync()
        {

            IEnumerable<Specialty> sp = await _db.Specialties.ToListAsync();
            return sp;
        }

        public async Task<Specialty> GetSpecialtyAsyncByIdAsync(string uuid)
        {
            var specialty = await _db.Specialties.FirstOrDefaultAsync(e => e.Uuid == uuid);
            return specialty;
        }

        public Task UpdateSpecialtyAsync(Specialty Sp)
        {
            _db.Specialties.Update(Sp);
            return _db.SaveChangesAsync();
        }
    }
}
