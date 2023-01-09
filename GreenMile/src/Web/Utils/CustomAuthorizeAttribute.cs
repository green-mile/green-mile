using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Utils
{
   /* public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected void HandleUnauthorizedRequest(PageHandlerExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Redirect to login page if the user is not authenticated
                context.Result = new RedirectToPageResult("/loginpath");
            }
            else
            {
                // Redirect to access denied page if the user is authenticated but does not have the necessary role
                context.Result = new RedirectToPageResult("/accessdeniedpath");
            }
        }
    }*/
}
