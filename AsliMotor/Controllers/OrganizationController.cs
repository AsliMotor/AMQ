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
using AsliMotor.Security.Models;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        IOrganizationRepository _orgRepo;
        IOrganizationService _orgService;
        public ActionResult Index()
        {
            return View();
        }

        public string Name()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            return OrganizationRepository.GetOrganization(cp.BranchId).OrganizationName;
        }
        [HttpGet]
        public JsonResult ProfileOrganization()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            Organization org = OrganizationRepository.GetOrganization(cp.BranchId);
            return Json(org, JsonRequestBehavior.AllowGet);
        }
        [MyAuthorize(Roles=RoleName.OWNER_ADMINISTRATOR)]
        [HttpPost]
        public JsonResult UpdateProfile(Organization newOrg)
        {
            try
            {
                if (newOrg != null)
                {
                    CompanyProfile cp = new CompanyProfile(this.HttpContext);
                    Organization org = OrganizationRepository.GetOrganization(cp.BranchId);
                    newOrg.BranchId = org.BranchId;
                    OrganizationService.Update(newOrg);
                    return Json(new { error = false, data= newOrg }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { error = true, message = "Gagal ubah profil perusahaan." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize(Roles = RoleName.OWNER_ADMINISTRATOR)]
        [HttpPost]
        public ActionResult UpdateLogo(HttpPostedFileBase image)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            Stream stream = image.InputStream;
            Image img = Image.FromStream(stream);
            OrganizationService.SaveLogo(img, cp.BranchId);
            return RedirectToAction("Index");
        }

        public ActionResult Logo()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            //Image img = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\img\logoorg\" + cp.BranchId + ".png"));
            //MemoryStream ms = new MemoryStream();
            //img.Save(ms, ImageFormat.Png);
            LogoOrganization logoOrg = OrganizationRepository.GetLogoOrganization(cp.BranchId);
            return File(logoOrg.Image, "image/png");
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
        private IOrganizationService OrganizationService
        {
            get
            {
                if (_orgService == null)
                    _orgService = new OrganizationService();
                return _orgService;
            }
        }
    }
}
