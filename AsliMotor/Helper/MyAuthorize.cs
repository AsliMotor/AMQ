using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AsliMotor.Helper
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MyAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Session["companyprofile"] == null)
            {
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectResult("/");
            }
            
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result =
                    new JsonResult
                    {
                        Data = new { error = true, message = "User ini tidak diizinkan untuk melakukan aksi ini." },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
            }
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    var authroized = base.AuthorizeCore(httpContext);
        //    if (!authroized)
        //    {
        //        return false;
        //    }
        //    var myvar = httpContext.Session["loginsession"];
        //    if (myvar == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}