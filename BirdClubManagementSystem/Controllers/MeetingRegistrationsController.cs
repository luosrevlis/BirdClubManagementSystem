using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
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

        public MeetingRegistrationsController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: MeetingRegistrationsController
        public ActionResult Index(int meetingId)
        {
            List<MeetingRegistration> registrations = _dbContext.MeetingRegistrations
                .Where(mr => mr.MeetingId == meetingId)
                .Include(mr => mr.User)
                .Include(mr => mr.Meeting)
                .ToList();
            return View(registrations);
        }

        // POST: MeetingRegistrationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            MeetingRegistration? registration = _dbContext.MeetingRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.MeetingRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Participant removed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { meetingId = registration.MeetingId }));
        }
    }
}
