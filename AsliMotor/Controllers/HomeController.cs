using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Models;
using AsliMotor.SalesReports.ReportRepository;
using Spring.Context.Support;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IDashboardRepository _repo;
        ISalesReportRepository _salesReportRepo;
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult TotalTransaction()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            return Json(DashboardRepository.GetTotalTransaction(cp.BranchId), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DailySalesReport()
        {
            DateTime now = DateTime.Now;
            DateTime to = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime from = now.AddDays(-30);
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<DailySalesReport> results = SalesReportRepository.FindDailySalesReport(cp.BranchId, from, to);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SalesReport()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            return Json(DashboardRepository.GetSalesReport(cp.BranchId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PiutangTelahJatuhTempo(int offset)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            return Json(DashboardRepository.GetPiutangTelahJatuhTempo(cp.BranchId, offset), JsonRequestBehavior.AllowGet);
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
        private IDashboardRepository DashboardRepository
        {
            get {
                if (_repo == null)
                    _repo = ContextRegistry.GetContext().GetObject("DashboardRepository") as IDashboardRepository;
                return _repo;
            }
        }

        #endregion
    }
}
