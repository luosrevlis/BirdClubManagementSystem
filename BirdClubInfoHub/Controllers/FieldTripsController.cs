using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class FieldTripsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public FieldTripsController(BcmsDbContext dbContext, IMapper mapper)
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
            IQueryable<FieldTrip> matches = _dbContext.FieldTrips
                .Where(ft => ft.StartDate.HasValue
                    && ft.StartDate.Value.Month == month.Month
                    && ft.StartDate.Value.Year == month.Year);
            if (!string.IsNullOrEmpty(status))
            {
                matches = matches.Where(ft => ft.Status == status);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(ft => ft.Name.ToLower().Contains(keyword.ToLower()));
            }

            List<FieldTripDTO> fieldTrips = matches
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .OrderByDescending(ft => ft.StartDate)
                .Select(ft => _mapper.Map<FieldTripDTO>(ft))
                .ToList();
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

            // if not open, return unavailable
            if (fieldTrip.Status != "Open")
            {
                ViewBag.Status = "Unavailable";
                return View(fieldTrip);
            }

            // open but not logged in, return unauth
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                ViewBag.Status = "Unauth";
                return View(fieldTrip);
            }
            
            // open, logged in, already registered
            if (_dbContext.FieldTripRegistrations.FirstOrDefault(x => x.FieldTripId == id && x.UserId == userId) != null)
            {
                ViewBag.Status = "Registered";
                return View(fieldTrip);
            }

            // open, logged in, not registered
            ViewBag.Status = "Available";
            return View(fieldTrip);
        }
    }
}
