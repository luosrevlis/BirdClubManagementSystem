using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    public class ClubEventsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public ClubEventsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<IClubEvent> eventList = new List<IClubEvent>();
            eventList.AddRange(_dbContext.FieldTrips.Cast<IClubEvent>());
            eventList.AddRange(_dbContext.Meetings.Cast<IClubEvent>());
            eventList.AddRange(_dbContext.Tournaments.Cast<IClubEvent>());
            return View(eventList);
        }
    }
}
