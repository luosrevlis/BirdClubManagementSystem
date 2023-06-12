using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public LoginController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginCredential loginCredential)
        {
            User? user = _dbContext.Users.FirstOrDefault(u => u.Email == loginCredential.Email);
            if (user == null)
            {
                //can't find email
                return View("Index");
            }
            if (user.Password != loginCredential.Password)
            {
                //wrong password
                return View("Index");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
