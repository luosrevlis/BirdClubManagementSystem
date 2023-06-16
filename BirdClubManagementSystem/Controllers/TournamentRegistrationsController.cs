using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class TournamentRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public TournamentRegistrationsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: TournamentRegistrationsController
        public ActionResult Index(int tournamentId)
        {
            List<TournamentRegistration> registrations = _dbContext.TournamentRegistrations
                .Where(tr => tr.TournamentId == tournamentId).ToList();
            foreach (TournamentRegistration tr in registrations)
            {
                tr.Bird = _dbContext.Birds.Find(tr.BirdId)!;
                tr.Bird.User = _dbContext.Users.Find(tr.Bird.UserId)!;
                tr.Tournament = _dbContext.Tournaments.Find(tr.TournamentId)!;
            }
            return View(registrations);
        }

        // GET: TournamentRegistrationsController/Details/5
        public ActionResult Details(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            registration.Bird = _dbContext.Birds.Find(registration.BirdId)!;
            registration.Bird.User = _dbContext.Users.Find(registration.Bird.UserId)!;
            registration.Tournament = _dbContext.Tournaments.Find(registration.TournamentId)!;
            return View(registration);
        }

        // GET: TournamentRegistrationsController/Delete/5
        public ActionResult Delete(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            registration.Bird = _dbContext.Birds.Find(registration.BirdId)!;
            registration.Bird.User = _dbContext.Users.Find(registration.Bird.UserId)!;
            registration.Tournament = _dbContext.Tournaments.Find(registration.TournamentId)!;
            return View(registration);
        }

        // POST: TournamentRegistrationsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            _dbContext.TournamentRegistrations.Remove(registration);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId = registration.TournamentId }));
        }
    }
}
