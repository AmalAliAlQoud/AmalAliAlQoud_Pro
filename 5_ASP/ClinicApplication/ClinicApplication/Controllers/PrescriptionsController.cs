using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
    [Authorize]

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
        public ActionResult Edit(string Uuid)
        {
            var Pre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == Uuid);
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
                var OldPre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == Pre.Uuid);

                if (OldPre == null)
                    return NotFound();

                OldPre.Medicine_Name=Pre.Medicine_Name;
                OldPre.mdRecord_ID = Pre.mdRecord_ID;
                OldPre.Dosage=Pre.Dosage;




                //  _db.Prescriptions.Update(Pre);
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
        public ActionResult Delete(string Uuid)
        {
            var Pre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == Uuid);
            if (Pre == null)
            {
                return NotFound();
            }

            return View(Pre);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string Uuid)
        {
            var Pre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == Uuid);
                    if (Pre==null)
                         return NotFound();


            _db.Prescriptions.Remove(Pre);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
