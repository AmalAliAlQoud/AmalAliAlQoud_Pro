using ClinicApplication.Dtos;
using ClinicApplication.Models;

namespace ClinicApplication.Services.Base
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctor();
        Task<IEnumerable<DoctorDto>> GetAllDoctorInclude();

        Task<DoctorUpdateDto> GetDoctorById(string uuid);
        Task<Doctor> GetDoctorByInclude(string uuid);
        Task AddDoctor(DoctorCreateDto drDto);
        Task UpdateDoctor(DoctorUpdateDto dr);
        Task DeleteDoctor(string uuid);
    }
}
