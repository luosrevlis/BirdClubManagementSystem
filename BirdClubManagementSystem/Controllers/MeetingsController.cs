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
            return View(_mapper.Map<MeetingDTO>(meeting));
        }

        // GET: MeetingsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MeetingDTO dto)
        {
            if (dto.StartDate < dto.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(dto);
            }

            Meeting meeting = _mapper.Map<Meeting>(dto);
            if (meeting.RegOpenDate < DateTime.Now && meeting.RegCloseDate > DateTime.Now)
            {
                meeting.Status = EventStatuses.RegOpened;
            }
            else
            {
                meeting.Status = EventStatuses.RegClosed;
            }
            if (string.IsNullOrEmpty(meeting.Address))
            {
                meeting.Address = "At Club";
            }
            _dbContext.Meetings.Add(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", meeting.Name + " has been created!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // GET: MeetingsController/Edit/5
        public IActionResult Edit(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(_mapper.Map<MeetingDTO>(meeting));
        }

        // POST: MeetingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MeetingDTO dto)
        {
            if (dto.StartDate < dto.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(dto);
            }

            Meeting? meeting = _dbContext.Meetings.Find(dto.Id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            meeting.Name = dto.Name;
            meeting.RegOpenDate = dto.RegOpenDate;
            meeting.RegCloseDate = dto.RegCloseDate;
            meeting.StartDate = dto.StartDate;
            meeting.ExpectedEndDate = dto.ExpectedEndDate;
            meeting.Address = dto.Address ?? "At Club";
            meeting.RegLimit = dto.RegLimit;
            meeting.Description = dto.Description;
            if (meeting.RegOpenDate < DateTime.Now && meeting.RegCloseDate > DateTime.Now)
            {
                meeting.Status = EventStatuses.RegOpened;
            }
            else
            {
                meeting.Status = EventStatuses.RegClosed;
            }
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", meeting.Name + " has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: MeetingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.Meetings.Remove(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", meeting.Name + " has been deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: MeetingController/UpdateStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, string statusCode)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            meeting.Status = statusCode;
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Status updated!");
            TempData.Add("success", "");
            return RedirectToAction("Details", new { id });
        }

        public IActionResult EditHighlights(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null || meeting.Status != EventStatuses.Ended)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(_mapper.Map<MeetingDTO>(meeting));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHighlights(MeetingDTO dto)
        {
            Meeting? meeting = _dbContext.Meetings.Find(dto.Id);
            if (meeting == null || meeting.Status != EventStatuses.Ended)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            meeting.Highlights = dto.Highlights;
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Highlights has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Details", new { id = dto.Id });
        }
    }
}
