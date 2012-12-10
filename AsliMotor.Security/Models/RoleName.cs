using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Security.Models
{
    public class RoleName
    {
        public const string ADMINISTRATOR = "Administrator";
        public const string OWNER = "Owner";
        public const string ADMINPURCHASE = "AdminPurchase";
        public const string ADMINSALES = "AdminSales";
        public const string CASHIER = "Cashier";
        public const string OWNER_ADMINPURCHASE = "Owner, AdminPurchase";
        public const string OWNER_ADMINPURCHASE_CASHIER = "Owner, AdminPurchase, Cashier";
        public const string OWNER_ADMINSALES = "Owner, AdminSales";
        public const string OWNER_ADMINSALES_CASHIER = "Owner, AdminSales, Cashier";
    }
}
