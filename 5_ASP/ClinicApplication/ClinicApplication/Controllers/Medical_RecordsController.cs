using ClinicApplication.Data;
using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Controllers
{
    public class Medical_RecordsController : Controller
    {
       

        private readonly AppDbContext _db;
        public Medical_RecordsController(AppDbContext db)
        {
            _db = db;

        }

        private void loadPatients()
        {
            var Patients = _db.Patients.ToList();
            SelectList selectListPatient = new SelectList(Patients, "Id", "Name");
            ViewBag.Patients = selectListPatient;

        }

        private void loadDoctors()
        {
            var doctors = _db.Doctors.ToList();
            SelectList selectList = new SelectList(doctors, "Id", "Name");
            ViewBag.Doctors = selectList;

        }

        //Id  Uuid  PatientName   DoctorName  Diagnosis   Notes

        public async Task<ActionResult> Index()
        {
            IEnumerable<Medical_RecordDto> Medical_Records = await _db.Medical_Records.Select(e=>new Medical_RecordDto {

            Id=e.Id,
            Uuid=e.Uuid,
            PatientName=e.Patient !=null ? e.Patient.Name : null,
            DoctorName =e.Doctor !=null ? e.Doctor.Name : null,
            Diagnosis=e.Diagnosis,
            Notes=e.Notes


            }).ToListAsync();

            // IEnumerable<Medical_Record> medical_Records = _db.Medical_Records.ToList();
            return View(Medical_Records);

        }



        //===============
        //Create
        //==========================
        [HttpGet]
        public ActionResult Create()
        {
            loadDoctors();
            loadPatients();
            return View();
        }

       
        [HttpPost]
        
         public ActionResult Create(Medical_RecordCreateDto mdDto)
        {
           
            if (ModelState.IsValid)
            {
                var mRecord = new Medical_Record
                {
                    //mapping  //Patient_ID    Doctor_ID      Diagnosis    Notes
                    Patient_ID = mdDto.Patient_ID,
                    Doctor_ID = mdDto.Doctor_ID,
                    Diagnosis = mdDto.Diagnosis,
                    Notes = mdDto.Notes,

                };
                _db.Medical_Records.Add(mRecord);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            loadDoctors();
            loadPatients();
            return View(mdDto);
        }
      


        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(string Uuid)
        {
            //   var md = _db.Medical_Records.FirstOrDefault(e => e.Uuid == Uuid);
            Medical_Record? md=_db.Medical_Records.FirstOrDefault(e => e.Uuid == Uuid);

            if (md == null)
            {
                return NotFound();
            }

            var mdDto = new Medical_RecordUpdateDto
            {
                Doctor_ID = md.Doctor_ID,
                Patient_ID = md.Patient_ID,
                Diagnosis = md.Diagnosis,
                Notes = md.Notes,

            };

            loadDoctors();
            loadPatients();

            return View(mdDto);
        }

        [HttpPost]
        public ActionResult Edit(Medical_RecordUpdateDto mdDto)
        {

            if (ModelState.IsValid)
            {
                var oldmd = _db.Medical_Records.FirstOrDefault(e => e.Uuid == mdDto.Uuid);

                oldmd.Patient_ID= mdDto.Patient_ID;
                oldmd.Doctor_ID = mdDto.Doctor_ID;
                oldmd.Diagnosis = mdDto.Diagnosis;
                oldmd.Notes = mdDto.Notes;


                //_db.Medical_Records.Update(md);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(mdDto);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(string Uuid)
        {
            Medical_Record? md = _db.Medical_Records.Include(p => p.Patient).Include(d =>d.Doctor).FirstOrDefault(e => e.Uuid == Uuid);
            if (md == null)
            return NotFound();

            var mdDto = new Medical_RecordUpdateDto
            {
                Patient_ID = md.Patient_ID,
                PatientName=md.Patient.Name,
                Doctor_ID = md.Doctor_ID,
                DoctorName = md.Doctor.Name,
                Diagnosis = md.Diagnosis,
                Notes = md.Diagnosis,
            };
            
            loadDoctors();
            loadPatients();

            return View(mdDto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Deleteconfirm(Medical_RecordUpdateDto mdDto)
        {
            var md = _db.Medical_Records.FirstOrDefault(e => e.Uuid == mdDto.Uuid);

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
