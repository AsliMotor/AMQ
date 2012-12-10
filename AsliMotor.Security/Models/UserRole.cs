using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Security.Models
{
    [NamedSqlQuery("remove", @"delete from userrole where rolename = @rolename and applicationname = @applicationname")]
    [NamedSqlQuery("removeByUserId", @"delete from userrole where userid = @userid")]
    [NamedSqlQuery("findByUsername", @"select * from userrole where username = @username and applicationname = @applicationname")]
    [NamedSqlQuery("findByRoleName", @"select * from userrole where rolename = @rolename and applicationname = @applicationname")]
    [NamedSqlQuery("findByUserNameAndRoleName",@"select * from userrole where username = @username and rolename = @rolename and applicationname = @applicationname")]
    public class UserRole : BaseSecurityModel
    {
        public UserRole() { }
        public UserRole(Users user, Role role)
            : base(Guid.NewGuid(), user.Name, role.ApplicationName)
        {
            assertValidUserAndRole(user, role);
            UserId = user.Id;
            UserName = user.Name;
            RoleId = role.Id;
            RoleName = role.Name;
            this.ApplicationName = role.ApplicationName;
        }
        public override string Name { get; set; }
        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public Guid RoleId { get; private set; }
        public string RoleName { get; private set; }
        public override string ApplicationName { get; set; }
        public object Key
        {
            get { return String.Concat(UserName, ".", RoleName, ".", ApplicationName); }
        }

        private void assertValidUserAndRole(Users user, Role role)
        {
            user.ReportIfNull("User cannot be null");
            role.ReportIfNull("Role cannot be null");
            if (user.ApplicationName != role.ApplicationName)
                throw new Exception(String.Format("Cannot add user {0} of {1} to {2} of {3}, because different application name", user, user.ApplicationName, role, role.ApplicationName));
        }
    }
}
