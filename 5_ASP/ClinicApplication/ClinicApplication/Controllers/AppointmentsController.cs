using ClinicApplication.Data;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.Controllers
{
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
        public ActionResult Edit(int Id)
        {
            var app = _db.Appointments.Find(Id);
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
                _db.Appointments.Update(app);
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
        public ActionResult Delete(int Id)
        {
            var app = _db.Appointments.Find(Id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        [HttpPost]
        public ActionResult Delete(Appointment app)
        {

            _db.Appointments.Remove(app);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
