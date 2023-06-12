using BirdClubInfoHub.Models;
using BirdClubInfoHub.Data;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public MeetingsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: FieldTripsController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        public IActionResult Register(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterConfirmed(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            MeetingRegistration meetingRegistration = new()
            {
                Meeting = meeting,
                User = user,
                PaymentReceived = true
            };
            _dbContext.MeetingRegistrations.Add(meetingRegistration);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "ClubEvents");
        }
    }
}
