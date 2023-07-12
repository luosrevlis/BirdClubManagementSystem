using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class MembershipRequestsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IFluentEmailFactory _emailFactory;

        public MembershipRequestsController(BcmsDbContext dbContext, IConfiguration configuration, IFluentEmailFactory emailFactory)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _emailFactory = emailFactory;
        }

        public IActionResult Index()
        {
            List<MembershipRequest> requests = _dbContext.MembershipRequests.ToList();
            return View(requests);
        }

        public IActionResult ViewPending()
        {
            List<MembershipRequest> requests = _dbContext.MembershipRequests.Where(request => request.Status == "Pending").ToList();
            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accept(int id)
        {
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null)
            {
                TempData.Add("notification", "Request not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            request.Status = "Accepted";
            _dbContext.MembershipRequests.Update(request);
            _dbContext.SaveChanges();

            string url = Url.Action(action: "GeneratePaymentUrl", controller: "MembershipRequests",
                values: new RouteValueDictionary(new { id }), protocol: "https", host: _configuration["InfoHubHost"])!;
            StringBuilder bodyContent = new();
            bodyContent.AppendLine("Your membership request has been accepted.")
                .AppendLine("Please go to the following URL to pay the membership fee.")
                .AppendLine(url);
            IFluentEmail email = _emailFactory
                .Create()
                .To(request.Email)
                .Subject("Request Accepted")
                .Body(bodyContent.ToString());
            email.Send();

            TempData.Add("notification", "Request accepted!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int id)
        {
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null)
            {
                TempData.Add("notification", "Request not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            request.Status = "Rejected";
            _dbContext.MembershipRequests.Update(request);
            _dbContext.SaveChanges();

            StringBuilder bodyContent = new();
            bodyContent.AppendLine($"Sorry {request.Name}, your request has been rejected.")
                .AppendLine("For more information please contact us.");
            IFluentEmail email = _emailFactory
                .Create()
                .To(request.Email)
                .Subject("Request Rejected")
                .Body(bodyContent.ToString());
            email.Send();

            TempData.Add("notification", "Request rejected!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
