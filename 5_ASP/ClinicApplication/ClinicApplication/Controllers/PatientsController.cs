using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using ClinicApplication.Repositories;
using ClinicApplication.Repositories.Base;
using ClinicApplication.Services;
using ClinicApplication.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
    [Authorize]

    public class PatientsController : Controller
    {



        private readonly IPatientService _PatientService;

        public PatientsController(IPatientService PatientService)
        {
            _PatientService = PatientService;
        }

        public async Task<ActionResult> Index()
        {

            IEnumerable<Patient> patients = await _PatientService.GetAllPatient();
            return View(patients);



        }
        //===============
        //Create
        //==========================

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Patient pat)
        {
            if (ModelState.IsValid)
            {
                await _PatientService.AddPatient(pat);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(pat);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<IActionResult> Edit(string Uuid)
        {


            var pat =await  _PatientService.GetPatientById(Uuid);//

            if (pat == null)
            {
                return NotFound();
            }

            return View(pat);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Patient pat)
        {
            if (ModelState.IsValid)
            {


                // var oldPatient = _db.Patients.FirstOrDefault(e => e.Uuid == pat.Uuid);
                var oldPatient = await _PatientService.GetPatientById(pat.Uuid);

                if (oldPatient == null)
                    return NotFound();

                oldPatient.Name = pat.Name;
                oldPatient.Phone = pat.Phone;
                oldPatient.Date_of_Birth = pat.Date_of_Birth;

                //_db.SaveChanges();
                await _PatientService.UpdatePatient(oldPatient);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(pat);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public async Task<ActionResult> Delete(string Uuid)
        {

            var pat = await _PatientService.GetPatientById(Uuid);
            if (pat == null)
            {
                return NotFound();
            }

            return View(pat);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(string Uuid)
        {

            // var pat = _db.Patients.FirstOrDefault(e => e.Uuid == Uuid);
            var pat = await _PatientService.GetPatientById(Uuid);
            if (pat == null)
            {
                return NotFound();
            }

            //DeletePatientAsync
            await _PatientService.DeletePatient(Uuid);

            // _db.Patients.Remove(pat);
            // _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}

