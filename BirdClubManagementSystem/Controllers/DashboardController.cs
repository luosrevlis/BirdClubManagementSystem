using Microsoft.AspNetCore.Mvc;
using BirdClubManagementSystem.Models;
using BirdClubManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public DashboardController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.NewMembers = GetNewMembers(7);
            ViewBag.NewBlogs = GetNewBlogs(7);
            ViewBag.NewEvents = GetHostedEvents(30);
            ViewBag.Revenue = $"{GetRevenue(30):n0}";
            ViewBag.ActiveRate = $"{GetActiveRate(7):n0}";
            return View(user);
        }

        private int GetNewMembers(int days)
        {
            return _dbContext.Users.Count(user => user.JoinDate.AddDays(days) >= DateTime.Now);
        }

        private int GetNewBlogs(int days)
        {
            return _dbContext.Blogs.Count(blog => blog.DateCreated.AddDays(days) >= DateTime.Now);
        }

        private int GetHostedEvents(int days)
        {
            int fieldTripCount = _dbContext.FieldTrips.Count(ft => ft.StartDate.AddDays(days) >= DateTime.Now);
            int meetingCount = _dbContext.Meetings.Count(ft => ft.StartDate.AddDays(days) >= DateTime.Now);
            int tournamentCount = _dbContext.Tournaments.Count(ft => ft.StartDate.AddDays(days) >= DateTime.Now);
            return fieldTripCount + meetingCount + tournamentCount;
        }

        private long GetRevenue(int days)
        {
            long revenue = 0;
            List<FieldTrip> fieldTrips = _dbContext.FieldTrips
                .Where(ft => ft.StartDate.AddDays(days) >= DateTime.Now)
                .ToList();
            foreach (FieldTrip fieldTrip in fieldTrips)
            {
                int regCount = _dbContext.FieldTripRegistrations
                    .Count(ftr => ftr.FieldTripId == fieldTrip.Id && ftr.PaymentReceived);
                revenue += regCount * fieldTrip.Fee;
            }
            List<Tournament> tournaments = _dbContext.Tournaments
                .Where(t => t.StartDate.AddDays(days) >= DateTime.Now)
                .ToList();
            foreach (Tournament tournament in tournaments)
            {
                int regCount = _dbContext.TournamentRegistrations
                    .Count(tr => tr.TournamentId == tournament.Id && tr.PaymentReceived);
                revenue += regCount * tournament.Fee;
            }
            return revenue;
        }

        private double GetActiveRate(int days)
        {
            int activeCount = _dbContext.Users
                .Select(user => user.LastLogin ?? new DateTime())
                .Count(dt => dt.AddDays(days) >= DateTime.Now);
            int totalCount = _dbContext.Users.Count();
            return activeCount * 100.0 / totalCount;
        }
    }
}
