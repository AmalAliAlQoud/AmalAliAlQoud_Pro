using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Services.Base
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatient();
        Task<Patient> GetPatientById(string uuid);
        Task AddPatient(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(string uuid);
    }
}
