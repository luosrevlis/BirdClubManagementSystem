using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class MeetingRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public MeetingRegistrationsController
            (BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1, string keyword = "")
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            IQueryable<MeetingRegistration> matches = _dbContext.MeetingRegistrations
                .Where(mr => mr.UserId == userId)
                .Include(mr => mr.Meeting);
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(mr => mr.Meeting.Name.ToLower().Contains(keyword.ToLower()));
            }

            List<MeetingRegistrationDTO> registrations = matches
                .OrderByDescending(mr => mr.DateCreated)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(mr => _mapper.Map<MeetingRegistrationDTO>(mr))
                .ToList();
            //registrations.RemoveAll(mr => mr.Meeting.Status != "Open" && mr.Meeting.Status != "Registration Closed");
            return View(registrations);
        }

        public IActionResult Register(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            MeetingRegistration registration = new()
            {
                User = user,
                Meeting = meeting
            };
            _dbContext.MeetingRegistrations.Add(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration success!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            MeetingRegistration? registration = _dbContext.MeetingRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Registration not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            _dbContext.MeetingRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration cancelled!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
