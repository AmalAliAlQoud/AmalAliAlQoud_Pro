using ClinicApplication.Data;
using ClinicApplication.Migrations; 
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClinicApplication.Repositories;

namespace ClinicApplication.Controllers
{
    [Authorize]

    public class PatientsController : Controller
    {

       
        private readonly IPatientRepository _PatientRepository;

        public PatientsController(IPatientRepository patientRepository)
        {
            _PatientRepository = patientRepository;
        }

        public async Task<ActionResult> Index()
        {
            //IEnumerable<Patient> patients = _db.Patients.ToList();
            //return View(patients);
            IEnumerable<Patient> patients = await _PatientRepository.GetAllPatientAsync();
            return View(patients);

          //  return View(await _categoryRepository.GetAllCategoriesAsync());

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
                await _PatientRepository.AddPatientAsync(pat);
                //_db.Patients.Add(pat);
                //_db.SaveChanges();
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
           

            var pat = await _PatientRepository.GetPatientAsyncByIdAsync(Uuid);

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
                var oldPatient= await _PatientRepository.GetPatientAsyncByIdAsync(pat.Uuid);

                if (oldPatient == null)
                        return NotFound();

                    oldPatient.Name = pat.Name;
                    oldPatient.Phone = pat.Phone;
                    oldPatient.Date_of_Birth = pat.Date_of_Birth;

                //_db.SaveChanges();
                await _PatientRepository.UpdatePatientAsync(oldPatient);
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
           // var pat = _db.Patients.FirstOrDefault(e => e.Uuid == Uuid);
            var pat = await _PatientRepository.GetPatientAsyncByIdAsync(Uuid);
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
            var pat = await _PatientRepository.GetPatientAsyncByIdAsync(Uuid);
            if (pat == null)
            {
                return NotFound();
            }

            //DeletePatientAsync
            await _PatientRepository.DeletePatientAsync(Uuid);

           // _db.Patients.Remove(pat);
           // _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
