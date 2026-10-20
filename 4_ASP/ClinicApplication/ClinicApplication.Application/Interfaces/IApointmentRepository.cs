using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Interfaces
{
    public interface IApointmentRepository
    {

        Task<IEnumerable<Appointment>> GetAlAppointmentAsync();
        Task<IEnumerable<Appointment>> GetAllAppointmentAsyncInclude();

        Task<Appointment> GetAppointmentAsyncByIdAsync(string uuid);
        Task<Appointment> GetAppointmentAsyncByIdAsyncInclude(string uuid);
        Task AddAppointmentAsync(Appointment app);
        Task UpdateAppointmentAsync(Appointment app);
        Task DeleteAppointmentAsync(string uuid);
    }
}
