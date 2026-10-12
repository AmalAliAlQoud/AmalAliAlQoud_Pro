using ClinicApplication.Data;
using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using ClinicApplication.Repositories;
using ClinicApplication.Repositories.Base;
using ClinicApplication.Services;
using ClinicApplication.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicApplication.Controllers
{
    public class Medical_RecordsController : Controller
    {



        //private readonly IUnitOfWork _unitOfWork;

        //public Medical_RecordsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;


        //}
        private readonly IMedical_RecordsService _Medical_RecordsService; // Medical_RecordsService: IMedical_RecordsService
        private readonly IDoctorService _DoctorService;
        private readonly IPatientService _PatientService;

        public Medical_RecordsController(IMedical_RecordsService Medical_RecordsService, IDoctorService DoctorService, IPatientService PatientService)
        {
            _Medical_RecordsService = Medical_RecordsService;

            _DoctorService = DoctorService;

            _PatientService = PatientService;
        }



        private async Task loadPatients()
        {
            
            var Patients = await _PatientService.GetAllPatient();
            ViewBag.Patients = new SelectList(Patients, "Id", "Name");

        }

        private async Task loadDoctors()
        {
            var doctors = await _DoctorService.GetAllDoctor();
            ViewBag.doctors = new SelectList(doctors, "Id", "Name");

        }

        //Id  Uuid  PatientName   DoctorName  Diagnosis   Notes

        public async Task<ActionResult> Index()
        {

            var Md = await _Medical_RecordsService.GetAllMedical_Record();
            return View(Md);

    //        var MdList = await _unitOfWork.MMdical_RecordsRepository.GetAllMedicalRecordAsyncInclude();

    //        var Md = MdList.Select(e => new Medical_RecordDto
    //        {
                
    //            Id = e.Id,
    //            Uuid = e.Uuid,
    //            PatientName = e.Patient != null ? e.Patient.Name : null,
    //            DoctorName = e.Doctor != null ? e.Doctor.Name : null,
    //            Diagnosis = e.Diagnosis,
    //            Notes = e.Notes
    //        })
    //.ToList();

    //        return View(Md);
           



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
                await _Medical_RecordsService.AddMedical_Record(mdDto);
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadDoctors();
            await loadPatients();
            return View(mdDto);

            //if (ModelState.IsValid)
            //{
            //    var mRecord = new Medical_Record
            //    {
            //        //mapping  //Patient_ID    Doctor_ID      Diagnosis    Notes
            //        Patient_ID = mdDto.Patient_ID,
            //        Doctor_ID = mdDto.Doctor_ID,
            //        Diagnosis = mdDto.Diagnosis,
            //        Notes = mdDto.Notes,

            //    };
            //    await _unitOfWork.MMdical_RecordsRepository.AddMedical_RecordAsync(mRecord);

            //    return RedirectToAction("Index");
        //}

        //    ModelState.AddModelError("", "Please fill all the required fields.");
        //    await loadDoctors();
        //    await loadPatients();
        //    return View(mdDto);
        }
      


        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<ActionResult> Edit(string Uuid)
        {
            var mdDto = await _Medical_RecordsService.GetMedical_RecordById(Uuid);
            await loadDoctors();
            await loadPatients();
            return View(mdDto);
            //Medical_Record? md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(Uuid);

            //if (md == null)
            //{
            //    return NotFound();
            //}

            //var mdDto = new Medical_RecordUpdateDto
            //{
            //    Doctor_ID = md.Doctor_ID,
            //    Patient_ID = md.Patient_ID,
            //    Diagnosis = md.Diagnosis,
            //    Notes = md.Notes,

            //};

            //await loadDoctors();
            //await loadPatients();

            //return View(mdDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Medical_RecordUpdateDto mdDto)
        {

            if (ModelState.IsValid)
            {
                await _Medical_RecordsService.UpdateMedical_Record(mdDto);


                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadPatients();
            await loadDoctors();
            return View(mdDto);

            //if (ModelState.IsValid)
            //{
            //    var oldmd = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(mdDto.Uuid);

            //    oldmd.Patient_ID= mdDto.Patient_ID;
            //    oldmd.Doctor_ID = mdDto.Doctor_ID;
            //    oldmd.Diagnosis = mdDto.Diagnosis;
            //    oldmd.Notes = mdDto.Notes;


            //    await _unitOfWork.MMdical_RecordsRepository.UpdateMedical_RecordAsync(oldmd);
            //    return RedirectToAction("Index");
            //}
            //ModelState.AddModelError("", "Please fill all the required fields.");
            //await loadDoctors();
            //await loadPatients();

            //return View(mdDto);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public async Task<ActionResult> Delete(string Uuid)
        {

            var Md = await _Medical_RecordsService.GetMedical_RecordById(Uuid);

            await loadDoctors();
            await loadPatients();

            return View(Md);

            //Medical_Record? md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsyncInclude(Uuid);
            //if (md == null)
            //return NotFound();

            //var mdDto = new Medical_RecordUpdateDto
            //{
            //    Patient_ID = md.Patient_ID,
            //    //PatientName=md.Patient.Name,
            //    PatientName = md.Patient != null ? md.Patient.Name : "null",
            //    Doctor_ID = md.Doctor_ID,
            //    //DoctorName = md.Doctor.Name,
            //    DoctorName = md.Doctor != null ? md.Doctor.Name : "null",
            //    Diagnosis = md.Diagnosis,
            //    Notes = md.Diagnosis,
            //};



            //return View(mdDto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Deleteconfirm(Medical_RecordUpdateDto mdDto)
        {

            var md = await _Medical_RecordsService.GetMedical_RecordById(mdDto.Uuid);

            if (md == null)
            {
                return NotFound();
            }

            await _Medical_RecordsService.DeleteMedical_Record(mdDto.Uuid);


            return RedirectToAction("Index");

            //var md = await _unitOfWork.MMdical_RecordsRepository.GetMedicalRecordAsyncByIdAsync(mdDto.Uuid);

            //if (md == null)
            //{
            //    return NotFound();
            //}
            //await _unitOfWork.MMdical_RecordsRepository.DeleteMedical_RecordAsync(mdDto.Uuid);

            //return RedirectToAction("Index");


        }
    }
}
