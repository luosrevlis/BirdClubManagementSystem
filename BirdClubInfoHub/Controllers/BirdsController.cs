using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BirdClubInfoHub.Controllers
{
    public class BirdsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public BirdsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult GetImageFromBytes(int id)
        {
            Bird? bird = _dbContext.Birds.Find(id);
            if (bird == null)
            {
                return NotFound();
            }
            //if bytearray is empty return default
            if (bird.ProfilePicture.Length == 0)
            {
                return File("/img/placeholder/bird.png", "image/png");
            }
            return File(bird.ProfilePicture, "image/png");
        }

        // GET: BirdsController
        [Authenticated]
        public ActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            List<Bird> birds = _dbContext.Birds.Where(bird => bird.UserId == userId).ToList();
            return View(birds);
        }

        [Authenticated]
        public ActionResult Details(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            Bird? bird = _dbContext.Birds.Find(id);
            if (bird == null || bird.UserId != userId)
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            return View(bird);
        }

        // GET: BirdsController/Create
        [Authenticated]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BirdsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bird bird, IFormFile profilePicture)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            bird.User = user;
            if (bird.Species.IsNullOrEmpty())
            {
                bird.Species = "Unknown";
            }
            if (bird.Description.IsNullOrEmpty())
            {
                bird.Description = "No description";
            }
            if (profilePicture != null)
            {
                using MemoryStream memoryStream = new();
                profilePicture.CopyTo(memoryStream);
                bird.ProfilePicture = memoryStream.ToArray();
            }
            _dbContext.Birds.Add(bird);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Bird added!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        // GET: BirdsController/Edit/5
        [Authenticated]
        public ActionResult Edit(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            Bird? bird = _dbContext.Birds.Find(id);
            if (bird == null || bird.UserId != userId)
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            return View(bird);
        }

        // POST: BirdsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bird bird, IFormFile profilePicture)
        {
            int? userId =  HttpContext.Session.GetInt32("USER_ID");
            Bird? birdInDb = _dbContext.Birds.Find(bird.Id);
            if (birdInDb == null || birdInDb.UserId != userId)
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            birdInDb.Name = bird.Name;
            birdInDb.Species = bird.Species.IsNullOrEmpty() ? "Unknown" : bird.Species;
            birdInDb.Description = bird.Description.IsNullOrEmpty() ? "No description" : bird.Description;
            if (profilePicture != null)
            {
                using MemoryStream memoryStream = new();
                profilePicture.CopyTo(memoryStream);
                birdInDb.ProfilePicture = memoryStream.ToArray();
            }
            _dbContext.Birds.Update(birdInDb);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Bird updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        // POST: BirdsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            Bird? bird = _dbContext.Birds.Find(id);
            if (bird == null || bird.UserId != userId)
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            _dbContext.Birds.Remove(bird);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Bird deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        public ActionResult ViewAchievements(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            Bird? bird = _dbContext.Birds.Find(id);
            if (bird == null || bird.UserId != userId)
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            bird.TournamentStandings = _dbContext.TournamentStandings
                .Where(ts => ts.BirdId == id)
                .Include(ts => ts.Tournament)
                .ToList();
            return View(bird);
        }
    }
}
