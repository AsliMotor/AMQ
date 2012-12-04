using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Web.Security;

namespace AsliMotor.Fixture
{
    [Subject("register a role")]
    class when_register_a_role
    {
        Establish context = () =>
        {
        };
        Because of = () =>
        {
            Roles.CreateRole("Administrator");
        };
        It should_be_register_a_role = () =>
        {
            Roles.RoleExists("Administrator").ShouldEqual(true);
        };
        Cleanup remove_role = () =>
        {
            Roles.DeleteRole("Administrator");
        };
    }
}
