using ClinicApplication.Data;
using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        private void loadMeddicalRecord()
        {
            var MRecords = _db.Medical_Records.ToList();
            SelectList selectListMRecords = new SelectList(MRecords, "Id","Id");
            ViewBag.MRecords = selectListMRecords;

        }


        //   Id  Uuid   mdRecord_ID     Medicine_Name  Dosage

        public ActionResult Index()
        {

            IEnumerable<PrescriptionDto> Prescriptions = _db.Prescriptions.Select(e=> new PrescriptionDto
            {

                Id=e.Id,
                Uuid=e.Uuid,
                mdRecord_ID=e.Medical_Record !=null ? e.Medical_Record.Id : e.Medical_Record.Id,
                Medicine_Name=e.Medicine_Name,
                Dosage=e.Dosage

            }).ToList();
            return View(Prescriptions);
        }

        //public ActionResult Index()
        //{

        //    IEnumerable<Prescription> Prescriptions = _db.Prescriptions.ToList();
        //    return View(Prescriptions);
        //}
        //===============
        //Create
        //==========================

        [HttpGet]
        public ActionResult Create()
        {

            loadMeddicalRecord();
            return View();
        }

        [HttpPost]
        public ActionResult Create(PrescriptionCreateDto PreDto)  
        {
            if (ModelState.IsValid)
            {
                var Pre = new Prescription
                {
                    mdRecord_ID = PreDto.mdRecord_ID,
                    Medicine_Name = PreDto.Medicine_Name,
                    Dosage = PreDto.Dosage,

                };

                _db.Prescriptions.Add(Pre);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(PreDto);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(string Uuid)
        {
            Prescription? Pre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == Uuid);
            if (Pre == null)
            {
                return NotFound();
            }
            var preDto = new PrescriptionUpdateDto
            {
                Id = Pre.Id,
                Uuid = Pre.Uuid,
                mdRecord_ID = Pre.mdRecord_ID,
                Medicine_Name =Pre.Medicine_Name,
                Dosage=Pre.Dosage,



            };
            loadMeddicalRecord();

            return View(preDto);
        }

        [HttpPost]
        public ActionResult Edit(PrescriptionUpdateDto PreDto)
        {


            if (ModelState.IsValid)
            {
                var OldPre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == PreDto.Uuid);

                if (OldPre == null)
                    return NotFound();

                OldPre.Medicine_Name= PreDto.Medicine_Name;
                OldPre.mdRecord_ID = PreDto.mdRecord_ID;
                OldPre.Dosage= PreDto.Dosage;




                //  _db.Prescriptions.Update(Pre);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(PreDto);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(string Uuid)
        {
            Prescription? Pre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == Uuid);
            if (Pre == null)
               return NotFound();
            var PreDto = new PrescriptionDto
            {
                Id = Pre.Id,
                Uuid=Pre.Uuid,
                mdRecord_ID=Pre.mdRecord_ID,
                Medicine_Name=Pre.Medicine_Name,
                Dosage=Pre.Dosage,

            };

            loadMeddicalRecord();


            return View(PreDto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(PrescriptionUpdateDto PreDto)
        {
            Prescription? Pre = _db.Prescriptions.FirstOrDefault(e => e.Uuid == PreDto.Uuid);
                    if (Pre==null)
                         return NotFound();



            _db.Prescriptions.Remove(Pre);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
