using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.PaymentTerms;
using Spring.Context.Support;
using AsliMotor.Models;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class PaymentTermController : Controller
    {
        IPaymentTermRepository _repo;
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Terms()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<PaymentTermReport> result = PaymentTermRepository.FindAll(cp.OwnerId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Private
        private IPaymentTermRepository PaymentTermRepository
        {
            get
            {
                if(_repo == null)
                    _repo = ContextRegistry.GetContext().GetObject("PaymentTermRepository") as IPaymentTermRepository;
                return _repo;
            }
        }
        #endregion
    }
}
