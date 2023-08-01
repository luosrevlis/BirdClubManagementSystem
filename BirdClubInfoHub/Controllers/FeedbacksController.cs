using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class FeedbacksController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public FeedbacksController
            (BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
        public IActionResult Create(FeedbackDTO dto)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Feedback feedback = _mapper.Map<Feedback>(dto);
            feedback.User = user;
            _dbContext.Feedbacks.Add(feedback);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Feedback recorded!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "Home");
        }
    }
}
