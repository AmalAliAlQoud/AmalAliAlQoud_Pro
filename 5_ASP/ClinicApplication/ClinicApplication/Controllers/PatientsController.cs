using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
    [Authorize]

    public class PatientsController : Controller
    {

        private readonly AppDbContext _db;
        public PatientsController(AppDbContext db)
        {
            _db = db;

        }

        public ActionResult Index()
        {
            IEnumerable<Patient> patients = _db.Patients.ToList();
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
        public ActionResult Create(Patient pat)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Add(pat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(pat);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(string Uuid)
        {
            var pat = _db.Patients.FirstOrDefault(e => e.Uuid == Uuid);

            if (pat == null)
            {
                return NotFound();
            }

            return View(pat);
        }

        [HttpPost]
        public ActionResult Edit(Patient pat)
        {
            if (ModelState.IsValid)
            {
               
               
                    var oldPatient = _db.Patients.FirstOrDefault(e => e.Uuid == pat.Uuid);

                    if (oldPatient == null)
                        return NotFound();

                    oldPatient.Name = pat.Name;
                    oldPatient.Phone = pat.Phone;
                    oldPatient.Date_of_Birth = pat.Date_of_Birth;


               
                    _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(pat);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(string Uuid)
        {
            var pat = _db.Patients.FirstOrDefault(e => e.Uuid == Uuid);
            if (pat == null)
            {
                return NotFound();
            }

            return View(pat);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string Uuid)
        {
                         
            var pat = _db.Patients.FirstOrDefault(e => e.Uuid == Uuid);
            if (pat == null)
            {
                return NotFound();
            }



            _db.Patients.Remove(pat);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
