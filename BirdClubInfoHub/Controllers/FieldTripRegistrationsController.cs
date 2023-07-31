using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Statuses;
using BirdClubInfoHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class FieldTripRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IVnPayService _vnPayService;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public FieldTripRegistrationsController
            (BcmsDbContext dbContext, IVnPayService vnPayService, IMapper mapper)
        {
            _dbContext = dbContext;
            _vnPayService = vnPayService;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1, string keyword = "")
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            IQueryable<FieldTripRegistration> matches = _dbContext.FieldTripRegistrations
                .Where(ftr => ftr.UserId == userId)
                .Include(ftr => ftr.FieldTrip);
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(ftr => ftr.FieldTrip.Name.ToLower().Contains(keyword.ToLower()));
            }

            List<FieldTripRegistrationDTO> registrations = matches
                .OrderByDescending(ftr => ftr.DateCreated)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(ftr => _mapper.Map<FieldTripRegistrationDTO>(ftr))
                .ToList();
            //registrations.RemoveAll(ftr => ftr.Status != "Open" && ftr.FieldTrip.Status != "Registration Closed");
            return View(registrations);
        }

        public IActionResult Register(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            FieldTripRegistration registration = new()
            {
                User = user,
                FieldTrip = fieldTrip
            };
            _dbContext.FieldTripRegistrations.Add(registration);
            _dbContext.SaveChanges();
            return RedirectToAction("GeneratePaymentUrl", new RouteValueDictionary(new { id = registration.Id }));
        }

        public IActionResult RegisterNoPay(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            FieldTripRegistration registration = new()
            {
                User = user,
                FieldTrip = fieldTrip
            };
            _dbContext.FieldTripRegistrations.Add(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration success!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        public IActionResult GeneratePaymentUrl(int id)
        {
            FieldTripRegistration registration = _dbContext.FieldTripRegistrations.Find(id)!;
            registration.FieldTrip = _dbContext.FieldTrips.Find(registration.FieldTripId)!;
            registration.User = _dbContext.Users.Find(registration.UserId)!;
            PaymentInformationModel model = new()
            {
                Amount = registration.FieldTrip.Fee,
                OrderDescription = registration.User.Name + " pay for " + registration.FieldTrip.Name
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
                TempData.Add("notification", "Registration not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            if (!model.Success || model.VnPayResponseCode != "00")
            {
                TempData.Add("notification", "Payment failed!");
                TempData.Add("error", "Please reattempt the payment process.");
                return RedirectToAction("Index");
            }
            registration.PaymentReceived = true;
            _dbContext.FieldTripRegistrations.Update(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Payment success!");
            TempData.Add("success", "");
            return RedirectToAction("Details", "FieldTrips", new { id = registration.FieldTripId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Registration not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            _dbContext.FieldTripRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration cancelled!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
