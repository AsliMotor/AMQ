using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.SalesReports.ReportRepository;
using Spring.Context.Support;
using AsliMotor.Models;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class SalesReportController : Controller
    {
        ISalesReportRepository _salesReportRepo;
        [HttpGet]
        public JsonResult DailySalesReport(string fromDate, string toDate)
        {
            DateTime from = new DateTime(int.Parse(fromDate.Split('-')[2]), int.Parse(fromDate.Split('-')[1]), int.Parse(fromDate.Split('-')[0]));
            DateTime to = new DateTime(int.Parse(toDate.Split('-')[2]), int.Parse(toDate.Split('-')[1]), int.Parse(toDate.Split('-')[0]));
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<DailySalesReport> results = SalesReportRepository.FindDailySalesReport(cp.BranchId, from, to);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult MonthlySalesReport(int year)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<MonthlySalesReport> results = SalesReportRepository.FindMonthlySalesReport(cp.BranchId, year);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult YearlySalesReport(int fromYear, int toYear)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<YearlySalesReport> results = SalesReportRepository.FindYearlySalesReport(cp.BranchId, fromYear, toYear);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #region Private

        private ISalesReportRepository SalesReportRepository
        {
            get
            {
                if (_salesReportRepo == null)
                    _salesReportRepo = ContextRegistry.GetContext().GetObject("SalesReportRepository") as ISalesReportRepository;
                return _salesReportRepo;
            }
        }

        #endregion
    }
}
