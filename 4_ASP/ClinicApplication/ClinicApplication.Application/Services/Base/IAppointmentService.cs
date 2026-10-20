using ClinicApplication.Application.Dtos;
using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Services.Base
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllApps();
        Task<IEnumerable<AppointmentDto>> GetAllAppInclude();

        Task<AppointmentUpdateDto> GetApprById(string uuid);
        Task<AppointmentDto> GetAppByInclude(string uuid);
        Task AddApp(AppointmentCreateDto appDto);
        Task UpdateApp(AppointmentUpdateDto app);
        Task DeleteApp(string uuid);
    }
}
