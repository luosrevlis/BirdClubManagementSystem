using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class MeetingRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public MeetingRegistrationsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<MeetingRegistration> registrations = _dbContext.MeetingRegistrations
                .Where(mr => mr.UserId == userId).ToList();
            foreach (MeetingRegistration mr in registrations)
            {
                mr.Meeting = _dbContext.Meetings.Find(mr.MeetingId)!;
                mr.User = user;
            }
            registrations.RemoveAll(mr => mr.Meeting.Status != "Open" && mr.Meeting.Status != "Registration Closed");
            return View(registrations);
        }

        [Authenticated]
        public IActionResult Register(int meetingId)
        {
            Meeting? meeting = _dbContext.Meetings.Find(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            MeetingRegistration? registration = _dbContext.MeetingRegistrations
                .FirstOrDefault(ftr => ftr.MeetingId == meetingId && ftr.UserId == userId);
            // If already registered, notify
            if (registration != null)
            {
                return View("AlreadyRegistered");
            }
            registration = new() { Meeting = meeting };
            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(MeetingRegistration registration)
        {
            int userId = registration.UserId;
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            int meetingId = registration.MeetingId;
            Meeting? meeting = _dbContext.Meetings.Find(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            registration.User = user;
            registration.Meeting = meeting;
            _dbContext.MeetingRegistrations.Add(registration);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "ClubEvents");
        }

        [Authenticated]
        public IActionResult Delete(int id)
        {
            MeetingRegistration? registration = _dbContext.MeetingRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            registration.Meeting = _dbContext.Meetings.Find(registration.MeetingId)!;
            registration.User = _dbContext.Users.Find(registration.UserId)!;
            return View(registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            MeetingRegistration? registration = _dbContext.MeetingRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            _dbContext.MeetingRegistrations.Remove(registration);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
