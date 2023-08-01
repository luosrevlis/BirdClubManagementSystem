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

        // GET: MeetingRegistrationsController
        public ActionResult Index(int meetingId, int page = 1, string keyword = "")
        {
            HttpContext.Session.SetInt32("MEETING_ID", meetingId);

            IQueryable<MeetingRegistration> matches = _dbContext.MeetingRegistrations
                .Where(mr => mr.MeetingId == meetingId)
                .Include(mr => mr.User);
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(mr => mr.User.Name.ToLower().Contains(keyword.ToLower()));
            }

            List<MeetingRegistrationDTO> registrations = matches
                .OrderByDescending(mr => mr.DateCreated)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(mr => _mapper.Map<MeetingRegistrationDTO>(mr))
                .ToList();
            return View(registrations);
        }

        // POST: MeetingRegistrationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            int meetingId = HttpContext.Session.GetInt32("MEETING_ID") ?? 0;
            MeetingRegistration? registration = _dbContext.MeetingRegistrations.Find(id);
            if (registration == null || registration.MeetingId != meetingId)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { meetingId }));
            }
            _dbContext.MeetingRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Participant removed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { meetingId }));
        }
    }
}
