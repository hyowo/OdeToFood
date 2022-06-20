using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Filter;

namespace OdeToFood.Controllers
{
    [Log]
    public class CuisineController : Controller
    {
        [HttpPost]
        public IActionResult Search(string name = "french")
        {
            return Content("First: " + name);
        }
        
        [HttpGet]
        public IActionResult Search(string name, bool notused)
        {
            return Content("Search");
        }
    }
}