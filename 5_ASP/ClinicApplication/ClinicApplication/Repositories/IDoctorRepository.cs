using ClinicApplication.Dtos;
using ClinicApplication.Models;

namespace ClinicApplication.Repositories
{
    public interface IDoctorRepository
    {

        Task<IEnumerable<Doctor>> GetAllDoctorAsync();
        Task<IEnumerable<Doctor>> GetAllDoctorAsyncInclude();
        
        Task<Doctor> GetDoctorAsyncByIdAsync(string uuid);
        Task<Doctor> GetDoctorAsyncByIdAsyncInclude(string uuid);
        Task AddDoctorAsync(Doctor dr);
        Task UpdateDoctorAsync(Doctor dr);
        Task DeleteDoctorAsync(string uuid);
    }
}
