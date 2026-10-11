using ClinicApplication.Data;
using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using ClinicApplication.Repositories;
using ClinicApplication.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Controllers
{
    public class Medical_RecordsController : Controller
    {

        //private readonly IMedical_RecordsRepository _Medical_RecordsRepository;//  : 
        //private readonly IDoctorRepository _DoctorRepository;
        //private readonly IPatientRepository _PatientRepository;

        private readonly IUnitOfWork _unitOfWork;

        public Medical_RecordsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }

      

        private async Task loadPatients()
        {
            //var Patients = _db.Patients.ToList();
            //SelectList selectListPatient = new SelectList(Patients, "Id", "Name");
            //ViewBag.Patients = selectListPatient;
            //---------
            var Patients = await _unitOfWork.PatientRepository.GetAllPatient();
            ViewBag.Patients = new SelectList(Patients, "Id", "Name");

        }

        private async Task loadDoctors()
        {
            //var doctors = _db.Doctors.ToList();
            //SelectList selectList = new SelectList(doctors, "Id", "Name");
            //ViewBag.Doctors = selectList;
            //-------------------
            var doctors = await _unitOfWork.DoctorRepository.GetAllDoctorAsync();
            ViewBag.doctors = new SelectList(doctors, "Id", "Name");

        }

        //Id  Uuid  PatientName   DoctorName  Diagnosis   Notes

        public async Task<ActionResult> Index()
        {
             var MdList = await _unitOfWork.MMdical_RecordsRepository.GetAllMedicalRecordAsyncInclude();

            var Md = MdList.Select(e => new Medical_RecordDto
            {
                
                Id = e.Id,
                Uuid = e.Uuid,
                PatientName = e.Patient != null ? e.Patient.Name : null,
                DoctorName = e.Doctor != null ? e.Doctor.Name : null,
                Diagnosis = e.Diagnosis,
                Notes = e.Notes
            })
    .ToList();

            return View(Md);
           



        }



        //===============
        //Create
        //==========================
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await loadDoctors();
            await loadPatients();
            return View();
        }

       
        [HttpPost]
        
         public async Task<ActionResult> Create(Medical_RecordCreateDto mdDto)
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
                await _unitOfWork.MMdical_RecordsRepository.AddMedical_RecordAsync(mRecord);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadDoctors();
            await loadPatients();
            return View(mdDto);
        }
      


        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<ActionResult> Edit(string Uuid)
        {
            //   var md = _db.Medical_Records.FirstOrDefault(e => e.Uuid == Uuid);
            Medical_Record? md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(Uuid);

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

            await loadDoctors();
            await loadPatients();

            return View(mdDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Medical_RecordUpdateDto mdDto)
        {

            if (ModelState.IsValid)
            {
                var oldmd = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(mdDto.Uuid);

                oldmd.Patient_ID= mdDto.Patient_ID;
                oldmd.Doctor_ID = mdDto.Doctor_ID;
                oldmd.Diagnosis = mdDto.Diagnosis;
                oldmd.Notes = mdDto.Notes;


                await _unitOfWork.MMdical_RecordsRepository.UpdateMedical_RecordAsync(oldmd);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadDoctors();
            await loadPatients();

            return View(mdDto);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public async Task<ActionResult> Delete(string Uuid)
        {
            Medical_Record? md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsyncInclude(Uuid);
            if (md == null)
            return NotFound();

            var mdDto = new Medical_RecordUpdateDto
            {
                Patient_ID = md.Patient_ID,
                //PatientName=md.Patient.Name,
                PatientName = md.Patient != null ? md.Patient.Name : "null",
                Doctor_ID = md.Doctor_ID,
                //DoctorName = md.Doctor.Name,
                DoctorName = md.Doctor != null ? md.Doctor.Name : "null",
                Diagnosis = md.Diagnosis,
                Notes = md.Diagnosis,
            };
            
           

            return View(mdDto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Deleteconfirm(Medical_RecordUpdateDto mdDto)
        {
            var md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(mdDto.Uuid);

            if (md == null)
            {
                return NotFound();
            }
            await _unitOfWork.MMdical_RecordsRepository.DeleteMedical_RecordAsync(mdDto.Uuid);
           
            return RedirectToAction("Index");


        }
    }
}
