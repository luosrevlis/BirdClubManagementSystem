using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public MeetingsController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1, string keyword = "", string status = "")
        {
            return Index(DateTime.Now, page, keyword, status);
        }

        public IActionResult Index(DateTime month, int page = 1, string keyword = "", string status = "")
        {
            IQueryable<Meeting> matches = _dbContext.Meetings
                .Where(m => m.StartDate.Month == month.Month && m.StartDate.Year == month.Year);
            if (!string.IsNullOrEmpty(status))
            {
                matches = matches.Where(m => m.Status == status);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(m => m.Name.ToLower().Contains(keyword.ToLower()));
            }

            List<MeetingDTO> meetings = matches
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .OrderByDescending(m => m.StartDate)
                .Select(m => _mapper.Map<MeetingDTO>(m))
                .ToList();
            return View(meetings);
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
