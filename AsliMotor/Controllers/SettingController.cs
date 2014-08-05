using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Helper;
using AsliMotor.Models;
using System.Web.Security;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ProfileUser()
        {
            MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
            var data = new
            {
                Username = currentUser.UserName,
                Email = currentUser.Email,
                CreatedDate = currentUser.CreationDate,
                LastLoginDate = currentUser.LastLoginDate
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangePassword(string oldpassword, string newpassword, string confirmnewpassword)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                bool changePasswordSucceeded;
                MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                changePasswordSucceeded = currentUser.ChangePassword(oldpassword, newpassword);
                if (changePasswordSucceeded)
                {
                    return Json(new { error = false, data = string.Empty }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = true, message = "Gagal ganti password" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
