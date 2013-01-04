using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.SI.AutoNumberGenerator;
using AsliMotor.Models;
using AsliMotor.Receives.AutoNumberGenerator;
using AsliMotor.SuratPeringatan.AutoNumberGenerator;
using AsliMotor.Perjanjian.AutoNumberGenerator;
using Spring.Context.Support;
using AsliMotor.Security.Models;

namespace AsliMotor.Controllers
{
    [Authorize(Roles=RoleName.ADMINISTRATOR)]
    public class SettingAutoNumberController : Controller
    {
        ISIAutoNumberGenerator _siGenerator;
        IReceiveAutoNumberGenerator _rcvGenerator;
        ISuratPeringatanAutoNumberGenerator _suratPeringatanGenerator;
        IPerjanjianAutoNumberGenerator _perjanjianGenerator;
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAutoNumberSI()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            SIAutoNumberConfig config = SIAutoNumberGenerator.GetSIAutoNumberConfig(cp.BranchId);
            return Json(config, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateAutoNumberSI(int mode, string prefix)
        {
            try
            {
                if (mode < 0 || mode > 1)
                    throw new Exception("Mode tidak ditemukan");
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                SIAutoNumberGenerator.SetupSIAutoMumber(mode, prefix, cp.BranchId);
                return Json(new { error = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetAutoNumberInvoice()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            ReceiveAutoNumberConfig config = ReceiveAutoNumberGenerator.GetReceiveAutoNumberConfig(cp.BranchId);
            return Json(config, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateAutoNumberInvoice(int mode, string prefix)
        {
            try
            {
                if (mode < 0 || mode > 1)
                    throw new Exception("Mode tidak ditemukan");
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                ReceiveAutoNumberGenerator.SetupReceiveAutoMumber(mode, prefix, cp.BranchId);
                return Json(new { error = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetAutoNumberSuratPeringatan()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            SuratPeringatanAutoNumberConfig config = SuratPeringatanAutoNumberGenerator.GetSuratPeringatanAutoNumberConfig(cp.BranchId);
            return Json(config, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateAutoNumberSuratPeringatan(int mode, string prefix)
        {
            try
            {
                if (mode < 0 || mode > 1)
                    throw new Exception("Mode tidak ditemukan");
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                SuratPeringatanAutoNumberGenerator.SetupSuratPeringatanAutoMumber(mode, prefix, cp.BranchId);
                return Json(new { error = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetAutoNumberSuratPerjanjian()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            PerjanjianAutoNumberConfig config = PerjanjianAutoNumberGenerator.GetPerjanjianAutoNumberConfig(cp.BranchId);
            return Json(config, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateAutoNumberSuratPerjanjian(int mode, string prefix)
        {
            try
            {
                if (mode < 0 || mode > 1)
                    throw new Exception("Mode tidak ditemukan");
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                PerjanjianAutoNumberGenerator.SetupPerjanjianAutoMumber(mode, prefix, cp.BranchId);
                return Json(new { error = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #region property

        private ISIAutoNumberGenerator SIAutoNumberGenerator
        {
            get
            {
                if (_siGenerator == null)
                    _siGenerator = new SIAutoNumberGenerator();
                return _siGenerator;
            }
        }
        private IReceiveAutoNumberGenerator ReceiveAutoNumberGenerator
        {
            get
            {
                if (_rcvGenerator == null)
                    _rcvGenerator = ContextRegistry.GetContext().GetObject("ReceiveAutoNumberGenerator") as IReceiveAutoNumberGenerator;
                return _rcvGenerator;
            }
        }
        private ISuratPeringatanAutoNumberGenerator SuratPeringatanAutoNumberGenerator
        {
            get
            {
                if (_suratPeringatanGenerator == null)
                    _suratPeringatanGenerator = new SuratPeringatanAutoNumberGenerator();
                return _suratPeringatanGenerator;
            }
        }
        private IPerjanjianAutoNumberGenerator PerjanjianAutoNumberGenerator
        {
            get
            {
                if (_perjanjianGenerator == null)
                    _perjanjianGenerator = ContextRegistry.GetContext().GetObject("PerjanjianAutoNumberGenerator") as IPerjanjianAutoNumberGenerator;
                return _perjanjianGenerator;
            }
        }
        #endregion
    }
}
