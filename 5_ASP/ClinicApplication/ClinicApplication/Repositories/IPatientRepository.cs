using ClinicApplication.Models;

namespace ClinicApplication.Repositories
{
    public interface IPatientRepository 
    {
        Task<IEnumerable<Patient>> GetAllPatient();
        Task<Patient> GetPatientById(string uuid);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(string uuid);
    }
}
