using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models;
using BirdClubInfoHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class DonationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IVnPayService _vnPayService;

        public DonationsController(BcmsDbContext dbContext, IVnPayService vnPayService)
        {
            _dbContext = dbContext;
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            User? user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                ViewBag.Name = user.Name;
                ViewBag.Email = user.Email;
            }
            return View();
        }
    }
}
