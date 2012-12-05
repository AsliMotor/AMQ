﻿using System;
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
        public const string ADMINISTRATOR_OWNER_ADMINPURCHASE = "Administrator, Owner, AdminPurchase";
        public const string ADMINISTRATOR_OWNER_ADMINPURCHASE_CASHIER = "Administrator, Owner, AdminPurchase, Cashier";
        public const string ADMINISTRATOR_OWNER_ADMINSALES = "Administrator, Owner, AdminSales";
        public const string ADMINISTRATOR_OWNER_ADMINSALES_CASHIER = "Administrator, Owner, AdminSales, Cashier";
    }
}