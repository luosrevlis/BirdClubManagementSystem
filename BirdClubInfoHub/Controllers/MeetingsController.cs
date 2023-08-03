using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Statuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public MeetingsController
            (BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(DateTime month = new DateTime(), int page = 1, string keyword = "", string status = "")
        {
            if (month.Ticks < 1)
            {
                month = DateTime.Now;
            }
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

            int maxPage = (int)Math.Ceiling(matches.Count() / (double)PageSize);
            if (page > maxPage)
            {
                page = maxPage;
            }
            if (page < 1)
            {
                page = 1;
            }

            List<MeetingDTO> meetings = matches
                .OrderByDescending(m => m.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(m => _mapper.Map<MeetingDTO>(m))
                .ToList();

            ViewBag.Month = month;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.Status = status;
            ViewBag.MaxPage = maxPage;
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

            MeetingDTO dto = _mapper.Map<MeetingDTO>(meeting);
            dto.MeetingRegistrations = _dbContext.MeetingRegistrations
                .Where(mr => mr.MeetingId == id)
                .Include(mr => mr.User)
                .Select(mr => _mapper.Map<MeetingRegistrationDTO>(mr))
                .ToList();

            // not open
            if (meeting.Status != EventStatuses.RegOpened)
            {
                ViewBag.Status = "Unavailable";
                return View(dto);
            }

            // open but not logged in
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                ViewBag.Status = "Unauth";
                return View(dto);
            }

            // reg limit reached
            int regCount = dto.MeetingRegistrations.Count;
            if (regCount >= dto.RegLimit)
            {
                ViewBag.Status = "NoSlots";
                return View(dto);
            }

            // open, logged in, already registered
            if (_dbContext.MeetingRegistrations.FirstOrDefault(x => x.MeetingId == id && x.UserId == userId) != null)
            {
                ViewBag.Status = "Registered";
                return View(dto);
            }

            // open, logged in, not registered
            ViewBag.Status = "Available";
            return View(dto);
        }
    }
}
