using BirdClubInfoHub.Models;
using BirdClubInfoHub.Data;
using Microsoft.AspNetCore.Mvc;
using BirdClubInfoHub.Filters;

namespace BirdClubInfoHub.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public MeetingsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: MeetingsController
        public IActionResult Index()
        {
            return View();
        }

        // GET: MeetingsController/Details/5
        public IActionResult Details(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }
    }
}
