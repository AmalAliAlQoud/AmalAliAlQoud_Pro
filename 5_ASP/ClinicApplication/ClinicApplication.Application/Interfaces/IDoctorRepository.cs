using ClinicApplication.Application.Dtos;
using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Interfaces
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
