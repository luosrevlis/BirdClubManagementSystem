using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
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
        public IActionResult Login(LoginCredential? loginCredential)
        {
            if (loginCredential == null)
            {
                return BadRequest();
            }
            User? user = _dbContext.Users.FirstOrDefault(u => u.Email == loginCredential.Email);
            if (user == null)
            {
                //can't find email
                return View("Index");
            }
            if (user.Password != loginCredential.Password)
            {
                //wrong password
                return View("Index");
            }
            //convert user to json maybe?
            HttpContext.Session.SetInt32("USER_ID", user.Id);
            HttpContext.Session.SetString("USER_NAME", user.Name);
            return RedirectToAction("Index", "Home");
        }
    }
}
