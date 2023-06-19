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
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<TournamentRegistration> registrations = _dbContext.TournamentRegistrations.ToList();
            for (int i = 0; i < registrations.Count; i++)
            {
                registrations[i].Bird = _dbContext.Birds.Find(registrations[i].BirdId)!;
                if (registrations[i].Bird.UserId != userId)
                {
                    registrations.Remove(registrations[i]);
                    i--;
                    continue;
                }
                registrations[i].Tournament = _dbContext.Tournaments.Find(registrations[i].TournamentId)!;
            }
            return View(registrations);
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
            HashSet<int> registeredBirds = _dbContext.TournamentRegistrations.Where(tr => tr.TournamentId == tournamentId)
                .Select(tr => tr.BirdId).ToHashSet();
            List<Bird> birds = _dbContext.Birds.Where(bird => bird.UserId == user.Id && !registeredBirds.Contains(bird.Id)).ToList();
            if (!birds.Any())
            {
                return View("NoBird");
            }
            SelectList options = new(birds, nameof(Bird.Id), nameof(Bird.Description));
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
                OrderDescription = "Thanh toan cho " + registration.Bird.Description + " tham gia giai dau " + registration.Tournament.Name,
                Name = registration.Bird.User.Name,
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

        [Authenticated]
        public IActionResult Delete(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            registration.Tournament = _dbContext.Tournaments.Find(registration.TournamentId)!;
            registration.Bird = _dbContext.Birds.Find(registration.BirdId)!;
            return View(registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                return NotFound();
            }
            _dbContext.TournamentRegistrations.Remove(registration);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
