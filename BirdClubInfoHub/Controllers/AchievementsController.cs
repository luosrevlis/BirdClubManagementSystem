using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class AchievementsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public AchievementsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: AchievementsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AchievementsController/Details/5
        public ActionResult Details(int id)
        {
            Achievement? achievement = _dbContext.Achievement.Find(id);
            if (achievement == null)
            {
                return NotFound();
            }
            return View(achievement);
        }
    }
}
