using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Diagnostics;

namespace BirdClubInfoHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public HomeController(
            BcmsDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
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