using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Helper;
using AsliMotor.AuditLog.Repository;
using Spring.Context.Support;
using AsliMotor.Models;
using AsliMotor.Security.Models;

namespace AsliMotor.Controllers
{
    [Authorize(Roles=RoleName.OWNER_ADMINISTRATOR)]
    public class PriceChangeLogController : Controller
    {
        private IPriceChangedRepository _repo;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View("Index");
        }
        [HttpGet]
        public JsonResult GetListSILog(int offset)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<SupplierInvoicePriceChangedLog> result = PriceChangedRepository.GetListSIPriceChangedLog(cp.BranchId, offset);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTotalSILog()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            TotalSupplierInvoicePriceChangedLog result = PriceChangedRepository.GetTotalSIPriceChangeLog(cp.BranchId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetListInvoiceLog(int offset)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<InvoicePriceChangedLog> result = PriceChangedRepository.GetListInvoicePriceChangedLog(cp.BranchId, offset);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTotalInvoiceLog()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            TotalInvoicePriceChangedLog result = PriceChangedRepository.GetTotalInvoicePriceChangeLog(cp.BranchId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region private
        private IPriceChangedRepository PriceChangedRepository
        {
            get
            {
                if(_repo == null)
                    _repo = ContextRegistry.GetContext().GetObject("PriceChangedRepository") as IPriceChangedRepository;
                return _repo;
            }
        }
        #endregion
    }
}
