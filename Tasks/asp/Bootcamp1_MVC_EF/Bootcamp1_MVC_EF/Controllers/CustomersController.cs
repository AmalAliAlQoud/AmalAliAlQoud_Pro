using Bootcamp1_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp1_MVC_EF.Controllers
{
    public class CustomersController : Controller
    {
       

        public IActionResult GetCustomers()
        {
            IList<Customers> customersList = new List<Customers>
            {

                new Customers(1,"Amal",20,"amal@live.com"),
                new Customers(2,"Nora",33,"Nora@live.com"),
                new Customers(3,"Aziz",33,"Aziz@live.com"),
                new Customers(4,"Ali",55,"Ali@live.com"),
                new Customers(5,"Fahad",33,"Fahad@live.com"),
                new Customers(6,"Malik",98,"Malik@live.com"),
            };

            return View(customersList);
        }
    }
}
