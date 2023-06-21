using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
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
            PasswordHasher<User> passwordHasher = new();
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.Password, loginCredential.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                //wrong password
                return View("Index");
            }
            else if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = passwordHasher.HashPassword(user, user.Password);
            }
            HttpContext.Session.SetInt32("USER_ID", user.Id);
            HttpContext.Session.SetString("USER_NAME", user.Name);
            HttpContext.Session.SetString("USER_ROLE", user.Role);
            return RedirectToAction("Index", "Home");
        }
    }
}
