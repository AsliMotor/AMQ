using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Customers;
using Spring.Context.Support;
using AsliMotor.Models;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ICustomerRepository _custRepo;
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Search(string key)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<CustomerSearch> result = CustomerRepository.Search(key, cp.BranchId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private ICustomerRepository CustomerRepository
        {
            get
            {
                if (_custRepo == null)
                    _custRepo = ContextRegistry.GetContext().GetObject("CustomerRepository") as ICustomerRepository;
                return _custRepo;
            }
        }
    }
}
