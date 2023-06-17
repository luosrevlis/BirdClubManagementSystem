using BirdClubInfoHub.Models;
using BirdClubInfoHub.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BirdClubInfoHub.Filters;

namespace BirdClubInfoHub.Controllers
{
    public class FieldTripsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public FieldTripsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: FieldTripsController
        public IActionResult Index()
        {
            return View();
        }

        // GET: FieldTripsController/Details/5
        public IActionResult Details(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            return View(fieldTrip);
        }
    }
}
