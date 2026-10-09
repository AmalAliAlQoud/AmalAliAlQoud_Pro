using ClinicApplication.Data;
using ClinicApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Repositories
{
    public class PrescriptionsRepository : IPrescriptionsRepository
    {
        private readonly AppDbContext _db;
        public PrescriptionsRepository(AppDbContext db)
        {
            _db = db;

        }
        public Task AddPrescriptionAsync(Prescription pre)
        {

            _db.Prescriptions.Add(pre);
            return _db.SaveChangesAsync();
        }

        public Task DeletePrescriptionAsync(string uuid)
        {
            var Pre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == uuid);
            if (Pre != null)
            {
                _db.Prescriptions.Remove(Pre);
                return _db.SaveChangesAsync();
            }


            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionAsync()
        {
            IEnumerable<Prescription> Pre = await _db.Prescriptions.ToListAsync();
            return Pre;
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionAsyncInclude()
        {

            IEnumerable<Prescription> Pre = await _db.Prescriptions
                .Include(d => d.Medical_Record)
                .ToListAsync();

            return Pre;
        }

        public async Task<Prescription> GetPrescriptionAsyncByIdAsync(string uuid)
        {
            var Pre = await _db.Prescriptions.FirstOrDefaultAsync(e => e.Uuid == uuid);
            return Pre;
        }

        public async Task<Prescription> GetPrescriptionAsyncByIdAsyncInclude(string uuid)
        {
            var Pre = await _db.Prescriptions
              .Include(d => d.Medical_Record)
              .FirstOrDefaultAsync(e => e.Uuid == uuid);
            return Pre;
        }

        public Task UpdatePrescriptionAsync(Prescription pre)
        {
            _db.Prescriptions.Update(pre);
            return _db.SaveChangesAsync();
        }
    }
}
