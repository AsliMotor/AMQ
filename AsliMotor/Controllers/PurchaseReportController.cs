using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Security.Models;
using AsliMotor.PurchaseReports.ReportRepository;
using Spring.Context.Support;
using AsliMotor.Models;

namespace AsliMotor.Controllers
{
    [Authorize(Roles = RoleName.OWNER)]
    public class PurchaseReportController : Controller
    {
        IPurchaseReportRepository _purchaseRepository;
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DailyPurchaseReport(string fromDate, string toDate)
        {
            DateTime from = new DateTime(int.Parse(fromDate.Split('-')[2]), int.Parse(fromDate.Split('-')[1]), int.Parse(fromDate.Split('-')[0]));
            DateTime to = new DateTime(int.Parse(toDate.Split('-')[2]), int.Parse(toDate.Split('-')[1]), int.Parse(toDate.Split('-')[0]));
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<DailyPurchaseReport> results = PurchaseRepository.FindDailyPurchaseReport(cp.BranchId, from, to);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult MonthlyPurchaseReport(int year)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<MonthlyPurchaseReport> results = PurchaseRepository.FindMonthlyPurchaseReport(cp.BranchId, year);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult YearlyPurchaseReport(int fromYear, int toYear)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<YearlyPurchaseReport> results = PurchaseRepository.FindYearlyPurchaseReport(cp.BranchId, fromYear, toYear);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult RateProductPurchaseReport(string fromDate, string toDate, int periority)
        {
            DateTime from = new DateTime(int.Parse(fromDate.Split('-')[2]), int.Parse(fromDate.Split('-')[1]), int.Parse(fromDate.Split('-')[0]));
            DateTime to = new DateTime(int.Parse(toDate.Split('-')[2]), int.Parse(toDate.Split('-')[1]), int.Parse(toDate.Split('-')[0]));
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<RateProductPurchaseReport> results = PurchaseRepository.FindRateProductPurchaseReport(cp.BranchId, from, to, periority);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GrafikProductPurchaseReport(string fromDate, string toDate)
        {
            DateTime from = new DateTime(int.Parse(fromDate.Split('-')[2]), int.Parse(fromDate.Split('-')[1]), int.Parse(fromDate.Split('-')[0]));
            DateTime to = new DateTime(int.Parse(toDate.Split('-')[2]), int.Parse(toDate.Split('-')[1]), int.Parse(toDate.Split('-')[0]));
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<GrafikProductPurchaseReport> results = PurchaseRepository.FindGrafikProductPurchaseReport(cp.BranchId, from, to);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #region Private
        public IPurchaseReportRepository PurchaseRepository
        {
            get
            {
                if(_purchaseRepository == null)
                    _purchaseRepository = ContextRegistry.GetContext().GetObject("PurchaseReportRepository") as IPurchaseReportRepository;
                return _purchaseRepository;
            }
        }
        #endregion
    }
}
