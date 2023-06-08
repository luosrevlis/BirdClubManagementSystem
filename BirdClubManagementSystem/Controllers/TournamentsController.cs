using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
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
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }

        // GET: TournamentsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TournamentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Tournaments.Add(tournament);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(tournament);
        }

        // GET: TournamentsController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }

        // POST: TournamentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Tournaments.Update(tournament);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(tournament);
        }

        // GET: TournamentsController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }

        // POST: TournamentsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }
            _dbContext.Tournaments.Remove(tournament);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "ClubEvents");
        }
    }
}
