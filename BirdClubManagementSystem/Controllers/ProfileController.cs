using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class ProfileController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProfileController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActionResult GetImageFromBytes(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            //if image is empty return default
            if (user.ProfilePicture == null || user.ProfilePicture.Length == 0)
            {
                return File("/img/placeholder/user.jpg", "image/png");
            }
            return File(user.ProfilePicture, "image/png");
        }

        // GET: ProfileController
        public ActionResult Index()
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(userID);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(_mapper.Map<UserDTO>(user));
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(id);
            if (user == null || id != userID)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(_mapper.Map<UserDTO>(user));
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserDTO dto)
        {
            User? user = _dbContext.Users.Find(dto.Id);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            user.Name = dto.Name;
            user.Address = dto.Address;
            user.Phone = dto.Phone;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Profile updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        // GET: ProfileController/ChangeProfilePicture/5
        public ActionResult ChangeProfilePicture(int id)
        {
            int? userID = HttpContext.Session.GetInt32("USER_ID");
            User? user = _dbContext.Users.Find(id);
            if (user == null || id != userID)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(_mapper.Map<UserDTO>(user));
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
            string oldPassword = formCollection["OldPassword"].ToString();
            string newPassword = formCollection["NewPassword"].ToString();
            string confirmPassword = formCollection["ConfirmPassword"].ToString();
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
