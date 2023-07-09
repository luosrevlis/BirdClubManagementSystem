﻿using BirdClubInfoHub.Models;
using BirdClubInfoHub.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BirdClubInfoHub.Filters;

namespace BirdClubInfoHub.Controllers
{
    public class FieldTripsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public FieldTripsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: FieldTripsController/Details/5
        public IActionResult Details(int id)
        {
            FieldTrip? fieldTrip = _dbContext.FieldTrips.Find(id);
            if (fieldTrip == null)
            {
                TempData.Add("notification", "Field trip not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }

            // if not open, return unavailable
            if (fieldTrip.Status != "Open")
            {
                ViewBag.Status = "Unavailable";
                return View(fieldTrip);
            }

            // open but not logged in, return unauth
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                ViewBag.Status = "Unauth";
                return View(fieldTrip);
            }
            
            // open, logged in, already registered
            if (_dbContext.FieldTripRegistrations.FirstOrDefault(x => x.FieldTripId == id && x.UserId == userId) != null)
            {
                ViewBag.Status = "Registered";
                return View(fieldTrip);
            }

            // open, logged in, not registered
            ViewBag.Status = "Available";
            return View(fieldTrip);
        }
    }
}
