using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Where(ftr => ftr.FieldTripId == fieldTripId)
                .Include(ftr => ftr.User)
                .Include(ftr => ftr.FieldTrip)
                .ToList();
            return View(registrations);
        }

        // POST: FieldTripRegistrationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.FieldTripRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Participant removed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { fieldTripId = registration.FieldTripId }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsPaid(int id)
        {
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            registration.PaymentReceived = true;
            _dbContext.FieldTripRegistrations.Update(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Entry has been marked as Payment received!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { fieldTripId = registration.FieldTripId }));
        }
    }
}
