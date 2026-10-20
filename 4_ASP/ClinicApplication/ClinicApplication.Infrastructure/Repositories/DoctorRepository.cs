using ClinicApplication.Infrastructure.Data;
using ClinicApplication.Application.Dtos;
//using ClinicApplication.Migrations;
using ClinicApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ClinicApplication.Application.Interfaces;

namespace ClinicApplication.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository 
    {

        private readonly AppDbContext _db;
        public DoctorRepository(AppDbContext db)
        {
            _db = db;

        }
        public Task AddDoctorAsync(Doctor dr)
        {
            _db.Doctors.Add(dr);
            return _db.SaveChangesAsync();
        }

        public Task DeleteDoctorAsync(string uuid)
        {
            var dr = _db.Doctors.FirstOrDefault(e => e.Uuid == uuid);
            if (dr != null)
            {
                _db.Doctors.Remove(dr);
                return _db.SaveChangesAsync();
            }


            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorAsync()
        {
            IEnumerable<Doctor> doctors = await _db.Doctors.ToListAsync();
            return doctors;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorAsyncInclude()
        {
            IEnumerable<Doctor> doctors = await _db.Doctors
                .Include(d => d.Specialty) 
                .ToListAsync();

            return doctors;
        }

        public async Task<Doctor> GetDoctorAsyncByIdAsync(string uuid)
        {
            var doctor = await _db.Doctors.FirstOrDefaultAsync(e => e.Uuid == uuid);
            return doctor;
        }

        public async Task<Doctor?> GetDoctorAsyncByIdAsyncInclude(string uuid) 
        {
            var doctor = await _db.Doctors
                .Include(d => d.Specialty) 
                .FirstOrDefaultAsync(e => e.Uuid == uuid);
            return doctor;
        }

        public Task UpdateDoctorAsync(Doctor dr)
        {
            _db.Doctors.Update(dr);
            return _db.SaveChangesAsync();
        }
    }
}
