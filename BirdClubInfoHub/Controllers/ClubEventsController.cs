using Microsoft.AspNetCore.Mvc;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.Entities;

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
