using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Security;

namespace AsliMotor.Fixture
{
    [Subject("signup new user")]
    public class when_signup
    {
        static IUserManagement userManage;

        Establish context = () =>
        {
            userManage = new UserManagement(new UserRepository());
        };

        Because of = () =>
        {
            userManage.SignUp("dnywu", "123", "dnywu@gmail.com");
        };

        It should_be_create_new_user = () =>
        {

        };
    }
}
