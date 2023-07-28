using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class FieldTripsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public FieldTripsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: FieldTripsController/Details/5
        public IActionResult Details(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
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
            if (fieldTrip.StartDate < fieldTrip.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(fieldTrip);
            }
            fieldTrip.Status = "Open";
            _dbContext.FieldTrips.Add(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", fieldTrip.Name + " has been created!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // GET: FieldTripsController/Edit/5
        public IActionResult Edit(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(fieldTrip);
        }

        // POST: FieldTripsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FieldTrip fieldTrip)
        {
            if (fieldTrip.StartDate < fieldTrip.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(fieldTrip);
            }
            FieldTrip? fieldTripInDb = _dbContext.FieldTrips.Find(fieldTrip.Id);
            if (fieldTripInDb == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            fieldTripInDb.Name = fieldTrip.Name;
            fieldTripInDb.StartDate = fieldTrip.StartDate;
            fieldTripInDb.RegCloseDate = fieldTrip.RegCloseDate;
            fieldTripInDb.Description = fieldTrip.Description;
            _dbContext.FieldTrips.Update(fieldTripInDb);
            _dbContext.SaveChanges();

            TempData.Add("notification", fieldTripInDb.Name + " has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: FieldTripsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.FieldTrips.Remove(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", fieldTrip.Name + " has been deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: FieldTripsController/Close/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Close(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            fieldTrip.Status = "Registration Closed";
            _dbContext.FieldTrips.Update(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration for " + fieldTrip.Name + " has been closed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: FieldTripsController/MarkAsEnded/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsEnded(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            fieldTrip.Status = "Ended";
            _dbContext.FieldTrips.Update(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", fieldTrip.Name + " has been marked as ended!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: FieldTripsController/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            fieldTrip.Status = "Cancelled";
            _dbContext.FieldTrips.Update(fieldTrip);
            _dbContext.SaveChanges();

            TempData.Add("notification", fieldTrip.Name + " has been cancelled!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        public IActionResult EditHighlights(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null || fieldTrip.Status != "Ended")
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(fieldTrip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHighlights(FieldTrip fieldTrip)
        {
            FieldTrip? fieldTripInDb = _dbContext.FieldTrips.Find(fieldTrip.Id);
            if (fieldTripInDb == null || fieldTripInDb.Status != "Ended")
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            fieldTripInDb.Highlights = fieldTrip.Highlights;
            _dbContext.FieldTrips.Update(fieldTripInDb);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Highlights has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Details", new { id = fieldTrip.Id });
        }
    }
}
