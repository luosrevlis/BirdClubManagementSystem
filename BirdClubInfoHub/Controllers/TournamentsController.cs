using Microsoft.AspNetCore.Mvc;
using BirdClubInfoHub.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BirdClubInfoHub.Models.Entities;
using AutoMapper;
using BirdClubInfoHub.Models.DTOs;

namespace BirdClubInfoHub.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public TournamentsController(
            BcmsDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            if (tournament.Status == "Ended")
            {
                tournament.TournamentStandings = _dbContext.TournamentStandings
                    .Where(ts => ts.TournamentId == id)
                    .Include(ts => ts.Bird)
                    .ThenInclude(bird => bird.User)
                    .ToList();
            }

            // if not open, return unavailable
            if (tournament.Status != "Open")
            {
                ViewBag.Status = "Unavailable";
                return View(_mapper.Map<TournamentDTO>(tournament));
            }

            // open but not logged in, return unauth
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                ViewBag.Status = "Unauth";
                return View(_mapper.Map<TournamentDTO>(tournament));
            }

            // open, logged in, no eligible birds
            HashSet<int> registeredBirds = _dbContext.TournamentRegistrations
                .Where(tr => tr.TournamentId == id)
                .Select(tr => tr.BirdId).ToHashSet();
            List<Bird> birds = _dbContext.Birds
                .Where(bird => bird.UserId == userId && !registeredBirds.Contains(bird.Id))
                .ToList();
            if (!birds.Any())
            {
                ViewBag.Status = "Registered";
                return View(_mapper.Map<TournamentDTO>(tournament));
            }

            // open, logged in, eligible birds
            ViewBag.Status = "Available";
            SelectList birdOptions = new(birds, nameof(Bird.Id), nameof(Bird.Name));

            // kiểm bn chim đk chưa trả tiền
            // nếu >2, viewbag.status = ...

            ViewBag.BirdOptions = birdOptions;
            return View(_mapper.Map<TournamentDTO>(tournament));
        }
    }
}
