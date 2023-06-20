using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class LoginController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IFluentEmailFactory _emailFactory;

        public LoginController(BcmsDbContext dbContext, IFluentEmailFactory emailFactory)
        {
            _dbContext = dbContext;
            _emailFactory = emailFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginCredential loginCredential)
        {
            User? user = _dbContext.Users.First(u => u.Email == loginCredential.Email);
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
            //convert user to json maybe?
            HttpContext.Session.SetInt32("USER_ID", user.Id);
            HttpContext.Session.SetString("USER_NAME", user.Name);
            HttpContext.Session.SetString("USER_ROLE", user.Role);
            return RedirectToAction("Index", "Home");
        }

        //public IActionResult ForgetPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ForgetPassword(string email)
        //{
        //    User? user = _dbContext.Users.First(x => x.Email == email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("NotFound", "Cannot find email");
        //        return View();
        //    }
        //    return RedirectToAction("EnterVerificationCode");
        //}
    }
}
