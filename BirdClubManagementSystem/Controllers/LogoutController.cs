using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Remove("USER_ID");
            HttpContext.Session.Remove("USER_NAME");
            HttpContext.Session.Remove("USER_ROLE");
            return RedirectToAction("Index", "Login");
        }
    }
}
