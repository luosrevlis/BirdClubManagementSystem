using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class NotificationsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IFluentEmailFactory _emailFactory;

        public NotificationsController(BcmsDbContext dbContext, IFluentEmailFactory emailFactory)
        {
            _dbContext = dbContext;
            _emailFactory = emailFactory;
        }

        public IActionResult Index()
        {
            List<User> users = _dbContext.Users.ToList();
            SelectList customOptions = new(users, nameof(Models.User.Email), nameof(Models.User.Name));
            ViewBag.CustomOptions = customOptions;
            return View(new Notification());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendNotification(Notification notification)
        {
            List<User> users = _dbContext.Users.ToList();
            if (!notification.IsRoleSelected["Custom"])
            {
                notification.Recipients.Clear();
            }
            foreach (User user in users)
            {
                if (notification.IsRoleSelected[user.Role])
                {
                    notification.Recipients.Add(user.Email);
                }
            }
            foreach (string recipient in notification.Recipients)
            {
                IFluentEmail email = _emailFactory
                    .Create()
                    .To(recipient)
                    .Subject("Notification")
                    .Body(notification.Contents);
                    //.UsingTemplateFromFile()
                email.Send();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
