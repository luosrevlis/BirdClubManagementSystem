using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Diagnostics;

namespace BirdClubInfoHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public HomeController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.Message = HttpContext.Session.GetString("USER_NAME");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}