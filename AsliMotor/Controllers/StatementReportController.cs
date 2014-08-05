using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Reporting;
using Spring.Context.Support;
using AsliMotor.Models;

namespace AsliMotor.Controllers
{
    public class StatementReportController : Controller
    {
        IStatementRepository _statementRepo;
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult TransactionListing(string fromDate, string toDate)
        {
            DateTime from = new DateTime(int.Parse(fromDate.Split('-')[2]), int.Parse(fromDate.Split('-')[1]), int.Parse(fromDate.Split('-')[0]));
            DateTime to = new DateTime(int.Parse(toDate.Split('-')[2]), int.Parse(toDate.Split('-')[1]), int.Parse(toDate.Split('-')[0]));
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<TransactionListing> transactions = StatementRepository.GetStatement(cp.BranchId, from, to, "Kas");
            return Json(transactions, JsonRequestBehavior.AllowGet);
        }
        private IStatementRepository StatementRepository
        {
            get
            {
                if (_statementRepo == null)
                    _statementRepo = ContextRegistry.GetContext().GetObject("StatementRepository") as IStatementRepository;
                return _statementRepo;
            }
        }
    }
}
