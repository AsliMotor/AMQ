using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Security.Models
{
    [NamedSqlQuery("findByEmail", @"SELECT * FROM users where email = @email")]
    [NamedSqlQuery("findByEmailAndApplicationName", @"SELECT * FROM users where email = @email and applicationname = @applicationname")]
    [NamedSqlQuery("findByApplicationName", @"SELECT * FROM users where applicationname = @applicationname")]
    [NamedSqlQuery("findUserOnline", @"SELECT * FROM users where lastactivitydate > @lastactivitydate and applicationname = @applicationname")]
    [NamedSqlQuery("findByName", @"SELECT * FROM users where name = @name and applicationname = @applicationname")]
    [NamedSqlQuery("deleteByEmail", @"DELETE FROM users where email = @email")]
    public class Users : BaseSecurityModel
    {
        public const string ADMINISTRATOR_USER = "administrator";

        //protected User() { }
        private Users(Guid id, string name, string applicationName) : base(id, name, applicationName) { }
        public Users() { }
        public Users(string username, string applicationname, string email, string password)
            : base(Guid.NewGuid(), username, applicationname)
        {
            Email = email;
            Password = ASCIIEncoding.ASCII.GetBytes(password);
        }
        public string ProviderName { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public bool ResetPasswordOnFirstLogin { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public bool IsApproved { get; set; }
        public bool EmailNotification { get; set; }
        public string OwnerId { get; set; }
        public string BranchId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime LastLockedOutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptWindowStart { get; set; }
        public int FailedPasswordAnswerAttemptCount { get; set; }
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

        public override string ToString()
        {
            return String.Format("{0}({1})", this.Name, Email);
        }

        public object Key { get { return Email; } }

        public static implicit operator MembershipUser(Users user)
        {
            return new MembershipUser(user.ProviderName, user.Name, new UserExtendedData(user.Id, user.EmailNotification, user.OwnerId, user.BranchId), user.Email, user.PasswordQuestion,
                user.Comment, user.IsApproved, user.IsLockedOut, user.DateCreated, user.LastLoginDate, user.LastActivityDate,
                user.LastPasswordChangedDate, user.LastLockedOutDate);
        }
        public static implicit operator Users(MembershipUser user)
        {
            var userExtData = user.ProviderUserKey as UserExtendedData;
            var u = new Users(userExtData.UserId, user.UserName, Membership.ApplicationName);
            u.ProviderName = user.ProviderName;
            u.Email = user.Email;
            u.PasswordQuestion = user.PasswordQuestion;
            u.Comment = user.Comment;
            u.IsApproved = user.IsApproved;
            u.IsLockedOut = user.IsLockedOut;
            u.DateCreated = user.CreationDate;
            u.LastLoginDate = user.LastLoginDate;
            u.LastActivityDate = user.LastActivityDate;
            u.LastPasswordChangedDate = user.LastPasswordChangedDate;
            u.LastLockedOutDate = user.LastLockoutDate;
            u.EmailNotification = userExtData.EmailNotification;
            u.OwnerId = userExtData.OwnerId;
            u.BranchId = userExtData.BranchId;
            return u;
        }

        class UserExtendedData
        {
            public UserExtendedData(Guid id, bool emailNotification, string ownerId, string branchId)
            {
                UserId = id;
                EmailNotification = emailNotification;
                OwnerId = ownerId;
                BranchId = branchId;
            }
            public Guid UserId { get; private set; }
            public bool EmailNotification { get; private set; }
            public string OwnerId { get; set; }
            public string BranchId { get; set; }
        }
    }
}