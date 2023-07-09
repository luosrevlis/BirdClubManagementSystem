using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
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
            if (tournament.Date < tournament.RegistrationCloseDate)
            {
                ModelState.AddModelError("RegDateError", "Event cannot take place before registration is closed!");
            }
            if (!ModelState.IsValid)
            {
                return View(tournament);
            }
            tournament.Status = "Open";
            _dbContext.Tournaments.Add(tournament);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "ClubEvents");
        }

        // GET: TournamentsController/Edit/5
        public IActionResult Edit(int id)
        {
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
            if (tournament.Date < tournament.RegistrationCloseDate)
            {
                ModelState.AddModelError("RegDateError", "Event cannot take place before registration is closed!");
            }
            if (!ModelState.IsValid)
            {
                return View(tournament);
            }
            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: TournamentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.Tournaments.Remove(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", tournament.Name + " has been deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: TournamentsController/Close/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Close(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            tournament.Status = "Registrtion Closed";
            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration for " + tournament.Name + " has been closed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: TournamentsController/MarkAsEnded/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsEnded(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            tournament.Status = "Ended";
            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", tournament.Name + " has been marked as ended!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: TournamentsController/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            tournament.Status = "Cancelled";
            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", tournament.Name + " has been cancelled!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        public IActionResult EditHighlights(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null || tournament.Status != "Ended")
            {
                return NotFound();
            }
            return View(tournament);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHighlights(Tournament tournament)
        {
            Tournament? tournamentInDb = _dbContext.Tournaments.Find(tournament.Id);
            if (tournamentInDb == null || tournamentInDb.Status != "Ended")
            {
                return NotFound();
            }
            tournamentInDb.Highlights = tournament.Highlights;
            _dbContext.Tournaments.Update(tournamentInDb);
            _dbContext.SaveChanges();
            return RedirectToAction("Details", new { id = tournament.Id });
        }
    }
}
