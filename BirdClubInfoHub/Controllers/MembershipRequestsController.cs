using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Statuses;
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
        private readonly IMapper _mapper;

        public MembershipRequestsController(
            BcmsDbContext dbContext,
            IConfiguration configuration,
            IFluentEmailFactory emailFactory,
            IVnPayService vnPayService,
            IAccountGenerationService accountGenerationService,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _emailFactory = emailFactory;
            _vnPayService = vnPayService;
            _accountGenerationService = accountGenerationService;
            _mapper = mapper;
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
        public IActionResult Create(MembershipRequestDTO dto)
        {
            MembershipRequest? request = _dbContext.MembershipRequests
                .FirstOrDefault(mr => mr.Email == dto.Email && mr.Status != MemRequestStatuses.Rejected);
            User? user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user != null || request != null)
            {
                TempData.Add("notification", "Account or request already existed");
                TempData.Add("error", "");
                return View(dto);
            }
            request = _mapper.Map<MembershipRequest>(dto);
            request.Status = MemRequestStatuses.Pending;
            _dbContext.MembershipRequests.Add(request);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Your request has been recorded");
            TempData.Add("success", "Please wait while our staff handle the request. You will receive an email upon request approval.");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GeneratePaymentUrl(int id)
        {
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null || request.Status != MemRequestStatuses.Accepted)
            {
                TempData.Add("notification", "Error");
                TempData.Add("error", "It seems like something went wrong. Please resubmit your membership request.");
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
                TempData.Add("notification", "Error");
                TempData.Add("error", "It seems like something went wrong. Please resubmit your membership request.");
                return View("Create");
            }
            if (!model.Success || model.VnPayResponseCode != "00")
            {
                TempData.Add("notification", "Payment failed!");
                TempData.Add("error", "Please reattempt the payment process.");
                return RedirectToAction("Index", "Home");
            }
            request.Status = MemRequestStatuses.PaymentReceived;
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

            TempData.Add("notification", "Payment success!");
            TempData.Add("success", "Please check the registered email for your account.");
            return RedirectToAction("Index", "Home");
        }
    }
}
