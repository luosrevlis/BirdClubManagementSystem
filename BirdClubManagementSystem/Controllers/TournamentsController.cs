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

        // GET: TournamentsController/Details/5
        public IActionResult Details(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
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
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(tournament);
            }
            tournament.Status = "Open";
            _dbContext.Tournaments.Add(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", tournament.Name + " has been created!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // GET: TournamentsController/Edit/5
        public IActionResult Edit(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
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
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(tournament);
            }
            Tournament? tournamentInDb = _dbContext.Tournaments.Find(tournament.Id);
            if (tournamentInDb == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            tournamentInDb.Name = tournament.Name;
            tournamentInDb.Date = tournament.Date;
            tournamentInDb.RegistrationCloseDate = tournament.RegistrationCloseDate;
            tournamentInDb.Description = tournament.Description;
            _dbContext.Tournaments.Update(tournamentInDb);
            _dbContext.SaveChanges();

            TempData.Add("notification", tournamentInDb.Name + " has been updated!");
            TempData.Add("success", "");
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
            tournament.Status = "Registration Closed";
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
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
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
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            tournamentInDb.Highlights = tournament.Highlights;
            _dbContext.Tournaments.Update(tournamentInDb);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Highlights has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Details", new { id = tournament.Id });
        }
    }
}
