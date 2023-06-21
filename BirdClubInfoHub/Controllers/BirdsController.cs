using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class BirdsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public BirdsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: BirdsController
        public ActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("USER_ID")!;
            //Bird? bird = _dbContext.Birds.Find();
            IEnumerable<Bird> objBirdsList = _dbContext.Birds.Where(bird => bird.UserId == userId);
            return View(objBirdsList);
        }

        // GET: BirdsController/Create
        public ActionResult Create(int id)
        {
            id = (int)HttpContext.Session.GetInt32("USER_ID")!;
            return View(new Bird() { UserId = id });
        }

        // POST: BirdsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bird bird)
        {
            if (ModelState.IsValid)
            {
                bird.User = _dbContext.Users.Find(bird.UserId)!;
                _dbContext.Birds.Add(bird);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: BirdsController/Edit/5
        public ActionResult Edit(int? id)
        {
            int userId = (int)HttpContext.Session.GetInt32("USER_ID")!;
            var bird = _dbContext.Birds.Find(id)!;
            if (bird.UserId != userId)
            {
                return RedirectToAction("Index");
            }
            if (bird == null)
            {
                return NotFound();
            }
            return View(bird);
        }

        // POST: BirdsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bird bird)
        {
            if (bird == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bird.User = _dbContext.Users.Find(bird.UserId)!;
                _dbContext.Birds.Update(bird);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View() ;
        }

        // GET: BirdsController/Delete/5
        public ActionResult Delete(int? id)
        {
            int userId = (int)HttpContext.Session.GetInt32("USER_ID")!;
            var bird = _dbContext.Birds.Find(id);
            if (bird == null)
            {
                return NotFound();
            }
            return View(bird);
        }

        // POST: BirdsController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePOST(int? id)
        {
            var bird = _dbContext.Birds.Find(id);
            if (bird == null)
            {
                return NotFound();
            }
            bird.User = _dbContext.Users.Find(bird.UserId);
            _dbContext.Birds.Remove(bird);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
