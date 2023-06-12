using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
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

        // GET: FieldTripsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FieldTripsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FieldTrip fieldTrip)
        {
            if (ModelState.IsValid)
            {
                _dbContext.FieldTrips.Add(fieldTrip);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(fieldTrip);
        }

        // GET: FieldTripsController/Edit/5
        public IActionResult Edit(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            return View(fieldTrip);
        }

        // POST: FieldTripsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FieldTrip fieldTrip)
        {
            if (ModelState.IsValid)
            {
                _dbContext.FieldTrips.Update(fieldTrip);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(fieldTrip);
        }

        // GET: FieldTripsController/Delete/5
        public IActionResult Delete(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            return View(fieldTrip);
        }

        // POST: FieldTripsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            _dbContext.FieldTrips.Remove(fieldTrip);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "ClubEvents");
        }
    }
}
