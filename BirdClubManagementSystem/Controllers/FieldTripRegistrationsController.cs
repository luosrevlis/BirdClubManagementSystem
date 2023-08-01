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
    public class FieldTripRegistrationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public FieldTripRegistrationsController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: FieldTripRegistrationsController
        public ActionResult Index(int fieldTripId, int page = 1, string keyword = "")
        {
            HttpContext.Session.SetInt32("FIELDTRIP_ID", fieldTripId);

            IQueryable<FieldTripRegistration> matches = _dbContext.FieldTripRegistrations
                .Where(ftr => ftr.FieldTripId == fieldTripId)
                .Include(ftr => ftr.User);
            if (!string.IsNullOrEmpty(keyword) )
            {
                matches = matches.Where(ftr => ftr.User.Name.ToLower().Contains(keyword.ToLower()));
            }

            List<FieldTripRegistrationDTO> registrations = matches
                .OrderByDescending(ftr => ftr.DateCreated)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(ftr => _mapper.Map<FieldTripRegistrationDTO>(ftr))
                .ToList();
            return View(registrations);
        }

        // POST: FieldTripRegistrationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            int fieldTripId = HttpContext.Session.GetInt32("FIELDTRIP_ID") ?? 0;
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null || registration.FieldTripId != fieldTripId)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { fieldTripId }));
            }
            _dbContext.FieldTripRegistrations.Remove(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Participant removed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { fieldTripId }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsPaid(int id)
        {
            int fieldTripId = HttpContext.Session.GetInt32("FIELDTRIP_ID") ?? 0;
            FieldTripRegistration? registration = _dbContext.FieldTripRegistrations.Find(id);
            if (registration == null || registration.FieldTripId != fieldTripId)
            {
                TempData.Add("notification", "Participant not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", new RouteValueDictionary(new { fieldTripId }));
            }
            registration.PaymentReceived = true;
            _dbContext.FieldTripRegistrations.Update(registration);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Entry has been marked as Payment received!");
            TempData.Add("success", "");
            return RedirectToAction("Index", new RouteValueDictionary(new { fieldTripId }));
        }
    }
}
