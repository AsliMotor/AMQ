using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using AsliMotor.Models;
using AsliMotor.Organizations;
using AsliMotor.Helper;

namespace AsliMotor.Controllers
{
    [MyAuthorize]
    public class OrganizationController : Controller
    {
        IOrganizationRepository _orgRepo;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Change()
        {
            return View();
        }

        public string Name()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            return OrganizationRepository.GetOrganization(cp.BranchId).OrganizationName;
        }
        public ActionResult Logo()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            return File(OrganizationRepository.GetLogoOrganization(cp.BranchId).Image, "image/png");
        }
        private IOrganizationRepository OrganizationRepository
        {
            get
            {
                if (_orgRepo == null)
                    _orgRepo = new OrganizationRepository();
                return _orgRepo;
            }
        }
    }
}
