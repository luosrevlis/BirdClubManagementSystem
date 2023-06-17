using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class MeetingRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public MeetingRegistrationsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: MeetingRegistrationsController
        public ActionResult Index(int meetingId)
        {
            List<MeetingRegistration> registrations = _dbContext.MeetingRegistrations
                .Where(mr => mr.MeetingId == meetingId).ToList();
            foreach (MeetingRegistration mr in registrations)
            {
                mr.User = _dbContext.Users.Find(mr.UserId)!;
                mr.Meeting = _dbContext.Meetings.Find(mr.MeetingId)!;
            }
            return View(registrations);
        }

        // GET: MeetingRegistrationsController/Details/5
        public ActionResult Details(int id)
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

        // GET: MeetingRegistrationsController/Delete/5
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

        // POST: MeetingRegistrationsController/Delete/5
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
            return RedirectToAction("Index", new RouteValueDictionary(new { meetingId = registration.MeetingId }));
        }
    }
}
