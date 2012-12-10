using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Models;
using System.Web.Security;
using AsliMotor.Security.Models;
using AsliMotor.Helper;

namespace AsliMotor.Controllers
{
    [MyAuthorize(Roles= RoleName.ADMINISTRATOR)]
    public class ManageUserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Lists()
        {
            IList<UserViewModel> allUsers = new List<UserViewModel>();
            foreach (MembershipUser muser in Membership.GetAllUsers())
            {
                string[] roles = Roles.GetRolesForUser(muser.UserName);
                string rolesString = "";

                foreach (string role in roles)
                {
                    rolesString += role + ",";
                }

                if (rolesString.Length > 1)
                {
                    rolesString = rolesString.Remove(rolesString.Length - 1, 1);
                }

                Users usr = muser;

                allUsers.Add(new UserViewModel
                {
                    Username = muser.UserName,
                    Roles = rolesString,
                    Email = usr.Email
                });
            }

            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }
    }
}
