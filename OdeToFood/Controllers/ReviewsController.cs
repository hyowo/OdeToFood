using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ReviewsController
        public ActionResult Index([Bind(Prefix = "id")] int restaurantId)
        {
            var model = _context.Restaurants.Find(restaurantId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}