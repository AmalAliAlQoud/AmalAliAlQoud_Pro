using Bootcamp1_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp1_MVC_EF.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult GetProducts()
        {

            IList<Product> products = new List<Product>
            {
             new Product(1,"pen",10.1,80),
             new Product(2,"note",6.5,130),
             new Product(3,"table",300.0,100),
             new Product(4,"phone",4500,1000),


            };
            return View(products);
        }
    }
}
