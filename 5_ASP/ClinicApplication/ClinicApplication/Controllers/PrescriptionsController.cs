using ClinicApplication.Data;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
    public class PrescriptionsController : Controller
    {

        private readonly AppDbContext _db;
        public PrescriptionsController(AppDbContext db)
        {
            _db = db;

        }
        public ActionResult Index()
        {

            IEnumerable<Prescription> Prescriptions = _db.Prescriptions.ToList();
            return View(Prescriptions);
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
        public ActionResult Create(Prescription Pre)
        {
            if (ModelState.IsValid)
            {
                _db.Prescriptions.Add(Pre);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(Pre);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Pre = _db.Prescriptions.Find(Id);
            if (Pre == null)
            {
                return NotFound();
            }

            return View(Pre);
        }

        [HttpPost]
        public ActionResult Edit(Prescription Pre)
        {
            if (ModelState.IsValid)
            {
                _db.Prescriptions.Update(Pre);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(Pre);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var Pre = _db.Prescriptions.Find(Id);
            if (Pre == null)
            {
                return NotFound();
            }

            return View(Pre);
        }

        [HttpPost]
        public ActionResult Delete(Prescription Pre)
        {

            _db.Prescriptions.Remove(Pre);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
