using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;

namespace AsliMotor.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrintSI()
        {
            return View();
        }
        public ActionResult PrintSuratPernyataanKredit()
        {
            return View();
        }
        public ActionResult DetailPurchase()
        {
            return View();
        }
        public ActionResult DateField()
        {
            return View();
        }
        public ActionResult TextField()
        {
            return View();
        }
        public ActionResult TextArea()
        {
            return View();
        }
        public ActionResult FormPanel()
        {
            return View();
        }
        public ActionResult SearchField()
        {
            return View();
        }
        public ActionResult RadioButton()
        {
            return View();
        }
        public ActionResult DetailInvoice()
        {
            return View();
        }
        public ActionResult PrintTandaJadi()
        {
            return View();
        }
        public ActionResult ErrorAlert()
        {
            return View();
        }
        public ActionResult ModalDialog()
        {
            return View();
        }
        public ActionResult PrintSuratPerjanjian()
        {
            return View();
        }
        public ActionResult PrintSuratPernyataan()
        {
            return View();
        }
        public ActionResult PrintSuratPeringatan()
        {
            return View();
        }
        public ActionResult PrintSuratPernyattanMampu()
        {
            return View();
        }
        public ActionResult PrintSuratKuasa()
        {
            return View();
        }
        public ActionResult PrintJBAngsuran()
        {
            return View();
        }
        public ActionResult PrintJBFidusia()
        {
            return View();
        }
        public ActionResult PrintSuratTandaTerima()
        {
            return View();
        }
        public ActionResult WebCam()
        {
            return View();
        }
        public JsonResult UploadCustomerImage(string image)
        {
            image = image.Substring("data:image/png;base64,".Length);
            var buffer = Convert.FromBase64String(image);
            // TODO: I am saving the image on the hard disk but
            // you could do whatever processing you want with it
            System.IO.File.WriteAllBytes(Server.MapPath("~/capture.png"), buffer);
            return Json(new { success = true },JsonRequestBehavior.AllowGet);
        }
    }
}
