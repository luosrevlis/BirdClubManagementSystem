using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BirdClubManagementSystem.Controllers
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
            User? user = _dbContext.Users.FirstOrDefault(u => u.Email == loginCredential.Email);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return View("Index");
            }
            PasswordHasher<User> passwordHasher = new();
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.Password, loginCredential.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                TempData.Add("notification", "Wrong password!");
                TempData.Add("error", "");
                return View("Index");
            }
            else if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = passwordHasher.HashPassword(user, user.Password);
            }
            user.LastLogin = DateTime.Now;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            //convert user to json maybe?
            HttpContext.Session.SetInt32("USER_ID", user.Id);
            HttpContext.Session.SetString("USER_NAME", user.Name);
            HttpContext.Session.SetString("USER_ROLE", user.Role);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(IFormCollection formCollection)
        {
            string userEmail = formCollection["userEmail"]!;
            User? user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return View("ResetPassword", userEmail);
            }
            //Generate a secure random string of length 30
            string randomString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            //Remove special characters and take the first 6 characters
            string verificationCode = Regex.Replace(randomString, "[^a-zA-Z0-9]", string.Empty).Remove(6);

            StringBuilder bodyContent = new();
            bodyContent.AppendLine($"Hello {user.Name},")
                .AppendLine("To complete the password reset process, please use the following code: ")
                .AppendLine(verificationCode)
                .AppendLine("The verification code is valid for 30 minutes.")
                .AppendLine($"If this request was not made by you, please ignore this email.");
                
            IFluentEmail email = _emailFactory
                .Create()
                .To(user.Email)
                .Subject("Verification Code")
                .Body(bodyContent.ToString());
            email.Send();

            user.ResetPasswordCode = verificationCode;
            user.ResetPasswordRequestTime = DateTime.Now;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return RedirectToAction("VerifyCode", new RouteValueDictionary(new { userEmail }));
        }

        public IActionResult VerifyCode(string userEmail)
        {
            return View("VerifyCode", userEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyCode(IFormCollection formCollection)
        {
            string userEmail = formCollection["userEmail"]!;
            string? code = formCollection["code"];
            User? user = _dbContext.Users.FirstOrDefault(user => user.Email == userEmail);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            if (DateTime.Now > user.ResetPasswordRequestTime.AddMinutes(30))
            {
                TempData.Add("notification", "Verification code has expired!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            if (code == null || code.Length != 6 || !code.Equals(user.ResetPasswordCode))
            {
                TempData.Add("notification", "Verification code is incorrect!");
                TempData.Add("error", "");
                return View("VerifyCode", userEmail);
            }
            return RedirectToAction("NewPassword", new RouteValueDictionary(new { userEmail }));
        }

        public IActionResult NewPassword(string userEmail)
        {
            return View("NewPassword", userEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewPassword(IFormCollection formCollection)
        {
            string userEmail = formCollection["userEmail"]!;
            string newPassword = formCollection["newPassword"]!;
            string confirmPassword = formCollection["confirmPassword"]!;
            User? user = _dbContext.Users.FirstOrDefault(user => user.Email == userEmail);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            if (confirmPassword != newPassword)
            {
                TempData.Add("notification", "Password does not match!");
                TempData.Add("error", "");
                return View("NewPassword", userEmail);
            }
            PasswordHasher<User> passwordHasher = new();
            user.Password = passwordHasher.HashPassword(user, newPassword);
            user.ResetPasswordCode = string.Empty;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Password reset successful!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
