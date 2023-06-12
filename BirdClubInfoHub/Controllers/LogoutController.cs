using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Remove("USER_ID");
            HttpContext.Session.Remove("USER_NAME");
            return RedirectToAction("Index", "Home");
        }
    }
}
