using ClinicApplication.Application.Dtos;
using ClinicApplication.Domain.Models;
using ClinicApplication.Application.Interfaces.Base;
using ClinicApplication.Application.Services.Base;

namespace ClinicApplication.Application.Services
{
    public class Medical_RecordsService: IMedical_RecordsService
    {

        private readonly IUnitOfWork _unitOfWork;

        public Medical_RecordsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }

        public async Task AddMedical_Record(Medical_RecordCreateDto MdDto)
        {
            // throw new NotImplementedException();
            var Md = new Medical_Record
            {

                Patient_ID = MdDto.Patient_ID,
                Doctor_ID = MdDto.Doctor_ID,
                Diagnosis = MdDto.Diagnosis,
                Notes = MdDto.Notes,


            };

            await _unitOfWork.MMdical_RecordsRepository.AddMedical_RecordAsync(Md);
        }

        public async Task DeleteMedical_Record(string uuid)
        {
            // throw new NotImplementedException();
            var md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(uuid);
            if (md != null)
            {
                await _unitOfWork.MMdical_RecordsRepository.DeleteMedical_RecordAsync(uuid);

            }
        }

        public async Task<IEnumerable<Medical_RecordDto>> GetAllMedical_Record()
        {
            var md = await _unitOfWork.MMdical_RecordsRepository.GetAllMedicalRecordAsyncInclude();
            return md.Select(e => new Medical_RecordDto
            {
                Id = e.Id,
                Uuid = e.Uuid,
                PatientName = e.Patient.Name,
                DoctorName = e.Doctor.Name,
                Diagnosis = e.Diagnosis,
                Notes = e.Notes,
            });
        }

        public Task<IEnumerable<Medical_RecordDto>> GetAllMedical_RecordDtoInclude()
        {
            throw new NotImplementedException();
        }

        public async  Task<Medical_RecordUpdateDto> GetMedical_RecordById(string uuid)
        {
            // var md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(uuid);
            
                var md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsyncInclude(uuid);


            if (md == null)
            {
                return null;
            }

            return new Medical_RecordUpdateDto// 
            {
                Id = md.Id,
                Uuid = md.Uuid,
                Doctor_ID = md.Doctor_ID,
                Patient_ID = md.Patient_ID,
                Diagnosis = md.Diagnosis,
                Notes = md.Notes,
            };
        }

        public Task<Doctor> GetMedical_RecordByInclude(string uuid)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMedical_Record(Medical_RecordUpdateDto MdDto)
        {
            var existingMd = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(MdDto.Uuid);
            if (existingMd != null) //(PatientId /	DoctorId	/Date	/Time) Id Uuid
            {
                existingMd.Doctor_ID = MdDto.Doctor_ID;
                existingMd.Patient_ID = MdDto.Patient_ID;
                existingMd.Diagnosis = MdDto.Diagnosis;
                existingMd.Notes = MdDto.Notes;

                await _unitOfWork.MMdical_RecordsRepository.UpdateMedical_RecordAsync(existingMd);
            }
        }
    }
}
