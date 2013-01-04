using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Helper;
using AsliMotor.Products;
using Spring.Context.Support;
using AsliMotor.Products.Models;
using AsliMotor.Models;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class TypeProductController : Controller
    {
        IProductRepository _repository;
        [HttpGet]
        public JsonResult Search(string key)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<TypeProduct> result = ProductRepository.SearchType(key, cp.BranchId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<TypeProduct> result = ProductRepository.GetAllType(cp.BranchId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private IProductRepository ProductRepository
        {
            get
            {
                if (_repository == null)
                    _repository = ContextRegistry.GetContext().GetObject("ProductRepository") as IProductRepository;
                return _repository;
            }
        }
    }
}
