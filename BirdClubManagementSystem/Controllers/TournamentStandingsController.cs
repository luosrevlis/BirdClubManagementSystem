using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class TournamentStandingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public TournamentStandingsController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: TournamentStandingsController
        public ActionResult Index(int tournamentId, int page = 1, string keyword = "", string placement = "")
        {
            HttpContext.Session.SetInt32("TOURNAMENT_ID", tournamentId);

            IQueryable<TournamentStanding> matches = _dbContext.TournamentStandings
                .Where(ts => ts.TournamentId == tournamentId)
                .Include(ts => ts.Bird)
                .ThenInclude(bird => bird.User);
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches
                    .Where(ts => ts.Bird.Name.ToLower().Contains(keyword.ToLower())
                    || ts.Bird.User.Name.ToLower().Contains(keyword.ToLower()));
            }
            if (!string.IsNullOrEmpty(placement))
            {
                matches = matches.Where(ts => ts.Placement == placement);
            }

            int maxPage = (int)Math.Ceiling(matches.Count() / (double)PageSize);
            if (page > maxPage)
            {
                page = maxPage;
            }
            if (page < 1)
            {
                page = 1;
            }

            List<TournamentStandingDTO> standings = matches
                .OrderBy(ts => ts.Placement)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(ts => _mapper.Map<TournamentStandingDTO>(ts))
                .ToList();

            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.Placement = placement;
            ViewBag.MaxPage = maxPage;
            return View(standings);
        }

        // GET: TournamentStandingsController/Details/5
        public ActionResult Details(int id)
        {
            int tournamentId = HttpContext.Session.GetInt32("TOURNAMENT_ID") ?? 0;
            TournamentStanding? standing = _dbContext.TournamentStandings.Find(id);
            if (standing == null || standing.TournamentId != tournamentId)
            {
                TempData.Add("notification", "Ranking information not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
            }
            standing.Bird = _dbContext.Birds.Find(standing.BirdId)!;
            standing.Bird.User = _dbContext.Users.Find(standing.Bird.UserId)!;
            standing.Tournament = _dbContext.Tournaments.Find(standing.TournamentId)!;
            return View(_mapper.Map<TournamentStandingDTO>(standing));
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

            TournamentStandingDTO dto = new();
            dto.Tournament.Id = tournamentId;
            return View(dto);
        }

        // POST: TournamentStandingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TournamentStandingDTO dto)
        {
            TournamentStanding standing = _mapper.Map<TournamentStanding>(dto);
            standing.Tournament = _dbContext.Tournaments.Find(dto.Tournament.Id)!;
            standing.Bird = _dbContext.Birds.Find(dto.Bird.Id)!;
            _dbContext.TournamentStandings.Add(standing);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Ranking information added!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId = dto.Tournament.Id }));
        }

        // GET: TournamentStandingsController/Edit/5
        public ActionResult Edit(int id)
        {
            int tournamentId = HttpContext.Session.GetInt32("TOURNAMENT_ID") ?? 0;
            TournamentStanding? standing = _dbContext.TournamentStandings.Find(id);
            if (standing == null || standing.TournamentId != tournamentId)
            {
                TempData.Add("notification", "Ranking information not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
            }

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

            TournamentStandingDTO dto = _mapper.Map<TournamentStandingDTO>(standing);
            dto.Tournament.Id = tournamentId;
            return View(dto);
        }

        // POST: TournamentStandingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TournamentStandingDTO dto)
        {
            int tournamentId = HttpContext.Session.GetInt32("TOURNAMENT_ID") ?? 0;
            TournamentStanding? standing = _dbContext.TournamentStandings.Find(dto.Id);
            if (standing == null || standing.TournamentId != tournamentId)
            {
                TempData.Add("notification", "Ranking information not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
            }

            standing.Bird = _dbContext.Birds.Find(dto.Bird.Id)!;
            standing.Placement = dto.Placement;
            _dbContext.TournamentStandings.Update(standing);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Ranking information updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
        }

        // POST: TournamentStandingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int tournamentId = HttpContext.Session.GetInt32("TOURNAMENT_ID") ?? 0;
            TournamentStanding? standing = _dbContext.TournamentStandings.Find(id);
            if (standing == null || standing.TournamentId != tournamentId)
            {
                TempData.Add("notification", "Ranking information not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
            }
            _dbContext.Remove(standing);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Ranking information deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
        }
    }
}
