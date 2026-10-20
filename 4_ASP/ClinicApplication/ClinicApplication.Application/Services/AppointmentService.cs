using ClinicApplication.Application.Dtos;
using ClinicApplication.Domain.Models;
using ClinicApplication.Application.Interfaces.Base;
using ClinicApplication.Application.Services.Base;

namespace ClinicApplication.Application.Services
{
    public class AppointmentService: IAppointmentService //

    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task AddApp(AppointmentCreateDto appDto)
        {
            var app = new Appointment
            {

                Patient_ID = appDto.Patient_ID,
                Doctor_ID = appDto.Doctor_ID,
                Date = appDto.Date,
                Time = appDto.Time,


            };

            await _unitOfWork.ApointmentRepository.AddAppointmentAsync(app);
        }

        public async Task DeleteApp(string uuid)
        {
            var app = await _unitOfWork.ApointmentRepository.GetAppointmentAsyncByIdAsync(uuid);
            if (app != null)
            {
                await _unitOfWork.ApointmentRepository.DeleteAppointmentAsync(uuid);

            }
        }

        public Task<IEnumerable<AppointmentDto>> GetAllAppInclude()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllApps()
        {
      

        var apps = await _unitOfWork.ApointmentRepository.GetAllAppointmentAsyncInclude();
            return apps.Select(e => new AppointmentDto
            {
                Id = e.Id,
                Uuid = e.Uuid,
                PatientName = e.Patient.Name, ////PatientName DoctorName
                DoctorName = e.Doctor.Name,
                Date = e.Date,
                Time = e.Time
            });
        }

        public Task<AppointmentDto> GetAppByInclude(string uuid)
        {
            throw new NotImplementedException();
        }

        public async Task<AppointmentUpdateDto> GetApprById(string uuid)
        {
            var app = await _unitOfWork.ApointmentRepository.GetAppointmentAsyncByIdAsync(uuid);


            if (app == null)
            {
                return null;
            }

            return new AppointmentUpdateDto// 
            {
                Id = app.Id,
                Uuid = app.Uuid,
                Patient_ID = app.Patient_ID,
                Doctor_ID = app.Doctor_ID,
                Date = app.Date,
                Time = app.Time
            };
        }

        public async Task UpdateApp(AppointmentUpdateDto app)
        {
            var existingApp = await _unitOfWork.ApointmentRepository.GetAppointmentAsyncByIdAsync(app.Uuid);
            if (existingApp != null) //(PatientId /	DoctorId	/Date	/Time) Id Uuid
            {
                existingApp.Patient_ID = app.Patient_ID;
                existingApp.Doctor_ID = app.Doctor_ID;
                existingApp.Date = app.Date;
                existingApp.Time = app.Time;

                await _unitOfWork.ApointmentRepository.UpdateAppointmentAsync(existingApp);
            }
        }
    }
}
