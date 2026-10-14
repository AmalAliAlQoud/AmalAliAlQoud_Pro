using ClinicApplication.Infrastructure.Data;
//using ClinicApplication.Migrations;
using ClinicApplication.Domain.Models;
using ClinicApplication.Application.Services;
using ClinicApplication.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ClinicApplication.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
       
        private readonly IUserService _UserService;

        public UsersController(IUserService UserService)
        {
            _UserService = UserService;
        }

        // =========================
        // Index
        // =========================
        public async Task<IActionResult> Index()
        {
            //var users = _db.Users.ToList();

            //return View(users);
            IEnumerable<User> users = await _UserService.GetAllUser();
            return View(users);

        }

        // =========================
        // Details
        // =========================
        //public IActionResult Details(string Uuid)
        //{
        //    var user = _db.Users.Find(Uuid);

        //    if (user == null)
        //        return NotFound();

        //    return View(user);
        //}


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
        public async Task<IActionResult> Create(User user)
        {
           
            if (ModelState.IsValid)
            {
                  user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
               // user.Password = user.Password;

                await _UserService.AddUser(user);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(user);
        }


        // =========================
        // Edit GET
        // =========================
        public async Task<IActionResult> Edit(string Uuid)
        {
            //var user = _db.Users.FirstOrDefault(e => e.Uuid == Uuid);

            //if (user == null)
            //    return NotFound();

            //return View(user);
            var user = await _UserService.GetUserById(Uuid);//

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = await _UserService.GetUserById(user.Uuid);

                if (oldUser == null)
                    return NotFound();
                if (!string.IsNullOrEmpty(user.Password))
                    //    {
                    oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                oldUser.Name = user.Name;
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.IsLocked = user.IsLocked;
              
                await _UserService.UpdateUser(oldUser);

                return RedirectToAction("Index");
            
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(user); 
        
        }
            //--------------

            //var oldUser = _db.Users.FirstOrDefault(e => e.Uuid == user.Uuid);



            //    if (oldUser == null)
            //        return NotFound();

            //    if (!string.IsNullOrEmpty(user.Password) )
            //    {

            //        oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            //        oldUser.Name = user.Name;
            //        oldUser.UserName = user.UserName;
            //        oldUser.Email = user.Email;

            //    }

            //    _db.SaveChanges();

            //    return RedirectToAction("Index");
            //}

            //return View(user);
        


        // =========================
        // Delete GET
        // =========================
        public async Task<IActionResult> Delete(string Uuid)
        {
            //var user = _db.Users.FirstOrDefault(e => e.Uuid == Uuid);

            //if (user == null)
            //    return NotFound();

            //return View(user);

            var user = await _UserService.GetUserById(Uuid);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }



        // POST
        [HttpPost]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirm(string Uuid)
        {
            //var user = _db.Users.FirstOrDefault(e => e.Uuid == Uuid);

            //if (user == null)
            //    return NotFound();

            //_db.Users.Remove(user);
            //_db.SaveChanges();

            //return RedirectToAction("Index");
            var user = await _UserService.GetUserById(Uuid);
            if (user == null)
            {
                return NotFound();
            }

            //DeletePatientAsync
            await _UserService.DeleteUser(Uuid);

           
            return RedirectToAction("Index");
        }
    }
}