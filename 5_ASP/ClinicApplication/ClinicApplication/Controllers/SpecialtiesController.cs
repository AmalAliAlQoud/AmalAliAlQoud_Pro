using ClinicApplication.Data;
using ClinicApplication.Models;
using ClinicApplication.Repositories;
using ClinicApplication.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
    public class SpecialtiesController : Controller
    {

        //private readonly ISpecialityRepository _specialtyRepository;

        //public SpecialtiesController(ISpecialityRepository specialtyRepository)
        //{

        //    _specialtyRepository = specialtyRepository;
        //}
        private readonly IUnitOfWork _unitOfWork;

        public SpecialtiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }
        public async Task<ActionResult> Index()
        {
            //IEnumerable<Specialty> Specialties = _db.Specialties.ToList();
            //return View(Specialties);

            IEnumerable<Specialty> Specialties = await _unitOfWork.SpecialityRepository.GetAllSpecialtyAsync();
            return View(Specialties);

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
        public async Task<ActionResult> Create(Specialty sp)
        {
            if (ModelState.IsValid)
            {
                //_db.Specialties.Add(sp);
                //_db.SaveChanges();
                await _unitOfWork.SpecialityRepository.AddSpecialtyAsync(sp);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(sp);

        }

        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<ActionResult> Edit(string Uuid)
        {

           // var sp = _db.Specialties.FirstOrDefault(e => e.Uuid == Uuid);
            var sp = await _unitOfWork.SpecialityRepository.GetSpecialtyAsyncByIdAsync(Uuid);



            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Specialty sp)
        {
            if (ModelState.IsValid)
            {


             //   var oldSpecialty = _db.Specialties.FirstOrDefault(e => e.Uuid == sp.Uuid);
                var oldSpecialty = await _unitOfWork.SpecialityRepository.GetSpecialtyAsyncByIdAsync(sp.Uuid);


                if (oldSpecialty == null)
                    return NotFound();

                oldSpecialty.Name = sp.Name;
                // _db.Specialties.Update(sp);



                //_db.SaveChanges();
                await _unitOfWork.SpecialityRepository.UpdateSpecialtyAsync(oldSpecialty);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(sp);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public async Task<ActionResult> Delete(string Uuid)
        {
            // var sp = _db.Specialties.FirstOrDefault(e => e.Uuid == Uuid);
            var sp = await _unitOfWork.SpecialityRepository.GetSpecialtyAsyncByIdAsync(Uuid);

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(string Uuid)
        {
            //var sp = _db.Specialties.FirstOrDefault(e => e.Uuid == Uuid);
            var sp = await _unitOfWork.SpecialityRepository.GetSpecialtyAsyncByIdAsync(Uuid);

           
            if (sp == null)
                return NotFound();

            //_specialtyRepository
            await _unitOfWork.SpecialityRepository.DeleteSpecialtyAsync(Uuid);

            //_db.Specialties.Remove(sp);
            //_db.SaveChanges();
            return RedirectToAction("Index");


        }


    }
}
