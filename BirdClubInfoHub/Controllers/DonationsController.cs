using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class DonationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IVnPayService _vnPayService;

        public DonationsController(BcmsDbContext dbContext, IVnPayService vnPayService)
        {
            _dbContext = dbContext;
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            PaymentInformationModel model = new();
            int? userId = HttpContext.Session.GetInt32("USERID");
            User? user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                model.Name = user.Name;
                model.Email = user.Email;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GeneratePaymentUrl(PaymentInformationModel model)
        {
            string returnUrl = Url.Action(action: "PaymentConfirmed", controller: "Donations",
                values: new RouteValueDictionary(), protocol: "https")!;
            string url = _vnPayService.CreatePaymentUrl(model, HttpContext, returnUrl);
            return Redirect(url);
        }

        public IActionResult PaymentConfirmed()
        {
            PaymentResponseModel model = _vnPayService.PaymentExecute(Request.Query);
            if (!model.Success || model.VnPayResponseCode != "00")
            {
                TempData.Add("notification", "Payment failed!");
                TempData.Add("error", "Please reattempt the payment process.");
                return RedirectToAction("Index");
            }
            TempData.Add("notification", "Thank you for your donation!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "Home");
        }
    }
}
