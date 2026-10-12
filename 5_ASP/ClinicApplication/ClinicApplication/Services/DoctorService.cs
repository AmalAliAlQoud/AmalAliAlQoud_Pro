using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using ClinicApplication.Repositories.Base;
using ClinicApplication.Services.Base;

namespace ClinicApplication.Services
{
    public class DoctorService: IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task AddDoctor(DoctorCreateDto drDto)
        {
            var dr = new Doctor
            {

                Name = drDto.Name,
                Specialty_ID = drDto.SpecialtyId,
                Phone = drDto.Phone


            };

            await _unitOfWork.DoctorRepository.AddDoctorAsync(dr);
              
        }

        public async Task DeleteDoctor(string uuid)
        {
            var dr = await _unitOfWork.DoctorRepository.GetDoctorAsyncByIdAsync(uuid);
            if (dr != null)
            {
                await _unitOfWork.DoctorRepository.DeleteDoctorAsync(uuid);

            }

        }

        public async  Task<IEnumerable<DoctorDto>> GetAllDoctor()
        {


            var doctors = await _unitOfWork.DoctorRepository
                .GetAllDoctorAsyncInclude();
            return doctors.Select(e => new DoctorDto
            {
                Id = e.Id,
                Uuid = e.Uuid,
                Name = e.Name,
                SpecialtyName = e.Specialty.Name,

                Phone = e.Phone
            });
        }

        public Task<IEnumerable<DoctorDto>> GetAllDoctorInclude()
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorUpdateDto> GetDoctorById(string uuid)
        {
            // throw new NotImplementedException();
            var dr = await _unitOfWork.DoctorRepository.GetDoctorAsyncByIdAsync(uuid);
             

            if (dr == null)
            {
                return null;
            }

            return  new DoctorUpdateDto
            {
                Id = dr.Id,
                Uuid = dr.Uuid,
                Name = dr.Name,
                SpecialtyId = dr.Specialty_ID,
                Phone = dr.Phone
            };
        }

        public Task<Doctor> GetDoctorByInclude(string uuid)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateDoctor(DoctorUpdateDto drDto)
        {
            var existingDoctor = await _unitOfWork.DoctorRepository.GetDoctorAsyncByIdAsync(drDto.Uuid);
            if (existingDoctor != null)
            {
                existingDoctor.Name = drDto.Name;
                existingDoctor.Specialty_ID = drDto.SpecialtyId;
                existingDoctor.Phone = drDto.Phone;
                await _unitOfWork.DoctorRepository.UpdateDoctorAsync(existingDoctor);
            }
                
            }
    }
}
