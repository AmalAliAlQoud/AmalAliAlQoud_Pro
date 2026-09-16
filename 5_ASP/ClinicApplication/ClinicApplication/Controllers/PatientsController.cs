using ClinicApplication.Data;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
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
        public ActionResult Edit(int Id)
        {
            var pat = _db.Patients.Find(Id);
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
                _db.Patients.Update(pat);
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
        public ActionResult Delete(int Id)
        {
            var pat = _db.Patients.Find(Id);
            if (pat == null)
            {
                return NotFound();
            }

            return View(pat);
        }

        [HttpPost]
        public ActionResult Delete(Patient pat)
        {

            _db.Patients.Remove(pat);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
