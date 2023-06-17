using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class FieldTripRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public FieldTripRegistrationsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: FieldTripRegistrationsController
        public ActionResult Index(int fieldTripId)
        {
            List<FieldTripRegistration> registrations = _dbContext.FieldTripRegistrations
                .Where(ftr => ftr.FieldTripId == fieldTripId).ToList();
            foreach (FieldTripRegistration ftr in registrations)
            {
                ftr.User = _dbContext.Users.Find(ftr.UserId)!;
                ftr.FieldTrip = _dbContext.FieldTrips.Find(ftr.FieldTripId)!;
            }
            return View(registrations);
        }

        // GET: FieldTripRegistrationsController/Details/5
        public ActionResult Details(int id)
        {
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            registration.FieldTrip = _dbContext.FieldTrips.Find(registration.FieldTripId)!;
            registration.User = _dbContext.Users.Find(registration.UserId)!;
            return View(registration);
        }

        // GET: FieldTripRegistrationsController/Delete/5
        public ActionResult Delete(int id)
        {
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            registration.FieldTrip = _dbContext.FieldTrips.Find(registration.FieldTripId)!;
            registration.User = _dbContext.Users.Find(registration.UserId)!;
            return View(registration);
        }

        // POST: FieldTripRegistrationsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            _dbContext.FieldTripRegistrations.Remove(registration);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { fieldTripId = registration.FieldTripId }));
        }
    }
}
