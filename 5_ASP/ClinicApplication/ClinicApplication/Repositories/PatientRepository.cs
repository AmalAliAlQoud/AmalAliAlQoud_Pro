using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _db;
        public PatientRepository(AppDbContext db)
        {
            _db = db;

        }

        public Task AddPatientAsync(Patient patient) 
        {
            _db.Patients.Add(patient);
            return _db.SaveChangesAsync();
        }

        public Task DeletePatient(string Uuid) 
        {
          
            var patient = _db.Patients.FirstOrDefault(e => e.Uuid == Uuid);
            if (patient != null)
            {
                _db.Patients.Remove(patient); 
                return _db.SaveChangesAsync();
            }
        

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Patient>> GetAllPatient()
        {
            IEnumerable<Patient> patients = await _db.Patients.ToListAsync();
            return patients;
        }

        public async Task<Patient?> GetPatientById(string Uuid)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(e => e.Uuid == Uuid);
            return patient;
        }

        public Task UpdatePatient(Patient patient)
        {
            _db.Patients.Update(patient);
            return _db.SaveChangesAsync();
        }
    }
}
