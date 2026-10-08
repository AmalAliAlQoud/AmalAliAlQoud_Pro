using ClinicApplication.Data;
using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
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
        private readonly AppDbContext _db;
        public AppointmentsController(AppDbContext db)
        {
            _db = db;

        }


        private void loadPatients()
        {
            var Patients = _db.Patients.ToList();
            SelectList selectListPatient = new SelectList(Patients, "Id", "Name");
            ViewBag.Patients = selectListPatient;

        }

        private void loadDoctors()
        {
            var doctors = _db.Doctors.ToList();
            SelectList selectList = new SelectList(doctors, "Id", "Name");
            ViewBag.Doctors = selectList;

        }

        public  ActionResult Index()
        {

            IEnumerable<AppointmentDto> Appointments = _db.Appointments.Select(e => new AppointmentDto {
                //mappingh from appointment to AppointmentDto

                Id = e.Id,
                Uuid=e.Uuid,
                PatientName =e.Patient != null ? e.Patient.Name : null,
                DoctorName = e.Doctor != null ? e.Doctor.Name : null,
                Date = e.Date,
                Time=e.Time,

                 }).ToList();

            return View(Appointments);

        }


        //===============
        //Create
        //==========================

        [HttpGet]
        public ActionResult Create()
        {


            loadDoctors();
            loadPatients();

            return View();
        }

      
        [HttpPost]
        public ActionResult Create(AppointmentCreateDto AppountmentDTO)
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

                _db.Appointments.Add(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            loadDoctors();
            loadPatients();
            return View(AppountmentDTO);

        }
        
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(string Uuid)
        { // AppointmentUpdateDto
            Appointment? app = _db.Appointments.FirstOrDefault(e => e.Uuid == Uuid);
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

            loadDoctors();
            loadPatients();


            return View(appDto);
        }

        [HttpPost]
        public ActionResult Edit(AppointmentUpdateDto appDto)
        {
            if (ModelState.IsValid)
            {

                var oldApp = _db.Appointments.FirstOrDefault(e => e.Uuid == appDto.Uuid);

                if (oldApp == null)
                    return NotFound();


               
                  oldApp.Patient_ID = appDto.Patient_ID;
                  oldApp.Doctor_ID = appDto.Doctor_ID;
                  oldApp.Date = appDto.Date;
                  oldApp.Time = appDto.Time;
                  


                // _db.Appointments.Update(app);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            loadDoctors();
            return View(appDto);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(string Uuid)
        {
            Appointment? app = _db.Appointments.Include(d => d.Patient).Include(d => d.Doctor).FirstOrDefault(e => e.Uuid == Uuid);

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

            loadDoctors();
            loadPatients();

            return View(appDto);
        }

        [HttpPost]
        [ActionName("Delete")]
      
        public ActionResult DeleteConfirm(AppointmentUpdateDto appDto)
        {
            var app = _db.Appointments.FirstOrDefault(e => e.Uuid == appDto.Uuid);
            if (app == null)
                return NotFound();


            _db.Appointments.Remove(app);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
