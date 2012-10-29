using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using AsliMotor.Model;
using System.Collections.Specialized;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using System.Security.Cryptography;

namespace AsliMotor.Security
{
    public class UserManagement:IUserManagement
    {
        IUserRepository userRepo;
        public UserManagement(UserRepository repo)
        {
            userRepo = repo;
        }

        public Account SignUp(string username, string password, string email)
        {
            FailIfAlreadyRegistered(username, email);
            if (!username.IsNullOrWhiteSpace() && !email.IsNullOrWhiteSpace())
            {
                Account user = new Account();
                user.Password = password;
                user.BranchId = email;
                user.UserName = username;
                user.Email = email;
                user.OwnerId = email;
                user.DateCreated = DateTime.Now;
                user.LastActivityDate = DateTime.Now;
                user.LastLoginDate = DateTime.Now;
                user.Role = 1;
                try
                {
                    userRepo.Save(user);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return user;
            }
            return null;
        }

        private void FailIfAlreadyRegistered(string username, string email)
        {
            Account userRecord = userRepo.GetUserByEmail(email);
            if (userRecord != null)
                throw new ApplicationException(string.Format("Email ({0}) sudah terdaftar",email));
            Account userRecord1 = userRepo.GetUserByUserName(username);
            if(userRecord1 != null)
                throw new ApplicationException(string.Format("Username ({0}) sudah terdaftar", username));
        }
    }
}
