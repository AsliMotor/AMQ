using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;
using System.Web.Security;

namespace AsliMotor.Security
{
    [NamedSqlQuery("FindUserByEmail",@"SELECT * FROM account WHERE email = @email")]
    [NamedSqlQuery("FindUserByUsername", @"SELECT * FROM account WHERE username = @username")]
    [NamedSqlQuery("FindUserByBranchId", @"SELECT * FROM account WHERE branchid = @branchid")]
    
    public class Account : IViewModel
    {
        public string OwnerId { get; set; }
        public string BranchId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }

        public override string ToString()
        {
            return String.Format("{0}({1})", this.UserName, Email);
        }
    }
}
