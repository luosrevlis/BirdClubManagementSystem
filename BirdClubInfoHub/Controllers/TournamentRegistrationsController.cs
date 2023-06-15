using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using BirdClubInfoHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdClubInfoHub.Controllers
{
    public class TournamentRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IVnPayService _vnPayService;

        public TournamentRegistrationsController(BcmsDbContext dbContext, IVnPayService vnPayService)
        {
            _dbContext = dbContext;
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "ClubEvents");
        }

        [Authenticated]
        public IActionResult Register(int tournamentId)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(tournamentId);
            if (tournament == null)
            {
                return NotFound();
            }
            User? user = _dbContext.Users.Find(HttpContext.Session.GetInt32("USER_ID"));
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            tournament.TournamentRegistrations = _dbContext.TournamentRegistrations
                .Where(tr => tr.TournamentId == tournamentId).ToList();
            List<Bird> birds = _dbContext.Birds.Where(b => b.UserId == user.Id).ToList();
            // Remove birds that have been registered
            for (int i = 0; i < birds.Count; i++)
            {
                foreach (TournamentRegistration tr in tournament.TournamentRegistrations)
                {
                    if (birds[i].Id == tr.BirdId)
                    {
                        birds.Remove(birds[i]);
                        i--;
                        break;
                    }
                }
            }
            
            if (!birds.Any())
            {
                return View("NoBird");
            }
            SelectList options = new(birds, nameof(Bird.Id), nameof(Bird.Id));
            ViewBag.Options = options;
            TournamentRegistration registration = new() { Tournament = tournament };
            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TournamentRegistration registration)
        {
            int birdId = registration.BirdId;
            Bird? bird = _dbContext.Birds.Find(birdId);
            if (bird == null)
            {
                return NotFound();
            }
            int tournamentId = registration.TournamentId;
            Tournament? tournament = _dbContext.Tournaments.Find(tournamentId);
            if (tournament == null)
            {
                return NotFound();
            }
            registration.Tournament = tournament;
            registration.Bird = bird;
            _dbContext.TournamentRegistrations.Add(registration);
            _dbContext.SaveChanges();
            int id = _dbContext.TournamentRegistrations
                .FirstOrDefault(tr => tr.BirdId == registration.BirdId && tr.TournamentId == registration.TournamentId)!.Id;
            return RedirectToAction("PayEntranceFee", new RouteValueDictionary(new { id }));
        }

        [Authenticated]
        public IActionResult PayEntranceFee(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            registration.Tournament = _dbContext.Tournaments.Find(registration.TournamentId)!;
            registration.Bird = _dbContext.Birds.Find(registration.BirdId)!;
            registration.Bird.User = _dbContext.Users.Find(registration.Bird.UserId)!;
            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GeneratePaymentUrl(int id)
        {
            TournamentRegistration registration = _dbContext.TournamentRegistrations.Find(id)!;
            registration.Tournament = _dbContext.Tournaments.Find(registration.TournamentId)!;
            registration.Bird = _dbContext.Birds.Find(registration.BirdId)!;
            registration.Bird.User = _dbContext.Users.Find(registration.Bird.UserId)!;
            PaymentInformationModel model = new()
            {
                Amount = registration.Tournament.Fee,
                OrderDescription = "Thanh toan cho " + registration.Bird.Id + " tham gia giai dau " + registration.Tournament.Name,
                Name = registration.Bird.User.Name
            };
            string returnUrl = Url.Action(action: "PaymentConfirmed", controller: "TournamentRegistrations",
                values: new RouteValueDictionary(new { id }), protocol: "https")!;
            string url = _vnPayService.CreatePaymentUrl(model, HttpContext, returnUrl);
            return Redirect(url);
        }

        [Authenticated]
        public IActionResult PaymentConfirmed(int id)
        {
            PaymentResponseModel model = _vnPayService.PaymentExecute(Request.Query);
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
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
            _dbContext.TournamentRegistrations.Update(registration);
            _dbContext.SaveChanges();
            return View(model);
        }
    }
}
