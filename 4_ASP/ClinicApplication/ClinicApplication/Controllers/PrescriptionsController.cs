using ClinicApplication.Infrastructure.Data;
using ClinicApplication.Application.Dtos;
//using ClinicApplication.Migrations;
using ClinicApplication.Domain.Models;
using ClinicApplication.Infrastructure.Repositories;
using ClinicApplication.Infrastructure.Repositories.Base;
using ClinicApplication.Application.Services;
using ClinicApplication.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicApplication.Controllers
{
    [Authorize]

    public class PrescriptionsController : Controller
    {



        //private readonly IUnitOfWork _unitOfWork;

        //public PrescriptionsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;


        //}

        private readonly IPrescriptionsService _PrescriptionsService;
        private readonly IMedical_RecordsService _Medical_RecordsService;

        public PrescriptionsController(IPrescriptionsService PrescriptionsService, IMedical_RecordsService Medical_RecordsService)
        {
            _PrescriptionsService = PrescriptionsService;
            _Medical_RecordsService = Medical_RecordsService;
        }
        private async Task loadMeddicalRecord()
        {
           

            var MRecords = await _Medical_RecordsService.GetAllMedical_Record();
            ViewBag.MRecords = new SelectList(MRecords, "Id", "Id");

        }


        //   Id  Uuid   mdRecord_ID     Medicine_Name  Dosage

        public async Task<ActionResult> Index()
        {
            var Prescription = await _PrescriptionsService.GetAllPrescription();


            //var Prescription = PreList.Select(e => new PrescriptionDto
            //{

            //    Id=e.Id,
            //    Uuid=e.Uuid,
            //    mdRecord_ID=e.Medical_Record !=null ? e.Medical_Record.Id : e.Medical_Record.Id,
            //    Medicine_Name=e.Medicine_Name,
            //    Dosage=e.Dosage

            //}).ToList();
            return View(Prescription);
        }

       
        //===============
        //Create
        //==========================

        [HttpGet]
        public async Task<ActionResult> Create()
        {

           await  loadMeddicalRecord();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PrescriptionCreateDto PreDto)  
        {
            if (ModelState.IsValid)
            {
                await _PrescriptionsService.AddPrescription(PreDto);

                //_unitOfWork.PrescriptionsRepository.AddPrescriptionAsync(Pre); ;
               
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadMeddicalRecord();
            return View(PreDto);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<ActionResult> Edit(string Uuid)
        {
            var pre = await _PrescriptionsService.GetPrescriptionById(Uuid);

            await loadMeddicalRecord();
            return View(pre);


            //Prescription? Pre = await _unitOfWork.PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(Uuid);    
            //if (Pre == null)
            //{
            //    return NotFound();
            //}
            //var preDto = new PrescriptionUpdateDto
            //{
            //    Id = Pre.Id,
            //    Uuid = Pre.Uuid,
            //    mdRecord_ID = Pre.mdRecord_ID,
            //    Medicine_Name =Pre.Medicine_Name,
            //    Dosage=Pre.Dosage,



            //};
            //await loadMeddicalRecord();

            //return View(preDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PrescriptionUpdateDto PreDto)
        {


            if (ModelState.IsValid)
            {
              //  var OldPre = await _unitOfWork.PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(PreDto.Uuid);
              await _PrescriptionsService.UpdatePrescription(PreDto);

                //if (OldPre == null)
                //    return NotFound();

                //OldPre.Medicine_Name= PreDto.Medicine_Name;
                //OldPre.mdRecord_ID = PreDto.mdRecord_ID;
                //OldPre.Dosage= PreDto.Dosage;




                //await _unitOfWork.PrescriptionsRepository.UpdatePrescriptionAsync(OldPre);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(PreDto);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public async Task<ActionResult> Delete(string Uuid)
        {

            var Pre = await _PrescriptionsService.GetPrescriptionById(Uuid);



            return View(Pre);


            //Prescription? Pre = await _unitOfWork.PrescriptionsRepository.GetPrescriptionAsyncByIdAsyncInclude(Uuid);
            //if (Pre == null)
            //   return NotFound();
            //var PreDto = new PrescriptionDto
            //{
            //    Id = Pre.Id,
            //    Uuid=Pre.Uuid,
            //    mdRecord_ID=Pre.mdRecord_ID,
            //    Medicine_Name=Pre.Medicine_Name,
            //    Dosage=Pre.Dosage,

            //};

           


            //return View(PreDto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(PrescriptionUpdateDto PreDto)
        {
            //Prescription? Pre = await _unitOfWork.PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(PreDto.Uuid);
            //        if (Pre==null)
            //             return NotFound();


            //_unitOfWork.PrescriptionsRepository.DeletePrescriptionAsync(PreDto.Uuid);


            //return RedirectToAction("Index");
            var pre = await _PrescriptionsService.GetPrescriptionById(PreDto.Uuid);

            if (pre == null)
            {
                return NotFound();
            }

            await _PrescriptionsService.DeletePrescription(PreDto.Uuid);


            return RedirectToAction("Index");


        }
    }
}
