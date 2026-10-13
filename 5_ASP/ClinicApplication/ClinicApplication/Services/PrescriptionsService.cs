using ClinicApplication.Dtos;
using ClinicApplication.Models;
using ClinicApplication.Repositories.Base;
using ClinicApplication.Services.Base;

namespace ClinicApplication.Services
{
    public class PrescriptionsService: IPrescriptionsService
    {

        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }

        public async Task AddPrescription(PrescriptionCreateDto PreDto)
        {

            var Pre = new Prescription
            {
                mdRecord_ID = PreDto.mdRecord_ID,
                Medicine_Name = PreDto.Medicine_Name,
                Dosage = PreDto.Dosage,


            };

            await _unitOfWork.PrescriptionsRepository.AddPrescriptionAsync(Pre);
        }

        public async Task DeletePrescription(string uuid)
        {
            //throw new NotImplementedException();
            var pre = await _unitOfWork.PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(uuid);
            if (pre != null)
            {
                await _unitOfWork.PrescriptionsRepository.DeletePrescriptionAsync(uuid);

            }
        }

        public async Task<IEnumerable<PrescriptionDto>> GetAllPrescription()
        {
            var Pre = await _unitOfWork.PrescriptionsRepository
                .GetAllPrescriptionAsyncInclude();
            return Pre.Select(e => new PrescriptionDto
            {
                Id = e.Id,
                Uuid = e.Uuid,
                mdRecord_ID = e.Medical_Record != null ? e.Medical_Record.Id : e.Medical_Record.Id,
                Medicine_Name = e.Medicine_Name,
                Dosage = e.Dosage
            });
        }

        public Task<IEnumerable<PrescriptionDto>> GetAllPrescriptionInclude()
        {
            throw new NotImplementedException();
        }

        public async Task<PrescriptionUpdateDto> GetPrescriptionById(string uuid)
        {
            // throw new NotImplementedException();
            var Pre = await _unitOfWork.PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(uuid);


            if (Pre == null)
            {
                return null;
            }

            return new PrescriptionUpdateDto
            {
                Id = Pre.Id,
                Uuid = Pre.Uuid,
                mdRecord_ID = Pre.mdRecord_ID,
                Medicine_Name = Pre.Medicine_Name,
                Dosage = Pre.Dosage,
            };
        }

        public Task<PrescriptionDto> GetPrescriptionByInclude(string uuid)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePrescription(PrescriptionUpdateDto PreDto)
        {
            // throw new NotImplementedException();
            var OldPre = await _unitOfWork.PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(PreDto.Uuid);
            if (OldPre != null)
            {
                
                OldPre.Medicine_Name = PreDto.Medicine_Name;
                OldPre.mdRecord_ID = PreDto.mdRecord_ID;
                OldPre.Dosage = PreDto.Dosage;

                await _unitOfWork.PrescriptionsRepository.UpdatePrescriptionAsync(OldPre);
            }
        }
    }
}
