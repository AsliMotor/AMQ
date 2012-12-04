using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using BonaStoco.Inf.Reporting;
using System.Collections.Specialized;
using Spring.Context.Support;
using AsliMotor.Security.Models;
using BonaStoco.Inf.DataMapper.Impl;
using System.Text.RegularExpressions;

namespace AsliMotor.Security.Provider
{
    public class AsliMotorRoleProvider : RoleProvider
    {
        IReportingRepository _reportingRepository;
        QueryObjectMapper _queryObjectMapper;

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config.IsNull())
                throw new ArgumentNullException("config");

            if (String.IsNullOrEmpty(name))
                name = "NHRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "AsliMotor Role provider");
            }

            base.Initialize(name, config);

            if (String.IsNullOrEmpty(config["applicationName"]) || config["applicationName"].Trim() == "")
            {
                ApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                ApplicationName = config["applicationName"];
            }

            _reportingRepository = ContextRegistry.GetContext().GetObject("ReportingRepository") as IReportingRepository;
            _queryObjectMapper = ContextRegistry.GetContext().GetObject("QueryObjectMapper") as QueryObjectMapper;
        }

        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            var users = findUsersByName(usernames);
            var roles = findRolesByName(rolenames);

            foreach (var user in users)
            {
                foreach (var role in roles)
                {
                    _reportingRepository.Save<UserRole>(role.AddUser(user));
                }
            }
        }

        public override void CreateRole(string rolename)
        {
            if (RoleExists(rolename))
                throw new Exception(String.Format("Role {0} already exists.", rolename));

            var role = new Role(rolename, ApplicationName);
            _reportingRepository.Save<Role>(role);
        }

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            if (!RoleExists(rolename))
                throw new Exception(String.Format("Role name {0} not found.", rolename));

            if (throwOnPopulatedRole && GetUsersInRole(rolename).IsNotEmpty())
                throw new Exception("Cannot delete a populated role.");

            _queryObjectMapper.Map<Role>("remove", new string[] { "name", "applicationname" }, new object[] { rolename, ApplicationName });
            _queryObjectMapper.Map<UserRole>("remove", new string[] { "rolename", "applicationname" }, new object[] { rolename, ApplicationName });
            return true;
        }

        public override string[] GetAllRoles()
        {
            IList<Role> roles = _queryObjectMapper.Map<Role>("findByApplicationName", new string[] { "applicationname" }, new object[] { ApplicationName }).ToList();
            return roles.Select(r => r.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            IList<UserRole> userRoles = _queryObjectMapper.Map<UserRole>("findByUsername", new string[] { "username", "applicationname" }, new object[] { username, ApplicationName }).ToList();
            //var cursor = pUserRolesCollection.Find(Query.And(Query.EQ("UserName", username), Query.EQ("ApplicationName", ApplicationName)));
            return userRoles.Select(ur => ur.RoleName).ToArray();
        }

        public override string[] GetUsersInRole(string rolename)
        {
            IList<UserRole> userRoles = _queryObjectMapper.Map<UserRole>("findByRoleName", new string[] { "rolename", "applicationname" }, new object[] { rolename, ApplicationName }).ToList();
            //var cursor = pUserRolesCollection.Find(Query.And(Query.EQ("RoleName", rolename), Query.EQ("ApplicationName", ApplicationName)));
            return userRoles.Select(ur => ur.UserName).ToArray();
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            UserRole userRole = _queryObjectMapper.Map<UserRole>("findByUserNameAndRoleName", new string[] { "username", "rolename", "applicationname" }, new object[] { username, rolename, ApplicationName }).FirstOrDefault();
            //return pUserRolesCollection.IsExist(Query.And(Query.EQ("UserName", username), Query.EQ("RoleName", rolename), Query.EQ("ApplicationName", ApplicationName)));
            return userRole != null;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            var users = findUsersByName(usernames);
            IList<Role> roles = findRolesByName(rolenames);

            foreach (var user in users)
            {
                foreach (Role role in roles)
                {
                    _queryObjectMapper.Map<UserRole>("remove", new string[] { "rolename", "applicationname" }, new object[] { role.Name, ApplicationName });
                }
            }
        }

        public override bool RoleExists(string rolename)
        {
            Role role = _queryObjectMapper.Map<Role>("findByName", new string[] { "name", "applicationname" }, new object[] { rolename, ApplicationName }).FirstOrDefault();
            return role != null;
        }

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            IList<UserRole> userRole = _queryObjectMapper.Map<UserRole>("findByUserNameAndRoleName", new string[] { "username", "rolename", "applicationname" }, new object[] { new Regex(usernameToMatch), rolename, ApplicationName }).ToList();
            //var cursor = pUserRolesCollection.Find(Query.And(Query.EQ("RoleName", rolename), Query.Matches("UserName",
            //    BsonRegularExpression.Create(new Regex(usernameToMatch))), Query.EQ("ApplicationName", ApplicationName)));
            return userRole.Select(ur => ur.UserName).ToArray();
        }

        private IList<Role> findRolesByName(string[] rolenames)
        {
            IList<Role> roles = new List<Role>();
            foreach (string role in rolenames)
            {
                 roles.Add(_queryObjectMapper.Map<Role>("findByName", new string[] { "name", "applicationname" }, new object[] { role, ApplicationName }).FirstOrDefault());
            }
            if(roles == null)
                throw new Exception(String.Format(String.Format("User {0} are not found.", createMessage(rolenames))));
            //var roles = findByNames<Role>(pRolesCollection, rolenames, "Role {0} are not found.");
            return roles;
        }
        private IList<Users> findUsersByName(string[] usernames)
        {
            IList<Users> users = new List<Users>();
            foreach (string username in usernames)
            {
                users.Add(_queryObjectMapper.Map<Users>("findByName", new string[] { "name", "applicationname" }, new object[] { username, ApplicationName }).FirstOrDefault());
            }
            //return findByNames<Users>(pUsersCollection, usernames, "User {0} are not found.");
            return users;
        }
        private string createMessage(IEnumerable<string> tokenNames)
        {
            return String.Join(", ", tokenNames.ToArray());
        }
    }
}
