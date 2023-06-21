using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class TournamentStandingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public TournamentStandingsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: TournamentStandingsController
        public ActionResult Index(int tournamentId)
        {
            List<TournamentStanding> standings = _dbContext.TournamentStandings
                .Where(ts => ts.TournamentId == tournamentId).ToList();
            foreach (TournamentStanding ts in standings)
            {
                ts.Bird = _dbContext.Birds.Find(ts.BirdId)!;
                ts.Bird.User = _dbContext.Users.Find(ts.Bird.UserId)!;
                ts.Tournament = _dbContext.Tournaments.Find(ts.TournamentId)!;
            }
            ViewBag.TournamentId = tournamentId;
            return View(standings);
        }

        // GET: TournamentStandingsController/Details/5
        public ActionResult Details(int id)
        {
            TournamentStanding? standing = _dbContext.TournamentStandings.Find(id);
            if (standing == null)
            {
                return NotFound();
            }
            standing.Bird = _dbContext.Birds.Find(standing.BirdId)!;
            standing.Bird.User = _dbContext.Users.Find(standing.Bird.UserId)!;
            standing.Tournament = _dbContext.Tournaments.Find(standing.TournamentId)!;
            return View(standing);
        }

        // GET: TournamentStandingsController/Create
        public ActionResult Create(int tournamentId)
        {
            HashSet<int> rankedBirds = _dbContext.TournamentStandings
                .Where(ts => ts.TournamentId == tournamentId).Select(ts => ts.BirdId).ToHashSet();
            HashSet<int> registeredBirds = _dbContext.TournamentRegistrations
                .Where(tr => tr.TournamentId == tournamentId).Select(tr => tr.BirdId).ToHashSet();
            List<Bird> birds = _dbContext.Birds
                .Where(bird => registeredBirds.Contains(bird.Id) && !rankedBirds.Contains(bird.Id)).ToList();
            SelectList birdOptions = new(birds, nameof(Bird.Id), nameof(Bird.Name));
            ViewBag.BirdOptions = birdOptions;
            return View(new TournamentStanding() { TournamentId = tournamentId });
        }

        // POST: TournamentStandingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TournamentStanding standing)
        {
            standing.Tournament = _dbContext.Tournaments.Find(standing.TournamentId)!;
            standing.Bird = _dbContext.Birds.Find(standing.BirdId)!;
            _dbContext.TournamentStandings.Add(standing);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId = standing.TournamentId }));
        }

        // GET: TournamentStandingsController/Edit/5
        public ActionResult Edit(int id)
        {
            TournamentStanding? standing = _dbContext.TournamentStandings.Find(id);
            if (standing == null)
            {
                return NotFound();
            }
            int tournamentId = standing.TournamentId;
            HashSet<int> rankedBirds = _dbContext.TournamentStandings
                .Where(ts => ts.TournamentId == tournamentId).Select(ts => ts.BirdId).ToHashSet();
            HashSet<int> registeredBirds = _dbContext.TournamentRegistrations
                .Where(tr => tr.TournamentId == tournamentId).Select(tr => tr.BirdId).ToHashSet();
            List<Bird> birds = _dbContext.Birds
                .Where(bird => registeredBirds.Contains(bird.Id) && !rankedBirds.Contains(bird.Id)).ToList();
            standing.Bird = _dbContext.Birds.Find(standing.BirdId)!;
            birds.Add(standing.Bird);
            SelectList birdOptions = new(birds, nameof(Bird.Id), nameof(Bird.Name));
            ViewBag.BirdOptions = birdOptions;
            return View(standing);
        }

        // POST: TournamentStandingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TournamentStanding standing)
        {
            standing.Tournament = _dbContext.Tournaments.Find(standing.TournamentId)!;
            standing.Bird = _dbContext.Birds.Find(standing.BirdId)!;
            _dbContext.TournamentStandings.Update(standing);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId = standing.TournamentId }));
        }

        // GET: TournamentStandingsController/Delete/5
        public ActionResult Delete(int id)
        {
            TournamentStanding? standing = _dbContext.TournamentStandings.Find(id);
            if (standing == null)
            {
                return NotFound();
            }
            standing.Bird = _dbContext.Birds.Find(standing.BirdId)!;
            standing.Bird.User = _dbContext.Users.Find(standing.Bird.UserId)!;
            standing.Tournament = _dbContext.Tournaments.Find(standing.TournamentId)!;
            return View(standing);
        }

        // POST: TournamentStandingsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TournamentStanding? standing = _dbContext.TournamentStandings.Find(id);
            if (standing == null)
            {
                return NotFound();
            }
            _dbContext.Remove(standing);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId = standing.TournamentId }));
        }
    }
}
