using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Statuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public TournamentsController
            (BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(DateTime month = new DateTime(), int page = 1, string keyword = "", string status = "")
        {
            if (month.Ticks < 1)
            {
                month = DateTime.Now;
            }
            IQueryable<Tournament> matches = _dbContext.Tournaments
                .Where(t => t.StartDate.Month == month.Month && t.StartDate.Year == month.Year);
            if (!string.IsNullOrEmpty(status))
            {
                matches = matches.Where(t => t.Status == status);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(t => t.Name.ToLower().Contains(keyword.ToLower()));
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

            List<TournamentDTO> tournaments = matches
                .OrderByDescending(t => t.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(t => _mapper.Map<TournamentDTO>(t))
                .ToList();

            ViewBag.Month = month;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.Status = status;
            ViewBag.MaxPage = maxPage;
            return View(tournaments);
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

            TournamentDTO dto = _mapper.Map<TournamentDTO>(tournament);
            dto.TournamentRegistrations = _dbContext.TournamentRegistrations
                .Where(tr => tr.TournamentId == id)
                .Include(tr => tr.Bird)
                .ThenInclude(tr => tr.User)
                .Select(tr => _mapper.Map<TournamentRegistrationDTO>(tr))
                .ToList();
            if (dto.Status == EventStatuses.Ended)
            {
                dto.TournamentStandings = _dbContext.TournamentStandings
                    .Where(ts => ts.TournamentId == id)
                    .Include(ts => ts.Bird)
                    .ThenInclude(bird => bird.User)
                    .Select(ts => _mapper.Map<TournamentStandingDTO>(ts))
                    .ToList();
            }

            // not open
            if (dto.Status != EventStatuses.RegOpened)
            {
                ViewBag.Status = "Unavailable";
                return View(dto);
            }

            // open but not logged in
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                ViewBag.Status = "Unauth";
                return View(dto);
            }

            // reg limit reached
            int regCount = dto.TournamentRegistrations.Count;
            if (regCount >= dto.RegLimit)
            {
                ViewBag.Status = "NoSlots";
                return View(dto);
            }

            // open, logged in, no eligible birds
            IQueryable<TournamentRegistration> registrations = _dbContext.TournamentRegistrations
                .Include(tr => tr.Bird)
                .Where(tr => tr.TournamentId == id && tr.Bird.UserId == userId);

            HashSet<int> regIds = registrations.Select(tr => tr.BirdId).ToHashSet();
            List<Bird> birds = _dbContext.Birds
                .Where(bird => bird.UserId == userId && !regIds.Contains(bird.Id))
                .ToList();
            if (!birds.Any())
            {
                ViewBag.Status = "Registered";
                return View(dto);
            }

            // open, logged in, eligible birds
            SelectList birdOptions = new(birds, nameof(Bird.Id), nameof(Bird.Name));
            ViewBag.BirdOptions = birdOptions;

            // 2 or more unpaid registrations
            int unpaidRegistrations = registrations.Where(tr => !tr.PaymentReceived).Count();
            if (unpaidRegistrations >= 2)
            {
                ViewBag.Status = "Limited";
                return View(dto);
            }

            // normal
            ViewBag.Status = "Available";
            return View(dto);
        }
    }
}
