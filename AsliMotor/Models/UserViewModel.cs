﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsliMotor.Models
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
        public bool Locked { get; set; }
    }
}