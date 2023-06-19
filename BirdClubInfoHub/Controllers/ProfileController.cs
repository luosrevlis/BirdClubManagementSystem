using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            int? _userID = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(_userID);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int? id)
        {
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
            if (ModelState.IsValid)
            {
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(IFormCollection formCollection)
        {
            string? oldPassword = formCollection["OldPassword"];
            string? newPassword = formCollection["NewPassword"];
            string? confirmPassword = formCollection["ConfirmPassword"];
            if (oldPassword.IsNullOrEmpty() || newPassword.IsNullOrEmpty() || confirmPassword.IsNullOrEmpty())
            {
                ModelState.AddModelError("emptyField", "");
            }
            User? user = _dbContext.Users.Find(HttpContext.Session.GetInt32("USER_ID"));
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (oldPassword != user.Password)
            {
                ModelState.AddModelError("oldIncorrect", "Old Password is incorrect!");
            }
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("confirmMismatch", "Confirm Password does not match New Password!");
            }
            if (!ModelState.IsValid)
            {
                return View(formCollection);
            }
            user.Password = newPassword;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
