using ClinicApplication.Application.Dtos;
using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Services.Base
{
    public interface IPrescriptionsService
    {
        Task<IEnumerable<PrescriptionDto>> GetAllPrescription();
        Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionInclude();

        Task<PrescriptionUpdateDto> GetPrescriptionById(string uuid);
        Task<PrescriptionDto> GetPrescriptionByInclude(string uuid);
        Task AddPrescription(PrescriptionCreateDto preDto);
        Task UpdatePrescription(PrescriptionUpdateDto PreDto);
        Task DeletePrescription(string uuid);
    }
}
