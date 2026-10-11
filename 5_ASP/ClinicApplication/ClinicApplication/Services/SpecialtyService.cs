using ClinicApplication.Models;
using ClinicApplication.Repositories.Base;
using ClinicApplication.Services.Base;

namespace ClinicApplication.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialtyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }

        public async Task AddSpecialty(Specialty Sp)
        {
            await _unitOfWork.SpecialityRepository.AddSpecialtyAsync(Sp);
        }

        public async Task DeleteSpecialty(string uuid)
        {
            var sp = await _unitOfWork.SpecialityRepository.GetSpecialtyAsyncByIdAsync(uuid);
            if (sp != null)
            {
                await _unitOfWork.SpecialityRepository.DeleteSpecialtyAsync(uuid);

            }
        }

        public async Task<IEnumerable<Specialty>> GetAllSpecialty()
        {

            IEnumerable<Specialty> sp = await _unitOfWork.SpecialityRepository.GetAllSpecialtyAsync();
            return (sp);
        }

        public async Task<Specialty> GetSpecialtyById(string uuid)
        {
            return await _unitOfWork.SpecialityRepository.GetSpecialtyAsyncByIdAsync(uuid);
        }

        public async Task UpdateSpecialty(Specialty Sp)
        {
            {
                await _unitOfWork.SpecialityRepository.UpdateSpecialtyAsync(Sp);
            }
        }
    }
}
