using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class FeedbacksController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public FeedbacksController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Feedback> feedbacks = _dbContext.Feedbacks
                .Include(feedback => feedback.User)
                .ToList();
            return View(feedbacks);
        }

        public IActionResult Details(int id)
        {
            Feedback? feedback = _dbContext.Feedbacks.Find(id);
            if (feedback == null)
            {
                TempData.Add("notification", "Feedback not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Feedback? feedback = _dbContext.Feedbacks.Find(id);
            if (feedback == null)
            {
                TempData.Add("notification", "Feedback not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            _dbContext.Feedbacks.Remove(feedback);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Feedback deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
