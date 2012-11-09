using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr3.ST;
using AsliMotor.Organizations;
using AsliMotor.SI.Repository;
using AsliMotor.Invoices.ReportRepository;

namespace AsliMotor.PrintDocuments
{
    public class PrintDocument : IPrintDocument
    {
        IOrganizationRepository _orgRepo;
        ISupplierInvoiceRepository _siRepo;
        public IInvoiceReportRepository InvoiceReportRepository { get; set; }
        public PrintDocument()
        {
            _orgRepo = new OrganizationRepository();
            _siRepo = new SupplierInvoiceRepository();
        }

        public string PrintSI(Guid siId, string branchId)
        {
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(SIStringTemplate.DEFAULT);
            SupplierInvoice si = _siRepo.GetById(siId, branchId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("Merk", (si.Merk == "") ? "-": si.Merk);
            template.SetAttribute("NoBpkb", (si.NoBpkb == "") ? "-": si.NoBpkb);
            template.SetAttribute("NoMesin", (si.NoMesin == "") ? "-": si.NoMesin);
            template.SetAttribute("NoPolisi", (si.NoPolisi == "") ? "-" : si.NoPolisi);
            template.SetAttribute("NoRangka", (si.NoRangka == "") ? "-" : si.NoRangka);
            template.SetAttribute("Note", (si.Note == "") ? "-" : si.Note);
            template.SetAttribute("Tahun", (si.Tahun == "") ? "-" : si.Tahun);
            template.SetAttribute("SupplierInvoiceNo", si.SupplierInvoiceNo);
            template.SetAttribute("Type", si.Type);
            template.SetAttribute("SIDate", si.SupplierInvoiceDate.ToString("dd MMMM yyyy"));
            template.SetAttribute("Total", si.HargaBeli.ToString("###,###,###,##0.#0"));
            template.SetAttribute("terbilang", SayNumber.Terbilang(si.HargaBeli) + "Rupiah");
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            return template.ToString();
        }

        public string PrintSuratPerjanjianJualBeli(Guid invId, string branchId)
        {
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(PerjanjianStringTemplate.DEFAULT);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            return template.ToString();
        }

        public string PrintSuratPernyataanKredit(Guid invId, string branchId)
        {
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(SuratPernyataanKreditTemplate.DEFAULT);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            return template.ToString();
        }

        public string PrintKwitansiTandaJadi(Guid invId, string branchId)
        {
            InvoiceBookingReport inv = InvoiceReportRepository.GetInvoiceBookingReport(invId, branchId);
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(KwitansiTandaJadiTemplate.DEFAULT);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute("transactionNo", inv.ReceiveNo);
            template.SetAttribute("custName", inv.CustomerName);
            template.SetAttribute("terbilang", SayNumber.Terbilang(inv.DebitNote) + " Rupiah");
            template.SetAttribute("total", inv.DebitNote.ToString("###,###,###,##0.#0"));
            template.SetAttribute("merk", inv.Merk);
            template.SetAttribute("type", inv.Type);
            template.SetAttribute("warna", inv.Warna);
            template.SetAttribute("norangka", inv.NoRangka);
            template.SetAttribute("nomesin", inv.NoMesin);
            template.SetAttribute("norangka", inv.NoRangka);
            template.SetAttribute("nopolisi", inv.NoPolisi);
            return template.ToString();
        }

        public string PrintKwitansiKontan(Guid invId, string branchId)
        {
            InvoiceCashReport inv = InvoiceReportRepository.GetInvoiceCashReport(invId, branchId);
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(KwitansiKontanTemplate.DEFAULT);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute("transactionNo", inv.ReceiveNo);
            template.SetAttribute("custName", inv.CustomerName);
            template.SetAttribute("terbilang", SayNumber.Terbilang(inv.Total) + " Rupiah");
            template.SetAttribute("total", inv.Total.ToString("###,###,###,##0.#0"));
            template.SetAttribute("merk", inv.Merk);
            template.SetAttribute("type", inv.Type);
            template.SetAttribute("warna", inv.Warna);
            template.SetAttribute("norangka", inv.NoRangka);
            template.SetAttribute("nomesin", inv.NoMesin);
            template.SetAttribute("norangka", inv.NoRangka);
            template.SetAttribute("nopolisi", inv.NoPolisi);
            return template.ToString();
        }

        public string PrintKwitansiUangMuka(Guid invId, string branchid)
        {
            ReceiveUangMukaReport inv = InvoiceReportRepository.GetReceiveUangMukaReport(invId, branchid);
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(KwitansiUangMukaTemplate.DEFAULT);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute("transactionNo", inv.ReceiveNo);
            template.SetAttribute("custName", inv.CustomerName);
            template.SetAttribute("terbilang", SayNumber.Terbilang(inv.Total) + " Rupiah");
            template.SetAttribute("total", inv.Total.ToString("###,###,###,##0.#0"));
            template.SetAttribute("merk", inv.Merk);
            template.SetAttribute("type", inv.Type);
            template.SetAttribute("warna", inv.Warna);
            template.SetAttribute("norangka", inv.NoRangka);
            template.SetAttribute("nomesin", inv.NoMesin);
            template.SetAttribute("norangka", inv.NoRangka);
            template.SetAttribute("nopolisi", inv.NoPolisi);
            template.SetAttribute("NoSuratPerjanjian", inv.NoSuratPerjanjian);
            template.SetAttribute("SuratPerjanjianDate", inv.SuratPerjanjianDate.ToString("dd MMMM yyyy"));
            template.SetAttribute("LamaAngsuran", inv.LamaAngsuran);
            template.SetAttribute("AngsuranBulanan", inv.AngsuranBulanan.ToString("###,###,###,##0.#0"));
            return template.ToString();
        }


        public string PrintKwitansiAngsuranBulanan(Guid rcvId, string branchid)
        {
            ReceiveAngsuranReport rcv = InvoiceReportRepository.GetReceiveAngsuranReport(rcvId);
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(KwitansiAngsuranTemplate.DEFAULT);
            string dendaTemplate = (rcv.Denda > 0) ? "<tr><td colspan='3'>Denda Sebesar <b>Rp. " + rcv.Denda.ToString("###,###,###,##0.#0") + "</b></td></tr>" : "";
            DateTime bulanAngsuran = new DateTime(Int32.Parse(rcv.Month.Substring(2, 4)), Int32.Parse(rcv.Month.Substring(0, 2)), 1);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute("terbilang", SayNumber.Terbilang(rcv.Total) + " Rupiah");
            template.SetAttribute("total", rcv.Total.ToString("###,###,###,##0.#0"));
            template.SetAttribute("SuratPerjanjianDate", rcv.SuratPerjanjianDate.ToString("dd MMMM yyyy"));
            template.SetAttribute("AngsuranBulanan", rcv.AngsuranBulanan.ToString("###,###,###,##0.#0"));
            template.SetAttribute("DendaTemplate", dendaTemplate);
            template.SetAttribute("BulanAngsuran", bulanAngsuran.ToString("MMMM yyyy"));
            template.SetAttribute("rcv", rcv);
            return template.ToString();
        }
    }
}
