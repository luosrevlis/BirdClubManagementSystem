using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Statuses;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
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
            return View(_mapper.Map<FieldTripDTO>(fieldTrip));
        }

        // GET: FieldTripsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FieldTripsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FieldTripDTO dto)
        {
            if (dto.StartDate < dto.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(dto);
            }

            FieldTrip fieldTrip = _mapper.Map<FieldTrip>(dto);
            if (fieldTrip.RegOpenDate < DateTime.Now && fieldTrip.RegCloseDate > DateTime.Now)
            {
                fieldTrip.Status = EventStatuses.RegOpened;
            }
            else
            {
                fieldTrip.Status = EventStatuses.RegClosed;
            }
            _dbContext.FieldTrips.Add(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", fieldTrip.Name + " has been created!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // GET: FieldTripsController/Edit/5
        public IActionResult Edit(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(_mapper.Map<FieldTripDTO>(fieldTrip));
        }

        // POST: FieldTripsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FieldTripDTO dto)
        {
            if (dto.StartDate < dto.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(dto);
            }
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(dto.Id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            fieldTrip.Name = dto.Name;
            fieldTrip.RegOpenDate = dto.RegOpenDate;
            fieldTrip.RegCloseDate = dto.RegCloseDate;
            fieldTrip.StartDate = dto.StartDate;
            fieldTrip.ExpectedEndDate = dto.ExpectedEndDate;
            fieldTrip.Address = dto.Address;
            fieldTrip.RegLimit = dto.RegLimit;
            fieldTrip.Description = dto.Description;
            fieldTrip.Fee = dto.Fee;
            if (fieldTrip.RegOpenDate < DateTime.Now && fieldTrip.RegCloseDate > DateTime.Now)
            {
                fieldTrip.Status = EventStatuses.RegOpened;
            }
            else
            {
                fieldTrip.Status = EventStatuses.RegClosed;
            }
            _dbContext.FieldTrips.Update(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", fieldTrip.Name + " has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: FieldTripsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.FieldTrips.Remove(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", fieldTrip.Name + " has been deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: FieldTripsController/Close/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, string statusCode)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            fieldTrip.Status = statusCode;
            _dbContext.FieldTrips.Update(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Status updated!");
            TempData.Add("success", "");
            return RedirectToAction("Details", new { id });
        }

        public IActionResult EditHighlights(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null || fieldTrip.Status != EventStatuses.Ended)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(fieldTrip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHighlights(FieldTripDTO dto)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(dto.Id);
            if (fieldTrip == null || fieldTrip.Status != EventStatuses.Ended)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            fieldTrip.Highlights = dto.Highlights;
            _dbContext.FieldTrips.Update(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Highlights has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Details", new { id = dto.Id });
        }
    }
}
