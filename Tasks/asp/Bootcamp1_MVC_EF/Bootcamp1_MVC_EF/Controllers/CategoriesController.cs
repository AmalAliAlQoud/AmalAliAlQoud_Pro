using Bootcamp1_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp1_MVC_EF.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult GetCategories()
        {
            IList<Category> CategoryList = new List<Category>()
{
    new Category(1, "Electronics", "https://images/electronics.png", 60),
    new Category(2, "Fashion", "https://images/fashion.png", 60),
    new Category(3, "Home & Kitchen", "https://images/home.png", 40),
    new Category(4, "Books", "https://images/books.png", 33),
    new Category(5, "Beauty & Care", "https://images/beauty.png", 25)
};




            return View(CategoryList);
        }
    }
}
