using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Web.Security;
using AsliMotor.Security.Models;

namespace AsliMotor.Fixture
{
    [Subject("create user")]
    class when_create_user
    {
        Establish context = () =>
        {
        };
        Because of = () =>
        {
            MembershipCreateStatus createStatus;
            Users user = Membership.CreateUser("admin", "123456", "admin@aslimotor.com", null, null, true, null, out createStatus);
            user.OwnerId = "dny@gmail.com";
            user.BranchId = "dny@gmail.com";
            Membership.UpdateUser(user);
            createStatus.ShouldEqual(MembershipCreateStatus.Success);
        };
        It should_user_created = () =>
        {
            string username = Membership.GetUserNameByEmail("admin@aslimotor.com");
            var user = Membership.GetUser(username);
            user.ShouldNotBeNull();
        };
        It must_be_valid_user = () =>
        {
            bool result = Membership.ValidateUser("demo", "123456");
            result.ShouldBeTrue();
        };

        Cleanup removeregisteredUser = () =>
        {
            //var result = Membership.DeleteUser("demo", true);
            //result.ShouldBeTrue();
        };
    }
}
