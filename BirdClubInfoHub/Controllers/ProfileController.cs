using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BirdClubInfoHub.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BcmsDbContext _db;
        private int _userID;
        public ProfileController(BcmsDbContext db)
        {
            _db = db;
        }
        // GET: ProfileController
        public ActionResult Index()
        {
            _userID = (int)HttpContext.Session.GetInt32("USER_ID")!;
            User? user = _db.Users.Find(_userID);
            return View(user);
        }

        // GET: ProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int? id)
        {
            id = (int)HttpContext.Session.GetInt32("USER_ID")!;
            User? user = _db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    

        // POST: ProfileController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? userId,User? user)
        { 
             userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }
             user = _db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
