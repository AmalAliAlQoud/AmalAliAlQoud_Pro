using ClinicApplication.Migrations;
using ClinicApplication.Models;
using ClinicApplication.Repositories.Base;
using ClinicApplication.Services.Base;

namespace ClinicApplication.Services
{
    public class PatientService: IPatientService
    {
        //جبته من الكنترولر 
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }

        public async Task AddPatient(Patient patient)
        {
            //  throw new NotImplementedException();
            await _unitOfWork.PatientRepository.AddPatientAsync(patient);
        }

        public async Task DeletePatient(string uuid)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientById(uuid);
            if (patient != null)
            {
                await _unitOfWork.PatientRepository.DeletePatient(uuid);
             
            }


         
        }

        public async Task<IEnumerable<Patient>> GetAllPatient()
        {
           
            IEnumerable<Patient> patients = await _unitOfWork.PatientRepository.GetAllPatient();
            return (patients);
        }

        //public async Task<Patient> GetPatientAsyncById(string uuid)
        //{
        //   // throw new NotImplementedException();
        //   return await _unitOfWork.PatientRepository.GetPatientById(uuid);
        //}

        public async Task<Patient> GetPatientById(string uuid)
        {
            //throw new NotImplementedException();
            return await _unitOfWork.PatientRepository.GetPatientById(uuid);
        }

        public async Task UpdatePatient(Patient patient)
        {
           await  _unitOfWork.PatientRepository.UpdatePatient(patient);
           // return _db.SaveChangesAsync();
        }
        //------------------------


    }
}
