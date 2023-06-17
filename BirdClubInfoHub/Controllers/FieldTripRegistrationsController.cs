using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using BirdClubInfoHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdClubInfoHub.Controllers
{
    public class FieldTripRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IVnPayService _vnPayService;

        public FieldTripRegistrationsController(BcmsDbContext dbContext, IVnPayService vnPayService)
        {
            _dbContext = dbContext;
            _vnPayService = vnPayService;
        }

        [Authenticated]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<FieldTripRegistration> registrations = _dbContext.FieldTripRegistrations.Where(ftr => ftr.UserId == userId).ToList();
            foreach (FieldTripRegistration ftr in registrations)
            {
                ftr.FieldTrip = _dbContext.FieldTrips.Find(ftr.FieldTripId)!;
                ftr.User = user;
            }
            return View(registrations);
        }

        [Authenticated]
        public IActionResult Register(int fieldTripId)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(fieldTripId);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations
                .FirstOrDefault(ftr => ftr.FieldTripId == fieldTripId && ftr.UserId == userId);
            // If already registered, notify
            if (registration != null)
            {
                return View("AlreadyRegistered");
            }
            registration = new() { FieldTrip = fieldTrip };
            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(FieldTripRegistration registration)
        {
            int userId = registration.UserId;
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            int fieldTripId = registration.FieldTripId;
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(fieldTripId);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            registration.User = user;
            registration.FieldTrip = fieldTrip;
            _dbContext.FieldTripRegistrations.Add(registration);
            _dbContext.SaveChanges();
            int id = _dbContext.FieldTripRegistrations
                .FirstOrDefault(ftr => ftr.UserId == userId && ftr.FieldTripId == fieldTripId)!.Id;
            return RedirectToAction("PayEntranceFee", new RouteValueDictionary(new { id }));
        }

        [Authenticated]
        public IActionResult PayEntranceFee(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GeneratePaymentUrl(int id)
        {
            FieldTripRegistration registration = _dbContext.FieldTripRegistrations.Find(id)!;
            registration.FieldTrip = _dbContext.FieldTrips.Find(registration.FieldTripId)!;
            registration.User = _dbContext.Users.Find(registration.UserId)!;
            PaymentInformationModel model = new PaymentInformationModel()
            {
                Amount = registration.FieldTrip.Fee,
                OrderDescription = registration.User.Name + " thanh toan cho " + registration.FieldTrip.Name
            };
            string returnUrl = Url.Action(action: "PaymentConfirmed", controller: "FieldTripRegistrations",
                values: new RouteValueDictionary(new { id }), protocol: "https")!;
            string url = _vnPayService.CreatePaymentUrl(model, HttpContext, returnUrl);
            return Redirect(url);
        }

        [Authenticated]
        public IActionResult PaymentConfirmed(int id)
        {
            PaymentResponseModel model = _vnPayService.PaymentExecute(Request.Query);
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            if (!model.Success)
            {
                return BadRequest();
            }
            if (model.VnPayResponseCode != "00")
            {
                return BadRequest(); // payment failed
            }
            registration.PaymentReceived = true;
            _dbContext.FieldTripRegistrations.Update(registration);
            _dbContext.SaveChanges();
            return View(model);
        }

        [Authenticated]
        public IActionResult Delete(int id)
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
            return RedirectToAction("Index");
        }
    }
}
