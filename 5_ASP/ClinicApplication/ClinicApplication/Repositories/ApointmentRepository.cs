using ClinicApplication.Data;
using ClinicApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Repositories
{
    public class ApointmentRepository : IApointmentRepository
    {
        private readonly AppDbContext _db;
        public ApointmentRepository(AppDbContext db)
        {
            _db = db;

        }
        public Task AddAppointmentAsync(Appointment app)
        {
            // throw new NotImplementedException();
            _db.Appointments.Add(app);
            return _db.SaveChangesAsync();
        }

        public Task DeleteAppointmentAsync(string uuid)
        {
            // throw new NotImplementedException();
            var app = _db.Appointments.FirstOrDefault(e => e.Uuid == uuid);
            if (app != null)
            {
                _db.Appointments.Remove(app);
                return _db.SaveChangesAsync();
            }


            return Task.CompletedTask;
        }

        public async  Task<IEnumerable<Appointment>> GetAlAppointmentAsync()
        {
            // throw new NotImplementedException();
            IEnumerable<Appointment> appointments = await _db.Appointments.ToListAsync();
            return appointments;
        }

        public async Task<Appointment> GetAppointmentAsyncByIdAsync(string uuid)
        {
            //throw new NotImplementedException();
            var app = await _db.Appointments
                  .Include(p => p.Patient).Include(d => d.Doctor)
               .FirstOrDefaultAsync(e => e.Uuid == uuid);
            return app;

        }

        public async Task<Appointment> GetAppointmentAsyncByIdAsyncInclude(string uuid)
        {
            var app = await _db.Appointments
                 .Include(p => p.Patient).Include(d => d.Doctor)
              .FirstOrDefaultAsync(e => e.Uuid == uuid);
            return app;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentAsyncInclude()
        {
            // throw new NotImplementedException();
            IEnumerable<Appointment> appointments = await _db.Appointments
              .Include(p => p.Patient).Include(d => d.Doctor)
              .ToListAsync();

            return appointments;
        }

        public Task UpdateAppointmentAsync(Appointment app)
        {
            _db.Appointments.Update(app);
            return _db.SaveChangesAsync();
        }
    }
}
