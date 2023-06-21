using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class ProfileController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public ProfileController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: ProfileController
        public ActionResult Index()
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userID);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            if (id != userID)
            {
                return RedirectToAction("Index", "Login");
            }
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword(int id)
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            if (id != userID)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(IFormCollection formCollection)
        {
            string oldPassword = formCollection["OldPassword"]!;
            string newPassword = formCollection["NewPassword"]!;
            string confirmPassword = formCollection["ConfirmPassword"]!;
            User? user = _dbContext.Users.Find(HttpContext.Session.GetInt32("USER_ID"));
            PasswordHasher<User> passwordHasher = new();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (passwordHasher.VerifyHashedPassword(user, user.Password, oldPassword) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("OldIncorrect", "Old Password is incorrect!");
            }
            if (oldPassword == newPassword)
            {
                ModelState.AddModelError("SamePassword", "New Password cannot be the same as Old Password!");
            }
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("ConfirmMismatch", "Confirm Password does not match New Password!");
            }
            if (!ModelState.IsValid)
            {
                return View(formCollection);
            }
            user.Password = passwordHasher.HashPassword(user, newPassword);
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
