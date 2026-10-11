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
using System.Numerics;

namespace ClinicApplication.Controllers
{
    public class DoctorsController : Controller
    {


       

        private readonly IUnitOfWork _unitOfWork;

        public DoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        //---------------------------------


        private async Task  loadSpecialties() 
        {
          
            var specialties = await _unitOfWork.SpecialityRepository.GetAllSpecialtyAsync();
            ViewBag.Specialty = new SelectList(specialties, "Id", "Name");
          

        }


        public async Task<ActionResult> Index()
        {
            var doctorsList = await _unitOfWork.DoctorRepository.GetAllDoctorAsyncInclude();

            var Doctors = doctorsList.Select(e => new DoctorDto 
            {
                 Id = e.Id,
                 Uuid = e.Uuid,
                 Name = e.Name,
                 SpecialtyName = e.Specialty.Name,
               
                 Phone = e.Phone
     })
     .ToList();

            return View(Doctors);

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

                var dr = new Doctor
                {//(Name/	SpecialtyId/	Phone)

                    Name = drDto.Name,
                    Specialty_ID = drDto.SpecialtyId,
                    Phone= drDto.Phone


                };
                await _unitOfWork.DoctorRepository.AddDoctorAsync(dr);

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
            Doctor? dr = await _unitOfWork.DoctorRepository.GetDoctorAsyncByIdAsync(Uuid);


            if (dr == null)
            {
                return NotFound();
            }

            var doctorDto = new DoctorUpdateDto
            {
                Id = dr.Id,
                Uuid = dr.Uuid,
                Name = dr.Name,
                SpecialtyId = dr.Specialty_ID,  
                Phone = dr.Phone
            };

            await loadSpecialties();
            return View(doctorDto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DoctorUpdateDto drDto) 

        {
            if (ModelState.IsValid)
            {

                var oldDoctor = await _unitOfWork.DoctorRepository.GetDoctorAsyncByIdAsync(drDto.Uuid);

                if (oldDoctor == null)
                    return NotFound();

                oldDoctor.Name = drDto.Name;
                oldDoctor.Specialty_ID = drDto.SpecialtyId;
                oldDoctor.Phone = drDto.Phone;

                await _unitOfWork.DoctorRepository.UpdateDoctorAsync(oldDoctor);

              
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
            Doctor? dr = await _unitOfWork.DoctorRepository.GetDoctorAsyncByIdAsyncInclude(Uuid);


            if (dr == null)
            {
                return NotFound();
            }
            var drDto = new DoctorUpdateDto
            {
                Id = dr.Id,
                Uuid = dr.Uuid,
                Name = dr.Name,
                SpecialtyId = dr.Specialty_ID,
                SpecialtyName = dr.Specialty != null ? dr.Specialty.Name : "null",
                Phone = dr.Phone

            };


         


            return View(drDto);
        }
        
        [HttpPost]
        [ActionName("Delete")]
             public async Task<ActionResult> DeleteConfirm(DoctorUpdateDto DrDto)
        {
            var dr = await _unitOfWork.DoctorRepository.GetDoctorAsyncByIdAsync(DrDto.Uuid);

            if (dr == null)
            {
                return NotFound();
            }

            await _unitOfWork.DoctorRepository.DeleteDoctorAsync(DrDto.Uuid);

           
            return RedirectToAction("Index");


        }



       
    }
}
