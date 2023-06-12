using BirdClubInfoHub.Models;
using BirdClubInfoHub.Data;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class FieldTripsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public FieldTripsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: FieldTripsController
        public IActionResult Index()
        {
            return View();
        }

        // GET: FieldTripsController/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            return View(fieldTrip);
        }

        public IActionResult Register(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            return View(fieldTrip);
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
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
            FieldTripRegistration fieldTripRegistration = new() {
                FieldTrip = fieldTrip,
                User = user,
                PaymentReceived = true
            };
            _dbContext.FieldTripRegistrations.Add(fieldTripRegistration);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "ClubEvents");
        }
    }
}
