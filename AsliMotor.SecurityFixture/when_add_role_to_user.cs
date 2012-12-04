using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Web.Security;
using AsliMotor.Security.Models;

namespace AsliMotor.Fixture
{
    class when_add_role_to_user
    {
        Because of = () =>
        {
            MembershipCreateStatus createStatus;
            Users user = Membership.CreateUser("kasir", "123456", "kasir@aslimotor.com", null, null, true, null, out createStatus);
            user.OwnerId = "dny@gmail.com";
            user.BranchId = "dny@gmail.com";
            Membership.UpdateUser(user);
            Roles.CreateRole("Cashier");
            Roles.AddUserToRole("kasir", "Cashier");
        };
        It should_be_membered_to_role = () =>
        {
            var validUser1Roles = new string[] { "Cashier" };
            var user1roles = Roles.GetRolesForUser("kasir");
        };

        Cleanup removeAllmembers = () =>
        {
        };
    }
}
