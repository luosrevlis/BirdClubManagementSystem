using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;

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
            //if bytearray is empty return default, 5 places (thumbnail x2 profile x2 bird profile)
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
                return NotFound();
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
            if (profilePicture != null)
            {
                using MemoryStream memoryStream = new();
                profilePicture.CopyTo(memoryStream);
                bird.ProfilePicture = memoryStream.ToArray();
            }
            _dbContext.Birds.Add(bird);
            _dbContext.SaveChanges();
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
                return NotFound();
            }
            return View(bird);
        }

        // POST: BirdsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bird bird, IFormFile profilePicture)
        {
            Bird? birdInDb = _dbContext.Birds.Find(bird.Id);
            if (birdInDb == null || birdInDb.UserId != HttpContext.Session.GetInt32("USER_ID"))
            {
                return NotFound();
            }
            birdInDb.Name = bird.Name;
            birdInDb.Description = bird.Description;
            birdInDb.Species = bird.Species;
            if (profilePicture != null)
            {
                using MemoryStream memoryStream = new();
                profilePicture.CopyTo(memoryStream);
                birdInDb.ProfilePicture = memoryStream.ToArray();
            }
            _dbContext.Birds.Update(birdInDb);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authenticated]
        // GET: BirdsController/Delete/5
        public ActionResult Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            Bird? bird = _dbContext.Birds.Find(id);
            if (bird == null || bird.UserId != userId)
            {
                return NotFound();
            }
            return View(bird);
        }

        // POST: BirdsController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePOST(int id)
        {
            Bird? bird = _dbContext.Birds.Find(id);
            if (bird == null)
            {
                return NotFound();
            }
            _dbContext.Birds.Remove(bird);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
