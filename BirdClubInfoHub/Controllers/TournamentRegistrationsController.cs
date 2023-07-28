using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class TournamentRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IVnPayService _vnPayService;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public TournamentRegistrationsController(
            BcmsDbContext dbContext,
            IVnPayService vnPayService,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _vnPayService = vnPayService;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<TournamentRegistrationDTO> registrations = _dbContext.TournamentRegistrations
                .Include(tr => tr.Bird)
                .ThenInclude(bird => bird.User)
                .Where(tr => tr.Bird.UserId == userId)
                .Include(tr => tr.Tournament)
                .Select(tr => _mapper.Map<TournamentRegistrationDTO>(tr))
                .ToList();
            //registrations.RemoveAll(tr => tr.Tournament.Status != "Open" && tr.Tournament.Status != "Registration Closed");
            return View(registrations
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }

        public IActionResult Register(int id, int birdId)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            Bird? bird = _dbContext.Birds.Find(birdId);
            if (bird == null)
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Details", "Tournaments", new { id });
            }
            TournamentRegistration registration = new()
            {
                Bird = bird,
                Tournament = tournament
            };
            _dbContext.TournamentRegistrations.Add(registration);
            _dbContext.SaveChanges();
            return RedirectToAction("GeneratePaymentUrl", new RouteValueDictionary(new { id = registration.Id }));
        }

        public IActionResult RegisterNoPay(int id, int birdId)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            Bird? bird = _dbContext.Birds.Find(birdId);
            if (bird == null)
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Details", "Tournaments", new { id });
            }
            TournamentRegistration registration = new()
            {
                Bird = bird,
                Tournament = tournament
            };
            _dbContext.TournamentRegistrations.Add(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration success!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        public IActionResult GeneratePaymentUrl(int id)
        {
            TournamentRegistration registration = _dbContext.TournamentRegistrations.Find(id)!;
            registration.Tournament = _dbContext.Tournaments.Find(registration.TournamentId)!;
            registration.Bird = _dbContext.Birds.Find(registration.BirdId)!;
            registration.Bird.User = _dbContext.Users.Find(registration.Bird.UserId)!;
            PaymentInformationModel model = new()
            {
                Amount = registration.Tournament.Fee,
                OrderDescription = "Payment for " + registration.Bird.Description + " to participate in " + registration.Tournament.Name,
                Name = registration.Bird.User.Name,
            };
            string returnUrl = Url.Action(action: "PaymentConfirmed", controller: "TournamentRegistrations",
                values: new RouteValueDictionary(new { id }), protocol: "https")!;
            string url = _vnPayService.CreatePaymentUrl(model, HttpContext, returnUrl);
            return Redirect(url);
        }

        public IActionResult PaymentConfirmed(int id)
        {
            PaymentResponseModel model = _vnPayService.PaymentExecute(Request.Query);
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Error");
                TempData.Add("error", "It seems like something went wrong. Please re-register.");
                return RedirectToAction("Index", "ClubEvents");
            }
            if (!model.Success || model.VnPayResponseCode != "00")
            {
                TempData.Add("notification", "Payment failed!");
                TempData.Add("error", "Please reattempt the payment process.");
                return RedirectToAction("Details", "Tournaments", new { id = registration.TournamentId });
            }
            registration.PaymentReceived = true;
            _dbContext.TournamentRegistrations.Update(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Payment success!");
            TempData.Add("success", "");
            return RedirectToAction("Details", "Tournaments", new { id = registration.TournamentId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Registration not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            _dbContext.TournamentRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration cancelled!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
