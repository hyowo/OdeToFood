using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using OdeToFood.Models;
using System.Diagnostics;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ActionResult Autocomplete(string term)
        {
            var model = _context.Restaurants
                .Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new
                {
                    label = r.Name
                });
            return Json(model);
        }

        public IActionResult Index(string search = null, int page = 1)
        {
            var model = _context.Restaurants
                .OrderByDescending(
                r => r.Reviews.Average(review => review.Rating)
                )
                .Where(r => search == null || r.Name.Contains(search))
                .Select(r => new RestaurantListViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    City = r.City,
                    Country = r.Country,
                    CountOfReviews = r.Reviews.Count()
                }).Take(10);
                // topagedlist doesn't exist in net 6

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_Restaurants", model);
            //}
            // ajax wont download
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            var model = new AboutModel()
            {
                Name = "Erik Vilumaa",
                Location = "Parila"
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}