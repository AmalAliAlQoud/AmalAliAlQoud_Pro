using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Interfaces
{
    public interface IMedical_RecordsRepository
    {
        Task<IEnumerable<Medical_Record>> GetAllMedical_RecordAsync();
        Task<IEnumerable<Medical_Record>> GetAllMedicalRecordAsyncInclude();

        Task<Medical_Record> GetMedicalRecordAsyncByIdAsync(string uuid);
        Task<Medical_Record> GetMedicalRecordAsyncByIdAsyncInclude(string uuid);
        Task AddMedical_RecordAsync(Medical_Record md);
        Task UpdateMedical_RecordAsync(Medical_Record md);
        Task DeleteMedical_RecordAsync(string uuid);
    }
}
