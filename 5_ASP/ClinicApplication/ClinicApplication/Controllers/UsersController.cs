using ClinicApplication.Data;
using ClinicApplication.Migrations;
using ClinicApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ClinicApplication.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        // =========================
        // Index
        // =========================
        public IActionResult Index()
        {
            var users = _db.Users.ToList();

            return View(users);
         
        }

        // =========================
        // Details
        // =========================
        public IActionResult Details(string Uuid)
        {
            var user = _db.Users.Find(Uuid);

            if (user == null)
                return NotFound();

            return View(user);
        }


        // =========================
        // Create GET
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // Create POST
        // =========================
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _db.Users.Add(user);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }


        // =========================
        // Edit GET
        // =========================
        public IActionResult Edit(string Uuid)
        {
            var user = _db.Users.FirstOrDefault(e => e.Uuid == Uuid);

            if (user == null)
                return NotFound();

            return View(user);
        }


        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {

                var oldUser = _db.Users.FirstOrDefault(e => e.Uuid == user.Uuid);



                if (oldUser == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(user.Password) )
                {

                    oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    oldUser.Name = user.Name;
                    oldUser.UserName = user.UserName;
                    oldUser.Email = user.Email;

                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }


        // =========================
        // Delete GET
        // =========================
        public IActionResult Delete(string Uuid)
        {
            var user = _db.Users.FirstOrDefault(e => e.Uuid == Uuid);

            if (user == null)
                return NotFound();

            return View(user);
        }



        // POST
        [HttpPost]
        [ActionName("Delete")]

        public IActionResult DeleteConfirm(string Uuid)
        {
            var user = _db.Users.FirstOrDefault(e => e.Uuid == Uuid);

            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}