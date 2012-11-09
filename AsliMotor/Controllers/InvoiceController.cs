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
        public ActionResult Detail(Guid id)
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
        [HttpGet]
        public JsonResult HeaderReport(Guid invId)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            InvoiceHeaderReport headerReport = InvoiceReportRepository.GetHeader(invId, cp.BranchId);
            return Json(headerReport, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ItemsReport(Guid invId)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            IList<InvoiceItemReport> items = InvoiceReportRepository.GetItems(invId, cp.BranchId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult TotalList()
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            TotalInvoice result = InvoiceReportRepository.GetTotalListView(cp.BranchId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Booking(BookingCommand cmd)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                cmd.id = Guid.NewGuid();
                cmd.BranchId = cp.BranchId;
                InvoiceService.Booking(cmd, cp.UserName);
                return Json(new { error = false, data = cmd }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Credit(CreditCommand cmd)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                cmd.id = Guid.NewGuid();
                cmd.BranchId = cp.BranchId;
                InvoiceService.Credit(cmd, cp.UserName);
                return Json(new { error = false, data = cmd }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Cash(CashCommand cmd)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                cmd.id = Guid.NewGuid();
                cmd.BranchId = cp.BranchId;
                InvoiceService.Cash(cmd, cp.UserName);
                return Json(new { error = false, data = cmd }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UpdateToCash(UpdateToCashCommand cmd)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                cmd.BranchId = cp.BranchId;
                InvoiceService.UpdateToCash(cmd, cp.UserName);
                return Json(new { error = false, data = cmd }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UpdateToCredit(UpdateToCreditCommand cmd)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                cmd.BranchId = cp.BranchId;
                InvoiceService.UpdateToCredit(cmd, cp.UserName);
                return Json(new { error = false, data = cmd }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult ChangeUangMuka(Guid invoiceId, decimal uangmuka)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                InvoiceService.ChangeUangMuka(invoiceId, uangmuka, cp.UserName);
                return Json(new { error = false, data = new { id = invoiceId } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult BayarAngsuran(Guid invoiceId, DateTime date)
        {
            try
            {
                CompanyProfile cp = new CompanyProfile(this.HttpContext);
                InvoiceService.BayarAngsuran(invoiceId, date, cp.UserName);
                return Json(new { error = false, data = new { InvoiceId = invoiceId } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public FileStreamResult PrintSuratPernyataanKredit(Guid id)
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
        [HttpGet]
        public FileStreamResult PrintKwitansiTandaJadi(Guid id)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            string template = PrintDocument.PrintKwitansiTandaJadi(id, cp.BranchId);
            EO.Pdf.Runtime.AddLicense(System.Configuration.ConfigurationManager.AppSettings["EOPdfLicense"]);
            EO.Pdf.HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
            EO.Pdf.HtmlToPdf.Options.OutputArea = new System.Drawing.RectangleF(0.5f, 0.1f, 7.3f, 12.1f);
            MemoryStream memStream = new MemoryStream();
            HtmlToPdf.ConvertHtml(template, memStream);
            MemoryStream resultStream = new MemoryStream(memStream.GetBuffer());
            return new FileStreamResult(resultStream, "application/pdf");
        }
        [HttpGet]
        public FileStreamResult PrintKwitansiKontan(Guid id)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            string template = PrintDocument.PrintKwitansiKontan(id, cp.BranchId);
            EO.Pdf.Runtime.AddLicense(System.Configuration.ConfigurationManager.AppSettings["EOPdfLicense"]);
            EO.Pdf.HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
            EO.Pdf.HtmlToPdf.Options.OutputArea = new System.Drawing.RectangleF(0.5f, 0.1f, 7.3f, 12.1f);
            MemoryStream memStream = new MemoryStream();
            HtmlToPdf.ConvertHtml(template, memStream);
            MemoryStream resultStream = new MemoryStream(memStream.GetBuffer());
            return new FileStreamResult(resultStream, "application/pdf");
        }
        [HttpGet]
        public FileStreamResult PrintKwitansiUangMuka(Guid id)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            string template = PrintDocument.PrintKwitansiUangMuka(id, cp.BranchId);
            EO.Pdf.Runtime.AddLicense(System.Configuration.ConfigurationManager.AppSettings["EOPdfLicense"]);
            EO.Pdf.HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
            EO.Pdf.HtmlToPdf.Options.OutputArea = new System.Drawing.RectangleF(0.5f, 0.1f, 7.3f, 12.1f);
            MemoryStream memStream = new MemoryStream();
            HtmlToPdf.ConvertHtml(template, memStream);
            MemoryStream resultStream = new MemoryStream(memStream.GetBuffer());
            return new FileStreamResult(resultStream, "application/pdf");
        }
        [HttpGet]
        public FileStreamResult PrintKwitansiAngsuranBulanan(Guid id)
        {
            CompanyProfile cp = new CompanyProfile(this.HttpContext);
            string template = PrintDocument.PrintKwitansiAngsuranBulanan(id, cp.BranchId);
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
                    _printDocument = ContextRegistry.GetContext().GetObject("PrintDocument") as IPrintDocument;
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
