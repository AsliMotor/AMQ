using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Security
{
    public interface IAuthService
    {
        string ValidateUser(string username, string password);
    }
}
