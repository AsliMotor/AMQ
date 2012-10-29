using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsliMotor.PrintDocuments;
using AsliMotor.Models;
using System.IO;
using EO.Pdf;
using AsliMotor.Invoices.ReportRepository;
using Spring.Context.Support;
using AsliMotor.Invoices.Services;
using AsliMotor.Invoices.Command;

namespace AsliMotor.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        IPrintDocument _printDocument;
        IInvoiceReportRepository _invoiceReportRepository;
        IInvoiceService _invoiceService;
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
        public JsonResult Lists(int offset)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<InvoiceListViewReport> listView = InvoiceReportRepository.GetListViewReport(cp.BranchId, offset);
            return Json(listView, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Booking(BookingCommand cmd)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            cmd.id = Guid.NewGuid();
            cmd.BranchId = cp.BranchId;
            InvoiceService.Booking(cmd, cp.UserName);
            return Json(cmd, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Credit(CreditCommand cmd)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            cmd.id = Guid.NewGuid();
            cmd.BranchId = cp.BranchId;
            InvoiceService.Credit(cmd, cp.UserName);
            return Json(cmd, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Cash(CashCommand cmd)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            cmd.id = Guid.NewGuid();
            cmd.BranchId = cp.BranchId;
            InvoiceService.Cash(cmd, cp.UserName);
            return Json(cmd, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public FileStreamResult Print(Guid id)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            string template = PrintDocument.PrintSuratPernyataanKredit(id, cp.BranchId);
            EO.Pdf.Runtime.AddLicense(System.Configuration.ConfigurationManager.AppSettings["EOPdfLicense"]);
            EO.Pdf.HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
            EO.Pdf.HtmlToPdf.Options.OutputArea = new System.Drawing.RectangleF(0.5f, 0.1f, 7.3f, 12.1f);
            MemoryStream memStream = new MemoryStream();
            HtmlToPdf.ConvertHtml(template, memStream);
            MemoryStream resultStream = new MemoryStream(memStream.GetBuffer());
            return new FileStreamResult(resultStream, "application/pdf");
        }
        private IPrintDocument PrintDocument
        {
            get
            {
                if (_printDocument == null)
                    _printDocument = new PrintDocument();
                return _printDocument;
            }
        }
        private IInvoiceReportRepository InvoiceReportRepository
        {
            get
            {
                if (_invoiceReportRepository == null)
                    _invoiceReportRepository = ContextRegistry.GetContext().GetObject("InvoiceReportRepository") as IInvoiceReportRepository;
                return _invoiceReportRepository;
            }
        }
        private IInvoiceService InvoiceService
        {
            get
            {
                if(_invoiceService == null)
                    _invoiceService = ContextRegistry.GetContext().GetObject("InvoiceService") as IInvoiceService;
                return _invoiceService;
            }
        }
    }
}
