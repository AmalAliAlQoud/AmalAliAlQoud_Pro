using ClinicApplication.Infrastructure.Data;
using ClinicApplication.Application.Dtos;
//using ClinicApplication.Migrations;
using ClinicApplication.Domain.Models;
using ClinicApplication.Application.Interfaces;// note
using ClinicApplication.Application.Interfaces.Base;
using ClinicApplication.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ClinicApplication.Controllers
{
    public class DoctorsController : Controller
    {





        //---------------------------------
        private readonly IDoctorService _DoctorService; 
        private readonly ISpecialtyService _SpecialtyService;

        public DoctorsController(IDoctorService DoctorService, ISpecialtyService SpecialtyService)
        {
            _DoctorService = DoctorService;
            _SpecialtyService = SpecialtyService;
        }
        //-----------------------------------------------
      

      


        private async Task  loadSpecialties() 
        {
          
            var specialties = await _SpecialtyService.GetAllSpecialty();
            ViewBag.Specialty = new SelectList(specialties, "Id", "Name");
          

        }


        public async Task<ActionResult> Index()
        {
            var doctors = await _DoctorService.GetAllDoctor();
             return View(doctors);

        }

      
        //===============
        //Create
        //==========================

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await loadSpecialties();
            return View();
        }

        [HttpPost]
        
        public async Task<ActionResult> Create(DoctorCreateDto drDto)
        {
            if (ModelState.IsValid)
            {
                await _DoctorService.AddDoctor(drDto);

              

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadSpecialties();

            return View(drDto);

        }

      
        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<ActionResult> Edit(string Uuid)
        {
            var dr = await _DoctorService.GetDoctorById(Uuid);

            await loadSpecialties();
            return View(dr);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DoctorUpdateDto drDto) 

        {
            if (ModelState.IsValid)
            {

                await _DoctorService.UpdateDoctor(drDto);

              

              
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            await loadSpecialties();
            return View(drDto);

        }
       

        //===============
        //Delete
        //==========================
        [HttpGet]

        public async Task<ActionResult> Delete(string Uuid)
        {
            var dr = await _DoctorService.GetDoctorById(Uuid);



            return View(dr);
        }
        
        [HttpPost]
        [ActionName("Delete")]
             public async Task<ActionResult> DeleteConfirm(DoctorUpdateDto DrDto)
        {
            var dr = await _DoctorService.GetDoctorById(DrDto.Uuid);

            if (dr == null)
            {
                return NotFound();
            }

            await _DoctorService.DeleteDoctor(DrDto.Uuid);

           
            return RedirectToAction("Index");


        }



       
    }
}
