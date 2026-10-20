using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Interfaces
{
    public interface IPrescriptionsRepository
    {
        Task<IEnumerable<Prescription>> GetAllPrescriptionAsync();
        Task<IEnumerable<Prescription>> GetAllPrescriptionAsyncInclude();

        Task<Prescription> GetPrescriptionAsyncByIdAsync(string uuid);
        Task<Prescription> GetPrescriptionAsyncByIdAsyncInclude(string uuid);
        Task AddPrescriptionAsync(Prescription pre);
        Task UpdatePrescriptionAsync(Prescription pre);
        Task DeletePrescriptionAsync(string uuid);
    }
}
