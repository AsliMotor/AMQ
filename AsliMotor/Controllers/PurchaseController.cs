using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.PrintDocuments;
using AsliMotor.Models;
using System.IO;
using EO.Pdf;
using AsliMotor.SI.Repository;
using AsliMotor.SI.Services;
using Spring.Context.Support;
using AsliMotor.Helper;
using AsliMotor.Security.Models;

namespace AsliMotor.Controllers
{
    [MyAuthorize(Roles = RoleName.ADMINISTRATOR_OWNER_ADMINPURCHASE_CASHIER)]
    public class PurchaseController : Controller
    {
        IPrintDocument _printDocument;
        ISupplierInvoiceRepository _siRepo;
        ISupplierInvoiceService _siService;
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
        public ActionResult Create()
        {
            return View("index");
        }
        [HttpGet]
        public JsonResult Purchase(Guid id)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            SupplierInvoice si = SupplierInvoiceRepository.GetById(id, cp.BranchId);
            return Json(si, JsonRequestBehavior.AllowGet);
        }

        [MyAuthorize(Roles = RoleName.ADMINISTRATOR_OWNER_ADMINPURCHASE)]
        [HttpPut]
        public JsonResult Purchase(SupplierInvoice si)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                SupplierInvoiceService.Update(si, cp.UserName);
                return Json(new { error = false, data = si }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [MyAuthorize(Roles = RoleName.ADMINISTRATOR_OWNER_ADMINPURCHASE)]
        [HttpPost]
        public JsonResult CreatePurchase(SupplierInvoice si)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                si.BranchId = cp.BranchId;
                si.id = Guid.NewGuid();
                si.ProductId = Guid.NewGuid();
                SupplierInvoiceService.Create(si, cp.UserName);
                return Json(new { error = false, data = si }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult TotalList()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            TotalSupplierInvoice total = SupplierInvoiceRepository.GetTotalList(cp.BranchId);
            return Json(total, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Lists(int offset, bool search, string key)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<SupplierInvoiceReport> listView = new List<SupplierInvoiceReport>();
            if (search)
                listView = SupplierInvoiceRepository.SearchListView(cp.BranchId, offset, key);
            else
                listView = SupplierInvoiceRepository.GetListView(cp.BranchId, offset);

            return Json(listView, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileStreamResult Print(Guid id)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            string template = PrintDocument.PrintSI(id, cp.BranchId);
            EO.Pdf.Runtime.AddLicense(System.Configuration.ConfigurationManager.AppSettings["EOPdfLicense"]);
            EO.Pdf.HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
            EO.Pdf.HtmlToPdf.Options.OutputArea = new System.Drawing.RectangleF(0.5f, 0.1f, 7.3f, 9.1f);
            MemoryStream memStream = new MemoryStream();
            HtmlToPdf.ConvertHtml(template, memStream);
            MemoryStream resultStream = new MemoryStream(memStream.GetBuffer());
            return new FileStreamResult(resultStream, "application/pdf");
        }

        #region private
        private IPrintDocument PrintDocument
        {
            get
            {
                if (_printDocument == null)
                    _printDocument = new PrintDocument();
                return _printDocument;
            }
        }
        private ISupplierInvoiceRepository SupplierInvoiceRepository
        {
            get
            {
                if (_siRepo == null)
                    _siRepo = ContextRegistry.GetContext().GetObject("SupplierInvoiceRepository") as ISupplierInvoiceRepository;
                return _siRepo;
            }
        }
        private ISupplierInvoiceService SupplierInvoiceService
        {
            get
            {
                if (_siService == null)
                    _siService = ContextRegistry.GetContext().GetObject("SupplierInvoiceService") as ISupplierInvoiceService;
                return _siService;
            }
        }
        #endregion
    }
}
