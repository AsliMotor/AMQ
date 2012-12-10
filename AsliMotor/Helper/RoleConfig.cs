using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using AsliMotor.Security.Models;

namespace AsliMotor.Helper
{
    public static class RoleConfig
    {
        public static void Configure()
        {
            if (!Roles.RoleExists(RoleName.ADMINISTRATOR))
            {
                Roles.CreateRole(RoleName.ADMINISTRATOR);
            }

            if (!Roles.RoleExists(RoleName.OWNER))
            {
                Roles.CreateRole(RoleName.OWNER);
            }

            if (!Roles.RoleExists(RoleName.ADMINSALES))
            {
                Roles.CreateRole(RoleName.ADMINSALES);
            }

            if (!Roles.RoleExists(RoleName.ADMINPURCHASE))
            {
                Roles.CreateRole(RoleName.ADMINPURCHASE);
            }

            if (!Roles.RoleExists(RoleName.CASHIER))
            {
                Roles.CreateRole(RoleName.CASHIER);
            }
        }
    }
}