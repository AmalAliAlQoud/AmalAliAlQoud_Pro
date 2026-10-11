using ClinicApplication.Data;
using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using ClinicApplication.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClinicApplication.Controllers
{
    [Authorize]

    public class AppointmentsController : Controller
    {
        //private readonly AppDbContext _db;
        //public AppointmentsController(AppDbContext db)
        //{
        //    _db = db;

        //}

        private readonly IUnitOfWork _unitOfWork;

        public AppointmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        private async Task loadPatients()
        {
            //var Patients = _unitOfWork.Patients.ToList();
            var Patients =await  _unitOfWork.PatientRepository.GetAllPatientAsync();
            SelectList selectListPatient = new SelectList(Patients, "Id", "Name");
            ViewBag.Patients = selectListPatient;

        }

        private async Task loadDoctors()
        {
            var doctors = await _unitOfWork.DoctorRepository.GetAllDoctorAsync();
            SelectList selectList = new SelectList(doctors, "Id", "Name");
            ViewBag.Doctors = selectList;

        }

        public async Task<ActionResult> Index()
        {


             var appsList  = await _unitOfWork.ApointmentRepository.GetAllAppointmentAsyncInclude();
            var apps = appsList.Select(e => new AppointmentDto
            {

                Id = e.Id,
                Uuid = e.Uuid,
                PatientName = e.Patient.Name,
                DoctorName = e.Doctor.Name,
                Date = e.Date,
                Time = e.Time,

            })
     .ToList();

            return View(apps);

        }


        //===============
        //Create
        //==========================

        [HttpGet]
        public async Task<ActionResult> Create()
        {


            await loadDoctors();
            await loadPatients();

            return View();
        }

      
        [HttpPost]
        public async Task<ActionResult> Create(AppointmentCreateDto AppountmentDTO)
        {
            if (ModelState.IsValid)
            {

                //Mapping the properties of AppointmentCreateDto to appointment
                //(PatientId /	DoctorId	/Date	/Time)
                var appointment = new Appointment
                {
                    Patient_ID = AppountmentDTO.Patient_ID,
                    Doctor_ID = AppountmentDTO.Doctor_ID,
                    Date = AppountmentDTO.Date,
                    Time = AppountmentDTO.Time,
                };

                await _unitOfWork.ApointmentRepository.AddAppointmentAsync(appointment);    
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadDoctors();
            await loadPatients();
            return View(AppountmentDTO);

        }
        
        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<ActionResult> Edit(string Uuid)
        { // AppointmentUpdateDto
            Appointment? app = await _unitOfWork.ApointmentRepository.GetAppointmentAsyncByIdAsync(Uuid);
            if (app == null)
            {
                return NotFound();
            }

            var appDto = new AppointmentUpdateDto
            {
                Id = app.Id,
                Uuid = app.Uuid,
                Patient_ID = app.Patient_ID,
              
                Doctor_ID = app.Doctor_ID,
                Date = app.Date,
                Time = app.Time,

            };

            await loadDoctors();
            await loadPatients();


            return View(appDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AppointmentUpdateDto appDto)
        {
            if (ModelState.IsValid)
            {

                var oldApp = await _unitOfWork.ApointmentRepository.GetAppointmentAsyncByIdAsync(appDto.Uuid);

                if (oldApp == null)
                    return NotFound();


               
                  oldApp.Patient_ID = appDto.Patient_ID;
                  oldApp.Doctor_ID = appDto.Doctor_ID;
                  oldApp.Date = appDto.Date;
                  oldApp.Time = appDto.Time;



                // _db.Appointments.Update(app);
                await _unitOfWork.ApointmentRepository.UpdateAppointmentAsync(oldApp);
               
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadDoctors();
            return View(appDto);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public async Task<ActionResult> Delete(string Uuid)
        {
            //Appointment? app = _db.Appointments.Include(d => d.Patient).Include(d => d.Doctor).FirstOrDefault(e => e.Uuid == Uuid);
            Appointment? app = await _unitOfWork.ApointmentRepository.GetAppointmentAsyncByIdAsync(Uuid);

            // var app = _db.Appointments.FirstOrDefault(e => e.Uuid == Uuid);
            if (app == null)
            {
                return NotFound();
            }
            var appDto = new AppointmentUpdateDto
            {
                Id = app.Id,
                Uuid = app.Uuid,
                Patient_ID = app.Patient_ID,
                Doctor_ID = app.Doctor_ID,
                Date = app.Date,
                Time = app.Time,
            };

            await loadDoctors();
            await loadPatients();

            return View(appDto);
        }

        [HttpPost]
        [ActionName("Delete")]
      
        public async Task<ActionResult> DeleteConfirm(AppointmentUpdateDto appDto)
        {
            var app = await _unitOfWork.ApointmentRepository.GetAppointmentAsyncByIdAsync(appDto.Uuid);
            if (app == null)
                return NotFound();


            await _unitOfWork.ApointmentRepository.DeleteAppointmentAsync(appDto.Uuid);
            return RedirectToAction("Index");


        }
    }
}
