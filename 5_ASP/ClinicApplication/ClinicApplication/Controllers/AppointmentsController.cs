using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ClinicApplication.Controllers
{
    [Authorize]

    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _db;
        public AppointmentsController(AppDbContext db)
        {
            _db = db;

        }

        public ActionResult Index()
        {

            IEnumerable<Appointment> Appointments = _db.Appointments.ToList();
            return View(Appointments);
           
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
        public ActionResult Create(Appointment app)
        {
            if (ModelState.IsValid)
            {
                _db.Appointments.Add(app);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(app);

        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(string Uuid)
        {
            var app = _db.Appointments.FirstOrDefault(e => e.Uuid == Uuid);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        [HttpPost]
        public ActionResult Edit(Appointment app)
        {
            if (ModelState.IsValid)
            {

                var oldApp = _db.Appointments.FirstOrDefault(e => e.Uuid == app.Uuid);

                if (oldApp == null)
                    return NotFound();


               
                    oldApp.Patient_ID = app.Patient_ID;
                    oldApp.Doctor_ID = app.Doctor_ID;
                    oldApp.Date = app.Date;
                    oldApp.Time = app.Time;
               

                // _db.Appointments.Update(app);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(app);

        }

        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(string Uuid)
        {
            var app = _db.Appointments.FirstOrDefault(e => e.Uuid == Uuid);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(string Uuid)
        {
            var app = _db.Appointments.FirstOrDefault(e => e.Uuid == Uuid);
            if (app == null)
                return NotFound();


            _db.Appointments.Remove(app);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
