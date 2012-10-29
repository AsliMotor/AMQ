using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Security
{
    public interface IUserManagement
    {
        Account SignUp(string username, string password, string email);
    }
}
