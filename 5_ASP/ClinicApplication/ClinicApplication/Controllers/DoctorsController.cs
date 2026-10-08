using ClinicApplication.Data;
using ClinicApplication.Dtos;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using ClinicApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ClinicApplication.Controllers
{
    public class DoctorsController : Controller
    {


        private readonly IDoctorRepository _DoctorRepository;
        private readonly ISpecialityRepository _specialtyRepository;

        public DoctorsController(IDoctorRepository DoctorRepository, ISpecialityRepository specialtyRepository)
        {
            _DoctorRepository = DoctorRepository;
            _specialtyRepository = specialtyRepository;
        }

        //---------------------------------


        private async Task  loadSpecialties() 
        {
           // var Specialties = _db.Specialties.ToList();
           // var selectListSpecialties = await _SpecialtyRepository.GetAllSpecialtyAsync();
            var specialties = await _specialtyRepository.GetAllSpecialtyAsync();
            ViewBag.Specialty = new SelectList(specialties, "Id", "Name");
            //SelectList selectListSpecialties = new SelectList(Specialties, "Id", "Name");

            //ViewBag.Specialty = selectListSpecialties;

        }


        public async Task<ActionResult> Index()
        {
            var doctorsList = await _DoctorRepository.GetAllDoctorAsyncInclude();

            var Doctors = doctorsList.Select(e => new DoctorDto 
            {
                 Id = e.Id,
                 Uuid = e.Uuid,
                 Name = e.Name,
                 SpecialtyName = e.Specialty.Name,
                // SpecialtyName = e.Specialty != null ? e.Specialty.Name : "null",
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
                await _DoctorRepository.AddDoctorAsync(dr);



                //_db.Doctors.Add(dr);
                //_db.SaveChanges();
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
            Doctor? dr = await _DoctorRepository.GetDoctorAsyncByIdAsync(Uuid);

           // Doctor? dr = _db.Doctors.FirstOrDefault(e => e.Uuid == Uuid);

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

                // var oldDoctor = _db.Doctors.FirstOrDefault(e => e.Uuid == drDto.Uuid);
                var oldDoctor = await _DoctorRepository.GetDoctorAsyncByIdAsync(drDto.Uuid);

                if (oldDoctor == null)
                    return NotFound();

                oldDoctor.Name = drDto.Name;
                oldDoctor.Specialty_ID = drDto.SpecialtyId;
                oldDoctor.Phone = drDto.Phone;

                await _DoctorRepository.UpdateDoctorAsync(oldDoctor);

               // _db.SaveChanges();
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
            //Doctor? dr = _db.Doctors.Include(d => d.Specialty).FirstOrDefault(e => e.Uuid == Uuid);
            Doctor? dr = await _DoctorRepository.GetDoctorAsyncByIdAsyncInclude(Uuid);


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


           // loadSpecialties();


            return View(drDto);
        }
        
        [HttpPost]
        [ActionName("Delete")]
             public async Task<ActionResult> DeleteConfirm(DoctorUpdateDto DrDto)
        {
          //  var dr = _db.Doctors.FirstOrDefault(e => e.Uuid == DrDto.Uuid);
            var dr = await _DoctorRepository.GetDoctorAsyncByIdAsync(DrDto.Uuid);

            if (dr == null)
            {
                return NotFound();
            }

            await _DoctorRepository.UpdateDoctorAsync(dr);

            //_db.Doctors.Remove(dr);
            //_db.SaveChanges();
            return RedirectToAction("Index");


        }



       
    }
}
