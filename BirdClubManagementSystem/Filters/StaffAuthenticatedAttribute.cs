using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BirdClubManagementSystem.Filters
{
    public class StaffAuthenticatedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? role = context.HttpContext.Session.GetString("USER_ROLE");
            if (role == null || (role != "Staff" && role != "Admin"))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(context);
        }
    }
}
