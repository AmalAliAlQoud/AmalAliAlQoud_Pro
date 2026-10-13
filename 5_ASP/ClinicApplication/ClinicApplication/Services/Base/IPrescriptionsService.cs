using ClinicApplication.Dtos;
using ClinicApplication.Models;

namespace ClinicApplication.Services.Base
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
