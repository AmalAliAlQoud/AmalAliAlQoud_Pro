using ClinicApplication.Application.Dtos;
using ClinicApplication.Domain.Models;

namespace ClinicApplication.Application.Services.Base
{
    public interface IMedical_RecordsService
    {
        Task<IEnumerable<Medical_RecordDto>> GetAllMedical_Record();
        Task<IEnumerable<Medical_RecordDto>> GetAllMedical_RecordDtoInclude();

        Task<Medical_RecordUpdateDto> GetMedical_RecordById(string uuid);
        Task<Doctor> GetMedical_RecordByInclude(string uuid);
        Task AddMedical_Record(Medical_RecordCreateDto MdDto);
        Task UpdateMedical_Record(Medical_RecordUpdateDto MdDto);
        Task DeleteMedical_Record(string uuid);
    }
}
