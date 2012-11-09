﻿using System;
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
        ICustomerService _custService;
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("index");
        }
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            return View("index");
        }
        [HttpGet]
        public JsonResult TotalList()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            TotalCustomer total = CustomerRepository.GetTotalList(cp.BranchId);
            return Json(total, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Lists(int offset)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<CustomerReport> listView = CustomerRepository.GetListView(cp.BranchId, offset);
            return Json(listView, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Customer(Guid id)
        {
            return Json(CustomerRepository.GetById(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Customer(Customer cust)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                cust.id = Guid.NewGuid();
                cust.BranchId = cp.BranchId;
                CustomerService.Create(cust);
                return Json(new { error = false, data = cust }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPut]
        public JsonResult UpdateCustomer(Customer cust)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                CustomerService.Update(cust);
                return Json(new { error = false, data = cust }, JsonRequestBehavior.AllowGet);
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
        private ICustomerService CustomerService
        {
            get
            {
                if(_custService == null)
                    _custService = ContextRegistry.GetContext().GetObject("CustomerService") as ICustomerService;
                return _custService;
            }
        }
    }
}
