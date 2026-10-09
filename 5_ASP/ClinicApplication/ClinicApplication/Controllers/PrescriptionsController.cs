using ClinicApplication.Data;
using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using ClinicApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicApplication.Controllers
{
    [Authorize]

    public class PrescriptionsController : Controller
    {

        private readonly IPrescriptionsRepository _PrescriptionsRepository;//PrescriptionsRepository : IPrescriptionsRepository
        private readonly IMedical_RecordsRepository _Medical_RecordsRepository;//Medical_RecordsRepository : IMedical_RecordsRepository

        public PrescriptionsController(IPrescriptionsRepository PrescriptionsRepository, IMedical_RecordsRepository Medical_RecordsRepository)
        {
            _PrescriptionsRepository = PrescriptionsRepository;
            _Medical_RecordsRepository = Medical_RecordsRepository;
        }

        private async Task loadMeddicalRecord()
        {
           

            var MRecords = await _Medical_RecordsRepository.GetAllMedical_RecordAsync();
            ViewBag.MRecords = new SelectList(MRecords, "Id", "Id");

        }


        //   Id  Uuid   mdRecord_ID     Medicine_Name  Dosage

        public async Task<ActionResult> Index()
        {
            var PreList = await _PrescriptionsRepository.GetAllPrescriptionAsyncInclude();


            var Prescription = PreList.Select(e => new PrescriptionDto
            {

                Id=e.Id,
                Uuid=e.Uuid,
                mdRecord_ID=e.Medical_Record !=null ? e.Medical_Record.Id : e.Medical_Record.Id,
                Medicine_Name=e.Medicine_Name,
                Dosage=e.Dosage

            }).ToList();
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

               _PrescriptionsRepository.AddPrescriptionAsync(Pre); ;
               
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(PreDto);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<ActionResult> Edit(string Uuid)
        {
            Prescription? Pre = await _PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(Uuid);    
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
            await loadMeddicalRecord();

            return View(preDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PrescriptionUpdateDto PreDto)
        {


            if (ModelState.IsValid)
            {
                var OldPre = await _PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(PreDto.Uuid);

                if (OldPre == null)
                    return NotFound();

                OldPre.Medicine_Name= PreDto.Medicine_Name;
                OldPre.mdRecord_ID = PreDto.mdRecord_ID;
                OldPre.Dosage= PreDto.Dosage;




                await _PrescriptionsRepository.UpdatePrescriptionAsync(OldPre);
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
            Prescription? Pre = await _PrescriptionsRepository.GetPrescriptionAsyncByIdAsyncInclude(Uuid);
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

           


            return View(PreDto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(PrescriptionUpdateDto PreDto)
        {
            Prescription? Pre = await _PrescriptionsRepository.GetPrescriptionAsyncByIdAsync(PreDto.Uuid);
                    if (Pre==null)
                         return NotFound();


            _PrescriptionsRepository.DeletePrescriptionAsync(PreDto.Uuid);


            return RedirectToAction("Index");


        }
    }
}
