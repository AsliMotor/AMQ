using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Web.Security;
using AsliMotor.Security.Models;

namespace AsliMotor.Fixture
{
    [Subject("create administrator user")]
    class when_create_administrastor_user
    {
        Establish context = () =>
        {
        };
        Because of = () =>
        {
            MembershipCreateStatus createStatus;
            if (Membership.GetUser("administrator") == null)
            {
                Users user = Membership.CreateUser("administrator", "123456", "admin@aslimotorsiq.com", null, null, true, null, out createStatus);
                user.OwnerId = "aslimotor@aslimotorsiq.com";
                user.BranchId = "aslimotor@aslimotorsiq.com";
                Membership.UpdateUser(user);
                if (!Roles.RoleExists(RoleName.ADMINISTRATOR))
                    Roles.CreateRole(RoleName.ADMINISTRATOR);

                Roles.AddUserToRole("administrator", RoleName.ADMINISTRATOR);
                createStatus.ShouldEqual(MembershipCreateStatus.Success);
            }
        };
        It should_user_created = () =>
        {
            var user = Membership.GetUser("administrator");
            user.ShouldNotBeNull();
        };
        It must_be_valid_user = () =>
        {
            bool result = Membership.ValidateUser("administrator", "123456");
            result.ShouldBeTrue();
        };

        Cleanup removeregisteredUser = () =>
        {
            //var result = Membership.DeleteUser("demo", true);
            //result.ShouldBeTrue();
        };
    }
}
