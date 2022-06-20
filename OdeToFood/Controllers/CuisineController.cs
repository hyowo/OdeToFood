using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    public class CuisineController : Controller
    {
        public IActionResult Search(string name = "french")
        {
            return RedirectToAction("Index", "Home", new { name = name });
        }
    }
}