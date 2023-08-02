using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
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
        private const int PageSize = 10;

        public TournamentRegistrationsController
            (BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: TournamentRegistrationsController
        public ActionResult Index(int tournamentId, int page = 1, string keyword = "")
        {
            HttpContext.Session.SetInt32("TOURNAMENT_ID", tournamentId);

            IQueryable<TournamentRegistration> matches = _dbContext.TournamentRegistrations
                .Where(tr => tr.TournamentId == tournamentId)
                .Include(tr => tr.Bird)
                .ThenInclude(bird => bird.User);
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches
                    .Where(tr => tr.Bird.Name.ToLower().Contains(keyword.ToLower())
                    || tr.Bird.User.Name.ToLower().Contains(keyword.ToLower()));
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

            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.MaxPage = maxPage;
            return View(registrations);
        }

        // POST: TournamentRegistrationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int tournamentId = HttpContext.Session.GetInt32("TOURNAMENT_ID") ?? 0;
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null || registration.TournamentId != tournamentId)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
            }
            _dbContext.TournamentRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Participant removed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAsPaid(int id)
        {
            int tournamentId = HttpContext.Session.GetInt32("TOURNAMENT_ID") ?? 0;
            TournamentRegistration? registration = _dbContext.TournamentRegistrations.Find(id);
            if (registration == null || registration.TournamentId != tournamentId)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
            }
            registration.PaymentReceived = true;
            _dbContext.TournamentRegistrations.Update(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Entry has been marked as Payment received!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { tournamentId }));
        }
    }
}
