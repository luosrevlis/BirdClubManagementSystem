using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [AdminAuthenticated]
    public class UserManagementController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        
        public UserManagementController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: UserManagementController
        public IActionResult Index()
        {
            List<User> userList = _dbContext.Users.ToList(); 
            return View(userList);
        }

        // GET: UserManagementController/Details/5
        public IActionResult Details(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UserManagementController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserManagementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            PasswordHasher<User> passwordHasher = new();
            user.Password = passwordHasher.HashPassword(user, user.Password);
            user.JoinDate = DateTime.Now;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: UserManagementController/Edit/5
        public IActionResult Edit(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return View("Index");
            }
            return View(user);
        }

        // POST: UserManagementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            User? userInDb = _dbContext.Users.Find(user.Id);
            if (userInDb == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return View("Index");
            }
            userInDb.Name = user.Name;
            userInDb.Address = user.Address;
            userInDb.Phone = user.Phone;
            userInDb.Role = user.Role;
            _dbContext.Users.Update(userInDb);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: UserManagementController/Delete/5
        public IActionResult Delete(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserManagementController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
