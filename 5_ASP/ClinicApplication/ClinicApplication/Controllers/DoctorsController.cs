using ClinicApplication.Data;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
    public class DoctorsController : Controller
    {

        private readonly AppDbContext _db;
        public DoctorsController(AppDbContext db)
        {
            _db = db;

        }
        public ActionResult Index()
        {
            IEnumerable<Doctor> Doctors = _db.Doctors.ToList();
            return View(Doctors);
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
        public ActionResult Create(Doctor dr)
        {
            if (ModelState.IsValid)
            {
                _db.Doctors.Add(dr);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(dr);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var dr = _db.Doctors.Find(Id);
            if (dr == null)
            {
                return NotFound();
            }

            return View(dr);
        }

        [HttpPost]
        public ActionResult Edit(Doctor dr)
        {
            if (ModelState.IsValid)
            {
                _db.Doctors.Update(dr);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(dr);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var dr = _db.Doctors.Find(Id);
            if (dr == null)
            {
                return NotFound();
            }

            return View(dr);
        }

        [HttpPost]
        public ActionResult Delete(Doctor dr)
        {

            _db.Doctors.Remove(dr);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
