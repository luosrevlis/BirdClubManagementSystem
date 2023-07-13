using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class FeedbacksController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public FeedbacksController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View("Create");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback feedback)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            feedback.User = user;
            _dbContext.Feedbacks.Add(feedback);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Feedback recorded!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "Home");
        }
    }
}
