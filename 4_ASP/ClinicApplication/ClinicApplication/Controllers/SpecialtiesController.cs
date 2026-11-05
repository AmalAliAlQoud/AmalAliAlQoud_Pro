using ClinicApplication.Application.Dtos;
using ClinicApplication.Domain.Models;
using ClinicApplication.Infrastructure.Repositories;
using ClinicApplication.Infrastructure.Repositories.Base;
using ClinicApplication.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
    public class SpecialtiesController : Controller
    {


        private readonly ISpecialtyService _SpecialtyService;

        public SpecialtiesController(ISpecialtyService SpecialtyService)
        {
            _SpecialtyService = SpecialtyService;
        }


        private string UploadImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "images",
      "Specialties"
    );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/Specialties/" + fileName;
        }

        //--------------------------------------------------------------

        public async Task<ActionResult> Index()
        {
            //IEnumerable<Specialty> Specialties = _db.Specialties.ToList();
            //return View(Specialties);

            IEnumerable<Specialty> Specialties = await _SpecialtyService.GetAllSpecialty();
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
        public async Task<ActionResult> Create(Specialty sp,IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    sp.ImageUrl = UploadImage(image);
                }
                await _SpecialtyService.AddSpecialty(sp);
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
            var sp = await _SpecialtyService.GetSpecialtyById(Uuid);



            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Specialty sp, IFormFile? image) 
        {

            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    sp.ImageUrl = UploadImage(image);
                }


                //   var oldSpecialty = _db.Specialties.FirstOrDefault(e => e.Uuid == sp.Uuid);
                var oldSpecialty = await _SpecialtyService.GetSpecialtyById(sp.Uuid);


                if (oldSpecialty == null)
                    return NotFound();

                oldSpecialty.Name = sp.Name;
                oldSpecialty.ImageUrl = sp.ImageUrl;
                // _db.Specialties.Update(sp);



                //_db.SaveChanges();
                await _SpecialtyService.UpdateSpecialty(oldSpecialty);
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
            var sp = await _SpecialtyService.GetSpecialtyById(Uuid);

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
            var sp = await _SpecialtyService.GetSpecialtyById(Uuid);

           
            if (sp == null)
                return NotFound();

            //_specialtyRepository
            await _SpecialtyService.DeleteSpecialty(Uuid);

           
            return RedirectToAction("Index");


        }


    }
}
