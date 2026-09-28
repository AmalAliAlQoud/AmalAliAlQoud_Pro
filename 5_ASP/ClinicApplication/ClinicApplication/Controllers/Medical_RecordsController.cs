using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
    public class Medical_RecordsController : Controller
    {
       

        private readonly AppDbContext _db;
        public Medical_RecordsController(AppDbContext db)
        {
            _db = db;

        }
        public ActionResult Index()
        {

            IEnumerable<Medical_Record> medical_Records = _db.Medical_Records.ToList();
            return View(medical_Records);
           
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
        public ActionResult Create(Medical_Record md)
        {
            if (ModelState.IsValid)
            {
                _db.Medical_Records.Add(md);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(md);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(string Uuid)
        {
            var md = _db.Medical_Records.FirstOrDefault(e => e.Uuid == Uuid);

            if (md == null)
            {
                return NotFound();
            }

            return View(md);
        }

        [HttpPost]
        public ActionResult Edit(Medical_Record md)
        {
            var oldmd = _db.Medical_Records.FirstOrDefault(e => e.Uuid == md.Uuid);

            if (ModelState.IsValid)
            {
                oldmd.Patient_ID=md.Patient_ID;
                oldmd.Doctor_ID = md.Doctor_ID;
                oldmd.Diagnosis = md.Diagnosis;
                oldmd.Notes = md.Notes;


                //_db.Medical_Records.Update(md);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(md);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(string Uuid)
        {
            var md = _db.Medical_Records.FirstOrDefault(e => e.Uuid == Uuid);
            if (md == null)
            {
                return NotFound();
            }

            return View(md);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Deleteconfirm(string Uuid)
        {
            var md = _db.Medical_Records.FirstOrDefault(e => e.Uuid == Uuid);

            if (md == null)
            {
                return NotFound();
            }

            _db.Medical_Records.Remove(md);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
