using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Security
{
    public interface IUserRepository
    {
        Account GetUserByEmail(string email);
        Account GetUserByUserName(string username);
        Account GetUserByBranchId(string branchId);
        void Save(Account user);
    }
}
