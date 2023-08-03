using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Statuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    public class FieldTripsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public FieldTripsController
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
            IQueryable<FieldTrip> matches = _dbContext.FieldTrips
                .Where(ft => ft.StartDate.Month == month.Month && ft.StartDate.Year == month.Year);
            if (!string.IsNullOrEmpty(status))
            {
                matches = matches.Where(ft => ft.Status == status);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(ft => ft.Name.ToLower().Contains(keyword.ToLower()));
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

            List<FieldTripDTO> fieldTrips = matches
                .OrderByDescending(ft => ft.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(ft => _mapper.Map<FieldTripDTO>(ft))
                .ToList();

            ViewBag.Month = month;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.Status = status;
            ViewBag.MaxPage = maxPage;
            return View(fieldTrips);
        }

        // GET: FieldTripsController/Details/5
        public IActionResult Details(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }

            FieldTripDTO dto = _mapper.Map<FieldTripDTO>(fieldTrip);
            dto.FieldTripRegistrations = _dbContext.FieldTripRegistrations
                .Where(ftr => ftr.FieldTripId == id)
                .Include(ftr => ftr.User)
                .Select(ftr => _mapper.Map<FieldTripRegistrationDTO>(ftr))
                .ToList();

            // not open
            if (fieldTrip.Status != EventStatuses.RegOpened)
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
            int regCount = dto.FieldTripRegistrations.Count;
            if (regCount >= dto.RegLimit)
            {
                ViewBag.Status = "NoSlots";
                return View(dto);
            }

            // open, logged in, already registered
            if (_dbContext.FieldTripRegistrations.FirstOrDefault(x => x.FieldTripId == id && x.UserId == userId) != null)
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
