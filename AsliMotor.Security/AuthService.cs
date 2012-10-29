using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;

namespace AsliMotor.Security
{
    public class AuthService : IAuthService
    {
        private QueryObjectMapper _qryObjectMapper;
        private IUserRepository userRepo;
        public AuthService()
        {
            _qryObjectMapper = ContextRegistry.GetContext().GetObject("QueryObjectMapper") as QueryObjectMapper;
            userRepo = new UserRepository();
        }
        public string ValidateUser(string username, string password)
        {
            Account acc = userRepo.GetUserByUserName(username);
            if (acc == null) return null;
            if (acc.Password != password) return null;
            return acc.BranchId;
        }
    }
}
