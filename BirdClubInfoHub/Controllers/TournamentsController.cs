using Microsoft.AspNetCore.Mvc;
using BirdClubInfoHub.Models;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdClubInfoHub.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public TournamentsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: TournamentsController
        public IActionResult Index()
        {
            return View();
        }

        // GET: TournamentsController/Details/5
        public IActionResult Details(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }

        [Authenticated]
        public IActionResult Register(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }
            User? user = _dbContext.Users.Find(HttpContext.Session.GetInt32("USER_ID"));
            if (user == null)
            {
                return NotFound();
            }
            IEnumerable<Bird> birds = _dbContext.Birds.Where(b => b.UserId == user.Id);
            if (!birds.Any())
            {
                return RedirectToAction("Index", "Home");
            }
            SelectList options = new(birds, nameof(Bird.Id), nameof(Bird.Id));
            ViewBag.Options = options;
            TournamentRegistration registration = new() { Tournament = tournament };
            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TournamentRegistration registration)
        {
            int birdId = registration.BirdId;
            Bird? bird = _dbContext.Birds.Find(birdId);
            if (bird == null)
            {
                return NotFound();
            }
            registration.Bird = bird;
            _dbContext.TournamentRegistrations.Add(registration);
            return RedirectToAction("Index", "ClubEvents");
        }
    }
}
