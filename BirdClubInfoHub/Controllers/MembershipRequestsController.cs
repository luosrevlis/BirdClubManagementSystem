using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models;
using BirdClubInfoHub.Services;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BirdClubInfoHub.Controllers
{
    public class MembershipRequestsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IFluentEmailFactory _emailFactory;
        private readonly IVnPayService _vnPayService;
        private readonly IAccountGenerationService _accountGenerationService;

        public MembershipRequestsController
            (BcmsDbContext dbContext, IConfiguration configuration, IFluentEmailFactory emailFactory,
            IVnPayService vnPayService, IAccountGenerationService accountGenerationService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _emailFactory = emailFactory;
            _vnPayService = vnPayService;
            _accountGenerationService = accountGenerationService;
        }

        public IActionResult Index()
        {
            return View("Create");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MembershipRequest request)
        {
            User? user = _dbContext.Users.FirstOrDefault(x => x.Email == request.Email);
            if (user != null)
            {
                return View("EmailExisted");
            }
            request.Status = "Pending";
            _dbContext.MembershipRequests.Add(request);
            _dbContext.SaveChanges();
            return View("RequestRecorded");
        }

        public IActionResult GeneratePaymentUrl(int id)
        {
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            if (request.Status != "Accepted")
            {
                return View("Create");
            }
            PaymentInformationModel model = new()
            {
                Amount = double.Parse(_configuration["MembershipFee"]!),
                Name = request.Name,
                OrderDescription = "Membership Fee Payment"
            };
            string returnUrl = Url.Action(action: "PaymentConfirmed", controller: "MembershipRequests",
                values: new RouteValueDictionary(new { id }), protocol: "https")!;
            string url = _vnPayService.CreatePaymentUrl(model, HttpContext, returnUrl);
            return Redirect(url);
        }

        public IActionResult PaymentConfirmed(int id)
        {
            PaymentResponseModel model = _vnPayService.PaymentExecute(Request.Query);
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            if (!model.Success)
            {
                return BadRequest();
            }
            if (model.VnPayResponseCode != "00")
            {
                return BadRequest(); // payment failed
            }
            request.Status = "Payment Received";
            _dbContext.MembershipRequests.Update(request);
            _dbContext.Users.Add(_accountGenerationService.GenerateAccount(request, out LoginCredential credential));
            _dbContext.SaveChanges();

            StringBuilder bodyContent = new();
            bodyContent.AppendLine($"Welcome {request.Name}.")
                .AppendLine("Your account has been created on our system.")
                .AppendLine($"Email: {credential.Email}")
                .AppendLine($"Password: {credential.Password}")
                .AppendLine("Please log in and change your password as soon as possible.");
            IFluentEmail email = _emailFactory
                .Create()
                .To(credential.Email)
                .Subject("Your account")
                .Body(bodyContent.ToString());
            email.Send();

            return View(model);
        }
    }
}
