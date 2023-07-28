using BirdClubInfoHub.Data;
using Microsoft.AspNetCore.Mvc;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models.Entities;
using AutoMapper;

namespace BirdClubInfoHub.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public MeetingsController(
            BcmsDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: MeetingsController/Details/5
        public IActionResult Details(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }

            // if not open, return unavailable
            if (meeting.Status != "Open")
            {
                ViewBag.Status = "Unavailable";
                return View(meeting);
            }

            // open but not logged in, return unauth
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                ViewBag.Status = "Unauth";
                return View(meeting);
            }

            // open, logged in, already registered
            if (_dbContext.MeetingRegistrations.FirstOrDefault(x => x.MeetingId == id && x.UserId == userId) != null)
            {
                ViewBag.Status = "Registered";
                return View(meeting);
            }

            // open, logged in, not registered
            ViewBag.Status = "Available";
            return View(meeting);
        }
    }
}
