﻿using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public ProfileController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult GetImageFromBytes(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            //if image is empty return default
            if (user.ProfilePicture.Length == 0)
            {
                return File("/img/placeholder/user.jpg", "image/png");
            }
            return File(user.ProfilePicture, "image/png");
        }

        // GET: ProfileController
        [Authenticated]
        public ActionResult Index()
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userID);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(user);
        }

        // GET: ProfileController/Edit/5
        [Authenticated]
        public ActionResult Edit(int id)
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(id);
            if (user == null || id != userID)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(user);
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            User? userInDb = _dbContext.Users.Find(user.Id);
            if (userInDb == null)
            {
                return RedirectToAction("Index", "Login");
            }
            userInDb.Name = user.Name;
            userInDb.Address = user.Address;
            userInDb.Phone = user.Phone;
            _dbContext.Users.Update(userInDb);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Profile updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        // GET: ProfileController/ChangeProfilePicture/5
        [Authenticated]
        public ActionResult ChangeProfilePicture(int id)
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(id);
            if (user == null || id != userID)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(user);
        }

        // POST: ProfileController/ChangeProfilePicture/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfilePicture(int id, IFormFile profilePicture)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (MemoryStream memoryStream = new())
            {
                profilePicture.CopyTo(memoryStream);
                user.ProfilePicture = memoryStream.ToArray();
            }
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Profile picture updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(IFormCollection formCollection)
        {
            string oldPassword = formCollection["OldPassword"]!;
            string newPassword = formCollection["NewPassword"]!;
            string confirmPassword = formCollection["ConfirmPassword"]!;
            User? user = _dbContext.Users.Find(HttpContext.Session.GetInt32("USER_ID"));
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "Login");
            }
            PasswordHasher<User> passwordHasher = new();
            if (passwordHasher.VerifyHashedPassword(user, user.Password, oldPassword) == PasswordVerificationResult.Failed)
            {
                TempData.Add("notification", "Incorrect old password!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            if (oldPassword == newPassword)
            {
                TempData.Add("notification", "New password can not be identical to old password!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            if (newPassword != confirmPassword)
            {
                TempData.Add("notification", "Password does not match!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            user.Password = passwordHasher.HashPassword(user, newPassword);
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Change password successful!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
