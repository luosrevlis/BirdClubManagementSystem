using Microsoft.AspNetCore.Mvc;
using BirdClubInfoHub.Models;
using BirdClubInfoHub.Data;

namespace BirdClubInfoHub.Controllers
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
            List<IClubEvent> eventList = new();
            eventList.AddRange(_dbContext.FieldTrips.Cast<IClubEvent>());
            eventList.AddRange(_dbContext.Meetings.Cast<IClubEvent>());
            eventList.AddRange(_dbContext.Tournaments.Cast<IClubEvent>());
            return View(eventList);
        }
    }
}
