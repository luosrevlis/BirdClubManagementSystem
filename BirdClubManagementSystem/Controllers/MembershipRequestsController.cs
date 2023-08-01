using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Statuses;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class MembershipRequestsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IFluentEmailFactory _emailFactory;
        private const int PageSize = 10;

        public MembershipRequestsController(
            BcmsDbContext dbContext,
            IMapper mapper,
            IConfiguration configuration,
            IFluentEmailFactory emailFactory)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            _emailFactory = emailFactory;
        }

        public IActionResult Index(int page = 1, string keyword = "", string status = "")
        {
            IQueryable<MembershipRequest> matches = _dbContext.MembershipRequests;
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches
                    .Where(mr => mr.Name.ToLower().Contains(keyword.ToLower())
                    || mr.Email.ToLower().Contains(keyword.ToLower()));
            }
            if (!string.IsNullOrEmpty(status))
            {
                matches = matches.Where(mr => mr.Status == status);
            }

            List<MembershipRequestDTO> requests = matches
                .Reverse()
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(mr => _mapper.Map<MembershipRequestDTO>(mr))
                .ToList();
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
            request.Status = MemRequestStatuses.Accepted;
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
            request.Status = MemRequestStatuses.Rejected;
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
