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
    [Authorize(Roles= RoleName.ADMINISTRATOR)]
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
                    Email = usr.Email,
                    Locked = usr.IsLockedOut
                });
            }

            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AllRoles()
        {
            string[] allRoles = Roles.GetAllRoles();
            IList<object> result = new List<object>();
            foreach (string role in allRoles)
            {
                result.Add(new { id = role, name = role });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUser(string username, string password, string email, string role)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                Users user = Membership.CreateUser(username, password, email);
                user.BranchId = cp.BranchId;
                user.OwnerId = cp.OwnerId;
                Membership.UpdateUser(user);
                Roles.AddUserToRole(user.Name, role);
                return Json(new { error = false, data = user }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteUser(string username)
        {
            try
            {
                Membership.DeleteUser(username);
                return Json(new { error = false, data = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }
    }
}
