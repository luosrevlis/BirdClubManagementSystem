﻿using AutoMapper;
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

        public TournamentRegistrationsController
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

            IQueryable<TournamentRegistration> matches = _dbContext.TournamentRegistrations
                .Include(tr => tr.Bird)
                .Where(tr => tr.Bird.UserId == userId)
                .Include(tr => tr.Tournament);
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(tr => tr.Tournament.Name.ToLower().Contains(keyword.ToLower()));
            }

            int maxPage = (int)Math.Ceiling(matches.Count() / (double)PageSize);
            if (page > maxPage)
            {
                page = maxPage;
            }
            if (page < 1)
            {
                page = 1;
            }

            List<TournamentRegistrationDTO> registrations = matches
                .OrderByDescending(tr => tr.DateCreated)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(tr => _mapper.Map<TournamentRegistrationDTO>(tr))
                .ToList();
            //registrations.RemoveAll(tr => tr.Tournament.Status != "Open" && tr.Tournament.Status != "Registration Closed");

            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.MaxPage = maxPage;
            return View(registrations);
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
            int regCount = _dbContext.TournamentRegistrations.Where(tr => tr.TournamentId == id).Count();
            if (regCount >= tournament.RegLimit)
            {
                TempData.Add("notification", "This event has reached maximum participants!");
                TempData.Add("error", "");
                return RedirectToAction("Details", "Tournaments", new { id });
            }
            Bird? bird = _dbContext.Birds.Find(birdId);
            if (bird == null || bird.UserId != HttpContext.Session.GetInt32("USER_ID"))
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Details", "Tournaments", new { id });
            }
            PaymentInformationModel model = new()
            {
                Amount = tournament.Fee,
                OrderDescription = "Payment for " + bird.Description + " to participate in " + tournament.Name,
                Name = bird.User.Name,
            };
            string returnUrl = Url.Action(action: "PaymentConfirmed", controller: "TournamentRegistrations",
                values: new RouteValueDictionary(new { id, birdId }), protocol: "https")!;
            string url = _vnPayService.CreatePaymentUrl(model, HttpContext, returnUrl);
            return Redirect(url);
        }

        public IActionResult PaymentConfirmed(int id, int birdId)
        {
            PaymentResponseModel model = _vnPayService.PaymentExecute(Request.Query);
            if (!model.Success || model.VnPayResponseCode != "00")
            {
                TempData.Add("notification", "Payment failed!");
                TempData.Add("error", "Please reattempt the payment process.");
                return RedirectToAction("Index");
            }
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            Bird? bird = _dbContext.Birds.Find(birdId);
            if (bird == null || bird.UserId != HttpContext.Session.GetInt32("USER_ID"))
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Details", "Tournaments", new { id });
            }
            TournamentRegistration registration = new()
            {
                Bird = bird,
                Tournament = tournament,
                PaymentReceived = true
            };
            _dbContext.TournamentRegistrations.Add(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Payment success!");
            TempData.Add("success", "");
            return RedirectToAction("Details", "Tournaments", new { id });
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
            int regCount = _dbContext.TournamentRegistrations.Where(tr => tr.TournamentId == id).Count();
            if (regCount >= tournament.RegLimit)
            {
                TempData.Add("notification", "This event has reached maximum participants!");
                TempData.Add("error", "");
                return RedirectToAction("Details", "Tournaments", new { id });
            }
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            Bird? bird = _dbContext.Birds.Find(birdId);
            if (bird == null || bird.UserId != userId)
            {
                TempData.Add("notification", "Bird not found!");
                TempData.Add("error", "");
                return RedirectToAction("Details", "Tournaments", new { id });
            }
            int unpaidCount = _dbContext.TournamentRegistrations
                .Include(tr => tr.Bird)
                .Where(tr => tr.TournamentId == id
                && tr.Bird.UserId == userId
                && !tr.PaymentReceived)
                .Count();
            if (unpaidCount >= 2)
            {
                TempData.Add("notification", "Unpaid registrations limit reached!");
                TempData.Add("error", "You already have 2 unpaid registrations!");
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
            string returnUrl = Url.Action(action: "MarkAsPaid", controller: "TournamentRegistrations",
                values: new RouteValueDictionary(new { id }), protocol: "https")!;
            string url = _vnPayService.CreatePaymentUrl(model, HttpContext, returnUrl);
            return Redirect(url);
        }

        public IActionResult MarkAsPaid(int id)
        {
            PaymentResponseModel model = _vnPayService.PaymentExecute(Request.Query);
            if (!model.Success || model.VnPayResponseCode != "00")
            {
                TempData.Add("notification", "Payment failed!");
                TempData.Add("error", "Please reattempt the payment process.");
                return RedirectToAction("Index");
            }
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Registration not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            registration.PaymentReceived = true;
            _dbContext.TournamentRegistrations.Update(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Payment success!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
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
