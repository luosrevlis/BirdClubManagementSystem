﻿using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class TournamentRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public TournamentRegistrationsController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: TournamentRegistrationsController
        public ActionResult Index(int tournamentId)
        {
            List<TournamentRegistration> registrations = _dbContext.TournamentRegistrations
                .Where(tr => tr.TournamentId == tournamentId)
                .Include(tr => tr.Bird)
                .ThenInclude(bird => bird.User)
                .Include(tr => tr.Tournament)
                .ToList();
            return View(registrations);
        }

        // POST: TournamentRegistrationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.TournamentRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Participant removed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId = registration.TournamentId }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAsPaid(int id)
        {
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            registration.PaymentReceived = true;
            _dbContext.TournamentRegistrations.Update(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Entry has been marked as Payment received!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId = registration.TournamentId }));
        }
    }
}
