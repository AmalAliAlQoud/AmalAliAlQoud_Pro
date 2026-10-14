using ClinicApplication;
using ClinicApplication.Application.Dtos;
//using ClinicApplication.Migrations;
using ClinicApplication.Domain.Models;
using ClinicApplication.Application.Interfaces.Base;
using ClinicApplication.Application.Services.Base;
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

        private readonly IAppointmentService _AppointmentService; // AppointmentService: IAppointmentService
         private readonly IDoctorService _DoctorService; 
        private readonly IPatientService _PatientService;

        public AppointmentsController(IAppointmentService AppointmentService, IDoctorService DoctorService, IPatientService PatientService)
        {
            _AppointmentService = AppointmentService;

            _DoctorService = DoctorService;
           
            _PatientService = PatientService;
        }


        private async Task loadPatients()
        {
            //var Patients = _unitOfWork.Patients.ToList();
            var Patients =await  _PatientService.GetAllPatient();
            SelectList selectListPatient = new SelectList(Patients, "Id", "Name");
            ViewBag.Patients = selectListPatient;

        }

        private async Task loadDoctors()
        {
            var doctors = await _DoctorService.GetAllDoctor();
            SelectList selectList = new SelectList(doctors, "Id", "Name");
            ViewBag.Doctors = selectList;

        }

        public async Task<ActionResult> Index()
        {
            var apps = await _AppointmentService.GetAllApps();
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
                await _AppointmentService.AddApp(AppountmentDTO);
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
        { 

            var appDto = await _AppointmentService.GetApprById(Uuid);            
            await loadDoctors();
            await loadPatients();
            return View(appDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AppointmentUpdateDto appDto)
        {
            if (ModelState.IsValid)
            {
                await _AppointmentService.UpdateApp(appDto);

               
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

            var Appointment = await _AppointmentService.GetApprById(Uuid);

            await loadDoctors();
            await loadPatients();

            return View(Appointment);
        }

        [HttpPost]
        [ActionName("Delete")]
      
        public async Task<ActionResult> DeleteConfirm(AppointmentUpdateDto appDto)
        {
           
            

            var app = await _AppointmentService.GetApprById(appDto.Uuid);

            if (app == null)
            {
                return NotFound();
            }

            await _AppointmentService.DeleteApp(appDto.Uuid);


            return RedirectToAction("Index");


        }
    }
}
