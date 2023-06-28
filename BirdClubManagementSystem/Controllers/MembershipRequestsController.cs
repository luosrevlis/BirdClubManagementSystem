using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Text;

namespace BirdClubManagementSystem.Controllers
{
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

        public IActionResult Accept(int id)
        {
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public IActionResult AcceptConfirmed(int id)
        {
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null)
            {
                return NotFound();
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

            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        [HttpPost, ActionName("Reject")]
        [ValidateAntiForgeryToken]
        public IActionResult RejectConfirmed(int id)
        {
            MembershipRequest? request = _dbContext.MembershipRequests.Find(id);
            if (request == null)
            {
                return NotFound();
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

            return RedirectToAction("Index");
        }
    }
}
