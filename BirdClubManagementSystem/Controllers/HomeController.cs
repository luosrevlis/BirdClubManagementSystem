using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/StatusCodeError/{statusCode}")]
        public IActionResult StatusCodeError(int statusCode)
        {
            if (statusCode == 404)
            {
                ViewBag.Message = "404 Not Found";
            }
            return View();
        }
    }
}