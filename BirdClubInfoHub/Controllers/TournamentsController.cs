using Microsoft.AspNetCore.Mvc;
using BirdClubInfoHub.Models;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdClubInfoHub.Controllers
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
        public IActionResult Details(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }
    }
}
