using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class RequestController : Controller
    {
        private readonly BcmsDbContext _db;

        public RequestController(BcmsDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET METHOD
        public IActionResult Create()
        {
            return View();
        }

        //POST METHOD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MembershipRequest obj)
        {
            if (ModelState.IsValid)
            {
                _db.MembershipRequests.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
