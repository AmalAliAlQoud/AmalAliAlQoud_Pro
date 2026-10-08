using ClinicApplication.Models;

namespace ClinicApplication.Repositories
{
    public interface IPatientRepository 
    {
        Task<IEnumerable<Patient>> GetAllPatientAsync();
        Task<Patient> GetPatientAsyncByIdAsync(string uuid);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(string uuid);
    }
}
