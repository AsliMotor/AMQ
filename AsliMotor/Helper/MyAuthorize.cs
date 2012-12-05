using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsliMotor.Helper
{
    public class MyAuthorize : AuthorizeAttribute
    {
        public string Message { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || filterContext.HttpContext.Session["loginsession"] == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Logon");
                return;
            }
            
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result =
                    new JsonResult
                    {
                        Data = new { error = true, message = "User ini tidak diizinkan untuk melakukan aksi ini." },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                return;
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authroized = base.AuthorizeCore(httpContext);
            if (!authroized)
            {
                // the user is not authenticated or the forms authentication
                // cookie has expired
                return false;
            }

            // Now check the session:
            var myvar = httpContext.Session["loginsession"];
            if (myvar == null)
            {
                // the session has expired
                return false;
            }

            return true;
        }
    }
}