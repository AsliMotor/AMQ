﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.Models;
using AsliMotor.Products;
using Spring.Context.Support;
using AsliMotor.Products.Models;
using AsliMotor.Helper;
using AsliMotor.Security.Models;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        IProductRepository _repository;
        IProductService _service;
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            return View("index");
        }
        [HttpGet]
        public ActionResult Detail(Guid id)
        {
            return View("index");
        }
        [HttpGet]
        public JsonResult Product(Guid id)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            Product si = ProductRepository.GetProductById(id, cp.BranchId);
            return Json(si, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Lists(int offset, string status, bool search, string key)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<ProductReport> listView = new List<ProductReport>();
            if (search)
                listView = ProductRepository.SearchListView(cp.BranchId, offset, key);
            else
                listView = ProductRepository.GetListView(cp.BranchId, offset, status);

            return Json(listView, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult TotalList(string status)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            TotalProduct total = ProductRepository.GetTotalList(cp.BranchId, status);
            return Json(total, JsonRequestBehavior.AllowGet);
        }

        [MyAuthorize(Roles = RoleName.OWNER_ADMINPURCHASE)]
        [HttpPut]
        public JsonResult Product(Product product)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                ProductService.Update(product, cp.UserName);
                return Json(new { error = false, data = product }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Search(string key)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<ProductSearch> results = ProductRepository.Search(cp.BranchId, key);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        #region Private
        private IProductRepository ProductRepository
        {
            get
            {
                if (_repository == null)
                    _repository = ContextRegistry.GetContext().GetObject("ProductRepository") as IProductRepository;
                return _repository;
            }
        }
        private IProductService ProductService
        {
            get
            {
                if (_service == null)
                    _service = ContextRegistry.GetContext().GetObject("ProductService") as IProductService;
                return _service;
            }
        }
        #endregion
    }
}
