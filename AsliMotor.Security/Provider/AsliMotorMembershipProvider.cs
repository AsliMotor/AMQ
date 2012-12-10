﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Collections.Specialized;
using System.Security.Cryptography;
using AsliMotor.Security.Models;
using BonaStoco.Inf.Reporting;
using Spring.Context.Support;
using BonaStoco.Inf.DataMapper.Impl;

namespace AsliMotor.Security.Provider
{
    public class AsliMotorMembershipProvider:MembershipProvider
    {
        IReportingRepository _reportingRepository;
        QueryObjectMapper _queryObjectMapper;
        bool pEnablePasswordReset;
        bool pEnablePasswordRetrieval;
        bool pRequiresQuestionAndAnswer;
        bool pRequiresUniqueEmail;
        int pMaxInvalidPasswordAttempts;
        int pPasswordAttemptWindow;
        MembershipPasswordFormat pPasswordFormat;
        int pMinRequiredNonAlphanumericCharacters;
        int pMinRequiredPasswordLength;
        string pPasswordStrengthRegularExpression;
        byte[] encryptionKey;

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config.IsNull())
                throw new ArgumentNullException("config");

            if (name.IsNullOrWhiteSpace())
                name = "AsliMotorMembershipProvider";

            if (config["description"].IsNullOrWhiteSpace())
            {
                config.Remove("description");
                config.Add("description", "AsliMotor Membership provider");
            }

            base.Initialize(name, config);

            ApplicationName = getConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            pMaxInvalidPasswordAttempts = Convert.ToInt32(getConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            pPasswordAttemptWindow = Convert.ToInt32(getConfigValue(config["passwordAttemptWindow"], "10"));
            pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(getConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            pMinRequiredPasswordLength = Convert.ToInt32(getConfigValue(config["minRequiredPasswordLength"], "7"));
            pPasswordStrengthRegularExpression = Convert.ToString(getConfigValue(config["passwordStrengthRegularExpression"], ""));
            pEnablePasswordReset = Convert.ToBoolean(getConfigValue(config["enablePasswordReset"], "true"));
            pEnablePasswordRetrieval = Convert.ToBoolean(getConfigValue(config["enablePasswordRetrieval"], "true"));
            pRequiresQuestionAndAnswer = Convert.ToBoolean(getConfigValue(config["requiresQuestionAndAnswer"], "false"));
            pRequiresUniqueEmail = Convert.ToBoolean(getConfigValue(config["requiresUniqueEmail"], "true"));

            encryptionKey = HexToByte(getConfigValue(config["encryptionKey"], "ABCDEEA2EFAA00B42A"));

            string password_format = config["passwordFormat"];
            if (password_format.IsNullOrWhiteSpace())
            {
                password_format = "Hashed";
            }

            switch (password_format)
            {
                case "Hashed":
                    pPasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    pPasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    pPasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new Exception(String.Format("PasswordFormatNotSupportedMessage"));
            }
            _reportingRepository = (IReportingRepository) ContextRegistry.GetContext().GetObject("ReportingRepository");
            _queryObjectMapper = (QueryObjectMapper) ContextRegistry.GetContext().GetObject("QueryObjectMapper");
        }

        public override string ApplicationName { get; set; }
        public override bool EnablePasswordReset { get { return pEnablePasswordReset; } }
        public override bool EnablePasswordRetrieval { get { return pEnablePasswordRetrieval; } }
        public override bool RequiresQuestionAndAnswer { get { return pRequiresQuestionAndAnswer; } }
        public override bool RequiresUniqueEmail { get { return pRequiresUniqueEmail; } }
        public override int MaxInvalidPasswordAttempts { get { return pMaxInvalidPasswordAttempts; } }
        public override int PasswordAttemptWindow { get { return pPasswordAttemptWindow; } }
        public override MembershipPasswordFormat PasswordFormat { get { return pPasswordFormat; } }
        public override int MinRequiredNonAlphanumericCharacters { get { return pMinRequiredNonAlphanumericCharacters; } }
        public override int MinRequiredPasswordLength { get { return pMinRequiredPasswordLength; } }
        public override string PasswordStrengthRegularExpression { get { return pPasswordStrengthRegularExpression; } }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!ValidateUser(username, oldPassword))
                throw new Exception("Old password is not valid. Change password failed.");

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new Exception("New password is not valid.");
            }
            if (newPassword.Length < MinRequiredPasswordLength)
                throw new Exception(String.Format("Password length is too weak. Minimum password length is {0}", MinRequiredPasswordLength));

            Users user = getProviderUser(username, true);
            string dbAnswer = user.PasswordAnswer;
            string pwSalt = ASCIIEncoding.ASCII.GetString(user.PasswordSalt);
            user.Password = ASCIIEncoding.ASCII.GetBytes(encodePassword(newPassword, pwSalt));
            user.PasswordAnswer = encodePassword(dbAnswer, pwSalt);
            user.LastPasswordChangedDate = DateTime.Now;
            user.ResetPasswordOnFirstLogin = false;
            user.FailedPasswordAnswerAttemptCount = 0;
            user.FailedPasswordAttemptCount = 0;
            _reportingRepository.Update<Users>(user, new { Id = user.Id });
            return true;
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPwdQuestion, string newPwdAnswer)
        {
            if (!ValidateUser(username, password))
                return false;

            Users user = getProviderUser(username, true);
            string passSalt = ASCIIEncoding.ASCII.GetString(user.PasswordSalt);
            user.PasswordQuestion = newPwdQuestion;
            user.PasswordAnswer = encodePassword(newPwdAnswer, passSalt);
            _reportingRepository.Update<Users>(user, new { Id = user.Id });
            return true;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (this.RequiresQuestionAndAnswer)
            {
                if (passwordQuestion.IsNullOrWhiteSpace())
                {
                    status = MembershipCreateStatus.InvalidQuestion;
                    return null;
                }
                else if (passwordAnswer.IsNullOrWhiteSpace())
                {
                    status = MembershipCreateStatus.InvalidQuestion;
                    return null;
                }
            }
            if (password.Length < MinRequiredPasswordLength)
                throw new Exception(String.Format("Password length is too weak. Minimum password length is {0}", MinRequiredPasswordLength));

            string name = GetUserNameByEmail(email);
            if (RequiresUniqueEmail && !name.IsNullOrWhiteSpace())
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            Users isUserExist = _queryObjectMapper.Map<Users>("findByName", new string[] { "name", "applicationname" }, new object[] { username, ApplicationName }).FirstOrDefault();
            if (isUserExist == null)
            {
                //if (providerUserKey.IsNull())
                //{
                //    status = MembershipCreateStatus.InvalidProviderUserKey;
                //    return null;
                //}
                string salt = getPasswordSalt();
                Users user = new Users(username, ApplicationName, email, encodePassword(password, salt));
                user.DateCreated = DateTime.Now;
                user.LastPasswordChangedDate = DateTime.Now;
                user.PasswordSalt = ASCIIEncoding.ASCII.GetBytes(salt);
                user.ProviderName = this.Name;
                user.PasswordQuestion = passwordQuestion;
                user.PasswordAnswer = passwordAnswer.IsNullOrWhiteSpace() ? null : encodePassword(passwordAnswer, salt);
                user.IsApproved = isApproved;
                user.ApplicationName = ApplicationName;
                user.IsLockedOut = false;
                user.LastLockedOutDate = DateTime.Now;
                user.FailedPasswordAttemptWindowStart = DateTime.Now;
                user.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;

                try
                {
                    _reportingRepository.Save<Users>(user);
                    status = MembershipCreateStatus.Success;
                }
                catch (Exception e)
                {
                    status = MembershipCreateStatus.UserRejected;
                    throw e;
                }

                return user;
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            Users user = getProviderUser(username, true);
            //_reportingRepository.Delete<Users>(user);
            _queryObjectMapper.Map<Users>("deleteByEmail", new string[] { "email" }, new object[] { user.Email});
            //pUsersCollection.Remove(Query.EQ("Email", user.Email), SafeMode.True);
            if (deleteAllRelatedData)
                _queryObjectMapper.Map<UserRole>("removeByUserId", new string[] { "userid" }, new object[] { user.Id });
            return true;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection membershipUsers = new MembershipUserCollection();
            int startIndex = pageSize * pageIndex;
            IEnumerable<Users> users = _queryObjectMapper.Map<Users>("findByApplicationName", new string[] { "applicationname" }, new object[] { ApplicationName });
            //IEnumerable<User> users = pUsersCollection.FindByApplicationName(ApplicationName, startIndex, pageSize);
            totalRecords = users.Count();
            if (totalRecords == 0)
                return membershipUsers;

            foreach (var user in users)
            {
                membershipUsers.Add(user);
            }
            return membershipUsers;
        }

        public override int GetNumberOfUsersOnline()
        {
            TimeSpan onlineSpan = new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);
            IEnumerable<Users> userOnline = _queryObjectMapper.Map<Users>("findUserOnline", new string[] { "applicationname", "lastactivitydate" }, new object[] { ApplicationName, compareTime });
            //int userOnline = Convert.ToInt32(pUsersCollection.Count(Query.And(Query.EQ("ApplicationName", ApplicationName), Query.GTE("LastActivityDate", compareTime))));
            return userOnline.Count();
        }

        public override string GetPassword(string username, string answer)
        {
            if (!EnablePasswordRetrieval)
                throw new Exception("Password retrieval is not enabled.");

            if (PasswordFormat == MembershipPasswordFormat.Hashed)
                throw new Exception("Cannot retrieve hashed passwords.");

            Users user = getProviderUser(username, true);
            if (user.IsLockedOut)
                throw new Exception(String.Format("Wrong password. Maximum invalid password attempt {0} has reached.", MaxInvalidPasswordAttempts));

            string password = ASCIIEncoding.ASCII.GetString(user.Password);
            string dbAnswer = user.PasswordAnswer;
            string passSalt = ASCIIEncoding.ASCII.GetString(user.PasswordSalt);

            if (RequiresQuestionAndAnswer && !validatePassword(answer, dbAnswer, passSalt))
            {
                updateFailureCount(user, FailureType.PasswordAnswer);
                throw new Exception(String.Format("IncorrectPasswordAnswerMessage"));
            }

            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
                password = decodePassword(password);

            return password;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            Users user = getProviderUser(username, false);
            if (user.IsNull()) return null;
            if (userIsOnline)
            {
                user.LastActivityDate = DateTime.Now;
                _reportingRepository.Update<Users>(user, new { Id = user.Id });
            }
            MembershipUser memUser = user;
            return memUser;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            providerUserKey.ReportIfNull("Argument ProviderUserKey on GetUser(object,bool)");
            Users user = _queryObjectMapper.Map<Users>("findByEmailAndApplicationName", new string[] { "email", "applicationname" }, new object[] { providerUserKey.ToString(), ApplicationName }).FirstOrDefault();
            //User user = pUsersCollection.FindOneAs<User>(Query.And(Query.EQ("Email", providerUserKey.ToString()), Query.EQ("ApplicationName", ApplicationName)));
            if (user.IsNull()) return null;
            if (userIsOnline)
            {
                user.LastActivityDate = DateTime.Now;
                _reportingRepository.Update<Users>(user, new { Id = user.Id });
            }
            MembershipUser memUser = user;
            return memUser;
        }

        public override bool UnlockUser(string username)
        {
            Users user = getProviderUser(username, true);
            user.IsLockedOut = false;
            user.LastLockedOutDate = DateTime.Now;
            _reportingRepository.Update<Users>(user, new { Id = user.Id });
            return true;
        }

        public override string GetUserNameByEmail(string email)
        {
            Users user = _queryObjectMapper.Map<Users>("findByEmail", new string[] { "email" }, new object[] { email }).FirstOrDefault();
            //User user = pUsersCollection.FindOneAs<User>(Query.EQ("Email", email));
            if (user.IsNull()) return String.Empty;
            return user.Name;
        }

        public override string ResetPassword(string username, string answer)
        {
            Users user = getProviderUser(username, true);
            if (!EnablePasswordReset)
                throw new Exception("Password reset is not enabled.");

            if (RequiresQuestionAndAnswer && (answer.IsNull() || answer.Length == 0))
            {
                updateFailureCount(user, FailureType.PasswordAnswer);
                throw new Exception("Password answer required.");
            }

            string newPassword = Membership.GeneratePassword(MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new Exception("Reset password canceled.");
            }

            bool isLockedOut = false;
            string salt = String.Empty;
            string dbAnswer = getPasswordAnswer(username, out isLockedOut, out salt);

            if (isLockedOut)
                throw new Exception(String.Format("Wrong password. Max invalid password attempts {0} has been reached.", MaxInvalidPasswordAttempts));

            if (RequiresQuestionAndAnswer && !validatePassword(answer, dbAnswer, salt))
            {
                updateFailureCount(user, FailureType.PasswordAnswer);
                throw new Exception("Incorrect password answer.");
            }
            user.Password = ASCIIEncoding.ASCII.GetBytes(encodePassword(newPassword, salt));
            user.PasswordSalt = ASCIIEncoding.ASCII.GetBytes(salt);
            user.PasswordAnswer = encodePassword(dbAnswer, salt);
            user.LastPasswordChangedDate = DateTime.Now;
            user.ResetPasswordOnFirstLogin = false;
            user.FailedPasswordAttemptCount = 0;
            user.FailedPasswordAttemptWindowStart = DateTime.Now;
            user.FailedPasswordAnswerAttemptCount = 0;
            user.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
            try
            {
                _reportingRepository.Update<Users>(user, new { Id = user.Id });
            }
            catch
            {
                throw new Exception(String.Format("Password {0} not reset.", username));
            }
            return newPassword;
        }
        public override void UpdateUser(MembershipUser user)
        {
            Users puser = getProviderUser(user.UserName, true);
            puser.Email = user.Email;
            puser.EmailNotification = ((Users)user).EmailNotification;
            puser.OwnerId = ((Users)user).OwnerId;
            puser.BranchId = ((Users)user).BranchId;
            puser.Comment = user.Comment;
            puser.IsApproved = user.IsApproved;
            _reportingRepository.Update<Users>(puser, new { Id = puser.Id });
        }

        public override bool ValidateUser(string username, string password)
        {
            if (username.IsNullOrWhiteSpace() || password.IsNullOrWhiteSpace())
                return false;
            bool isValid = false;
            Users user = _queryObjectMapper.Map<Users>("findByName", new string[] { "name", "applicationname" }, new object[] { username, ApplicationName }).FirstOrDefault();
            //User user = pUsersCollection.FindByName<User>(username, ApplicationName);
            if (user.IsNull())
                return false;

            if (user.IsLockedOut)
                throw new Exception(String.Format("Wrong password. Max invalid password attempts {0} has been reached.", MaxInvalidPasswordAttempts));
            string pwd = ASCIIEncoding.ASCII.GetString(user.Password);
            string salt = user.PasswordSalt != null ? ASCIIEncoding.ASCII.GetString(user.PasswordSalt) : String.Empty;

            if (validatePassword(password, pwd, salt))
            {
                if (user.IsApproved)
                {
                    isValid = true;
                    user.LastLoginDate = DateTime.Now;
                    user.LastActivityDate = DateTime.Now;
                    user.FailedPasswordAttemptCount = 0;
                    user.FailedPasswordAttemptWindowStart = DateTime.Now;
                    user.FailedPasswordAnswerAttemptCount = 0;
                    user.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
                    _reportingRepository.Update<Users>(user, new { Id = user.Id });
                }
            }
            else if (!user.Name.Equals(Users.ADMINISTRATOR_USER))
                updateFailureCount(user, FailureType.Password);

            return isValid;
        }

        //public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        //{
        //    totalRecords = 0;
        //    MembershipUserCollection membershipUsers = new MembershipUserCollection();
        //    if (usernameToMatch.IsNullOrWhiteSpace())
        //        return membershipUsers;

        //    int startIndex = pageSize * pageIndex;
        //    IEnumerable<User> users = pUsersCollection.FindLike<User>("Name", usernameToMatch, ApplicationName, startIndex, pageSize);
        //    totalRecords = users.Count();
        //    if (totalRecords == 0)
        //        return membershipUsers;

        //    foreach (User user in users)
        //    {
        //        membershipUsers.Add(user);
        //    }
        //    return membershipUsers;
        //}

        //public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        //{
        //    totalRecords = 0;
        //    MembershipUserCollection membershipUsers = new MembershipUserCollection();
        //    if (emailToMatch.IsNullOrWhiteSpace())
        //        return membershipUsers;

        //    int startIndex = pageSize * pageIndex;
        //    IEnumerable<User> users = pUsersCollection.FindLike<User>("Email", emailToMatch, ApplicationName, startIndex, pageSize);
        //    totalRecords = users.Count();
        //    if (totalRecords == 0)
        //        return membershipUsers;

        //    foreach (User user in users)
        //    {
        //        membershipUsers.Add(user);
        //    }
        //    return membershipUsers;
        //}
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        #region private
        private string getConfigValue(string configValue, string defaultValue)
        {
            return configValue.IsNullOrWhiteSpace() ? defaultValue : configValue;
        }
        private void updateFailureCount(Users user, FailureType failureType)
        {
            DateTime windowStart = new DateTime();
            int failureCount = 0;
            switch (failureType)
            {
                case FailureType.Password:
                    failureCount = user.FailedPasswordAttemptCount;
                    windowStart = user.FailedPasswordAttemptWindowStart;
                    break;
                case FailureType.PasswordAnswer:
                    failureCount = user.FailedPasswordAnswerAttemptCount;
                    windowStart = user.FailedPasswordAnswerAttemptWindowStart;
                    break;
            }
            DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);
            if (failureCount == 0 || DateTime.Now > windowEnd)
            {
                switch (failureType)
                {
                    case FailureType.Password:
                        user.FailedPasswordAttemptCount = 1;
                        user.FailedPasswordAttemptWindowStart = DateTime.Now;
                        break;
                    case FailureType.PasswordAnswer:
                        user.FailedPasswordAnswerAttemptCount = 1;
                        user.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
                        break;
                }
            }
            else if (++failureCount >= MaxInvalidPasswordAttempts)
            {
                user.IsLockedOut = true;
                switch (failureType)
                {
                    case FailureType.Password:
                        user.FailedPasswordAttemptCount = failureCount;
                        user.FailedPasswordAttemptWindowStart = windowEnd;
                        break;
                    case FailureType.PasswordAnswer:
                        user.FailedPasswordAnswerAttemptCount = failureCount;
                        user.FailedPasswordAnswerAttemptWindowStart = windowEnd;
                        break;
                }
                user.LastLockedOutDate = DateTime.Now;
            }
            else
            {
                switch (failureType)
                {
                    case FailureType.Password:
                        user.FailedPasswordAttemptCount = failureCount;
                        break;
                    case FailureType.PasswordAnswer:
                        user.FailedPasswordAnswerAttemptCount = failureCount;
                        break;
                }
            }
            _reportingRepository.Update<Users>(user, new { Id = user.Id });
        }

        private bool validatePassword(string password, string dbpassword, string salt)
        {
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    return password == decodePassword(dbpassword);
                case MembershipPasswordFormat.Hashed:
                    return dbpassword == encodePassword(password, salt);
                default:
                    return false;
            }
        }

        static SHA1 hash = null;
        private string encodePassword(string password, string salt)
        {
            if (password.IsNull())
                return null;
            if (salt.IsNull() || salt.Length == 0)
                return password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    return password;
                case MembershipPasswordFormat.Encrypted:
                    return Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                case MembershipPasswordFormat.Hashed:
                    if (hash.IsNull())
                        hash = SHA1Managed.Create();
                    return Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password + salt)));
                default:
                    throw new Exception("Unsupported password format.");
            }
        }
        private string getPasswordSalt()
        {
            return Membership.GeneratePassword(24, 12);
        }

        private string decodePassword(string encodedPassword)
        {
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    return encodedPassword;
                case MembershipPasswordFormat.Encrypted:
                    return Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(encodedPassword)));
                case MembershipPasswordFormat.Hashed:
                    throw new Exception("Cannot unencode a hashed password.");
                default:
                    throw new Exception("Unsupported password format.");
            }
        }
        private string getPasswordAnswer(string username, out bool isLockedOut, out string salt)
        {
            Users user = getProviderUser(username, true);
            string answer = user.PasswordAnswer;
            salt = ASCIIEncoding.ASCII.GetString(user.PasswordSalt);
            isLockedOut = user.IsLockedOut;
            return answer;
        }

        private Users getProviderUser(string username, bool throwError)
        {
            Users user = _queryObjectMapper.Map<Users>("findByName", new string[] { "name", "applicationname" }, new object[] { username, ApplicationName }).FirstOrDefault();
            //User user = pUsersCollection.FindByName<User>(username, ApplicationName);
            if (user.IsNull() && throwError)
                throw new Exception(String.Format("User {0} was not registered in {1}.", username, ApplicationName));
            return user;
        }
        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        #endregion
    }
}
