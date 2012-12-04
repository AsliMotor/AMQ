using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Security.Models
{
    [NamedSqlQuery("remove", @"delete from role where name = @name and applicationname = @applicationname")]
    [NamedSqlQuery("findByApplicationName", @"select * from role where applicationname = @applicationname")]
    [NamedSqlQuery("findByName", @"select * from role where name = @name and applicationname = @applicationname")]
    public class Role : BaseSecurityModel
    {
        public const string ADMINISTRATOR_ROLE = "Administrators";
        List<UserRole> _userRoles = new List<UserRole>();

        public Role() { }
        public Role(string rolename, string applicationname)
            : base(Guid.NewGuid(), rolename, applicationname)
        { }
        public int MemberCount
        {
            get { return _userRoles.Count; }
        }
        public UserRole AddUser(Users user)
        {
            user.ReportIfNull("Argument AddUser(user)");
            if (_userRoles.Contains(ur => ur.UserId.Equals(user.Id)))
                throw new Exception(String.Format("User {0} is already in {1}", user.Name, this.Name));

            var userRole = new UserRole(user, this);
            _userRoles.Add(userRole);
            return userRole;
        }
        internal void AddUserRole(UserRole userRole)
        {
            _userRoles.Add(userRole);
        }
        public UserRole RemoveUser(Users user)
        {
            user.ReportIfNull("Argument RemoveUser(user)");
            if (!_userRoles.Contains(ur => ur.UserId.Equals(user.Id)))
                throw new Exception(String.Format("User {0} is not in {1}", user.Name, this.Name));
            if (user.Name.Equals(Users.ADMINISTRATOR_USER) && this.Name.Equals(ADMINISTRATOR_ROLE))
                throw new Exception(String.Format("Cannot remove {0} from {1}", user, this));
            foreach (var ur in _userRoles)
            {
                if (ur.UserId.Equals(user.Id))
                {
                    _userRoles.Remove(ur);
                    return ur;
                }
            }
            return null;
        }
        public IList<UserRole> UserRoles { get { return _userRoles.AsReadOnly(); } }
        public object Key
        {
            get { return String.Concat(Name, ApplicationName); }
        }
        public override string ToString()
        {
            return this.Name;
        }

        #region IEnumerable Members

        //public IEnumerator<UserRoleModel> GetEnumerator()
        //{
        //    return Roles.Values.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        #endregion
    }
}
