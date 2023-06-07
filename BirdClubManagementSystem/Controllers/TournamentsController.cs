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
        public ActionResult Index()
        {
            return View();
        }

        // GET: TournamentsController/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: TournamentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TournamentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TournamentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TournamentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TournamentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
