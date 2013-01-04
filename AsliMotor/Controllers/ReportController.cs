using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Helper;
using AsliMotor.Security.Models;

namespace AsliMotor.Controllers
{
    [Authorize(Roles = RoleName.OWNER)]
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
