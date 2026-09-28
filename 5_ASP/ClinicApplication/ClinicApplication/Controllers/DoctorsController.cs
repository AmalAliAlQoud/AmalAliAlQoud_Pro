using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult Edit(string Uuid)
        {
            var dr = _db.Doctors.FirstOrDefault(e => e.Uuid == Uuid);
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
                var oldDoctor = _db.Doctors.FirstOrDefault(e => e.Uuid == dr.Uuid);

                if (oldDoctor == null)
                    return NotFound();

                oldDoctor.Name = dr.Name;
                oldDoctor.Specialty = dr.Specialty;
                oldDoctor.Phone = dr.Phone;
               
                //_db.Doctors.Update(dr);
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
        public ActionResult Delete(string Uuid)
        {
            var dr = _db.Doctors.FirstOrDefault(e => e.Uuid == Uuid);
            if (dr == null)
            {
                return NotFound();
            }

            return View(dr);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string Uuid)
        {
            var dr = _db.Doctors.FirstOrDefault(e => e.Uuid == Uuid);
            if (dr == null)
            {
                return NotFound();
            }

            _db.Doctors.Remove(dr);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
