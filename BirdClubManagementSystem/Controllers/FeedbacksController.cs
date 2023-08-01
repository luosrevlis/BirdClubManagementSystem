using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class FeedbacksController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public FeedbacksController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1, string keyword = "")
        {
            List<FeedbackDTO> feedbacks = _dbContext.Feedbacks
                .Where(feedback => feedback.Title.ToLower().Contains(keyword.ToLower()))
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Include(feedback => feedback.User)
                .Select(feedback => _mapper.Map<FeedbackDTO>(feedback))
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
            return View(_mapper.Map<FeedbackDTO>(feedback));
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
