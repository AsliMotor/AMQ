using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr3.ST;
using AsliMotor.Organizations;
using AsliMotor.SI.Repository;
using AsliMotor.Invoices.ReportRepository;
using AsliMotor.SuratPeringatan.AutoNumberGenerator;

namespace AsliMotor.PrintDocuments
{
    public class PrintDocument : IPrintDocument
    {
        IOrganizationRepository _orgRepo;
        ISupplierInvoiceRepository _siRepo;
        ISuratPeringatanAutoNumberGenerator _spGen;
        public IInvoiceReportRepository InvoiceReportRepository { get; set; }

        #region Property
        private const string TRANSACTIONNUMBER = "transactionno";
        private const string ORGANIZATIONNAME = "OrganizationName";
        private const string CUSTOMERNAME = "custname";
        private const string NOKTP = "noktp";
        private const string KTPDATE = "ktpdate";
        private const string BILLINGADDRESS = "billingaddress";
        private const string GENDER = "gender";
        private const string BIRTHDAY = "birthday";
        private const string CITY = "city";
        private const string JOB = "job";
        private const string MERK = "merk";
        private const string NoBpkb = "nobpkb";
        private const string NoMesin = "nomesin";
        private const string NoPolisi = "nopolisi";
        private const string NoRangka = "norangka";
        private const string Warna = "warna";
        private const string Note = "note";
        private const string Tahun = "tahun";
        private const string Type = "type";
        private const string Total = "total";
        private const string Terbilang = "terbilang";
        private const string CurrentDate = "currentdate";
        private const string SURATPERJANJIANNO = "suratperjanjianno";
        private const string SURATPERJANJIANDATE = "suratperjanjiandate";
        #endregion

        public PrintDocument()
        {
            _orgRepo = new OrganizationRepository();
            _siRepo = new SupplierInvoiceRepository();
            _spGen = new SuratPeringatanAutoNumberGenerator();
        }

        public string PrintSI(Guid siId, string branchId)
        {
            Organization org = _orgRepo.GetOrganization(branchId);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchId);
            StringTemplate template = new StringTemplate(SIStringTemplate.DEFAULT);
            SupplierInvoice si = _siRepo.GetById(siId, branchId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute(ORGANIZATIONNAME, org.OrganizationName);
            template.SetAttribute(MERK, si.Merk.parseString());
            template.SetAttribute(NoBpkb, si.NoBpkb.parseString());
            template.SetAttribute(NoMesin, (si.NoMesin == "") ? "-": si.NoMesin);
            template.SetAttribute(NoPolisi, (si.NoPolisi == "") ? "-" : si.NoPolisi);
            template.SetAttribute(NoRangka, (si.NoRangka == "") ? "-" : si.NoRangka);
            template.SetAttribute(Note, (si.Note == "") ? "-" : si.Note);
            template.SetAttribute(Tahun, (si.Tahun == "") ? "-" : si.Tahun);
            template.SetAttribute("SupplierInvoiceNo", si.SupplierInvoiceNo);
            template.SetAttribute(Type, si.Type);
            template.SetAttribute("SIDate", si.SupplierInvoiceDate.ToString("dd MMMM yyyy"));
            template.SetAttribute(Total, si.HargaBeli.ToString("###,###,###,##0.#0"));
            template.SetAttribute(Terbilang, SayNumber.Terbilang(si.HargaBeli) + "Rupiah");
            template.SetAttribute(CurrentDate, DateTime.Now.ToString("dd MMMM yyyy"));
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
            PernyataanKreditReport report = InvoiceReportRepository.GetPernyataanKredit(invId);
            decimal uangMuka = report.UangMuka + report.DebitNote;
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute(CurrentDate, DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute(CUSTOMERNAME, report.CustomerName.parseString());
            template.SetAttribute(BILLINGADDRESS, report.BillingAddress.parseString());
            template.SetAttribute(NOKTP, report.NoKtp.parseString());
            template.SetAttribute(KTPDATE, report.KtpDate.parseStringDate());
            template.SetAttribute(CITY, report.City.parseString());
            template.SetAttribute(JOB, report.Job.parseString());
            template.SetAttribute("angsuranbulanan", report.AngsuranBulanan.parsePrice());
            template.SetAttribute("angsuranbulananterbilang", report.AngsuranBulanan.parseTerbilang());
            template.SetAttribute("lamaangsuran", report.LamaAngsuran);
            template.SetAttribute("lamaangsuranterbilang", SayNumber.Terbilang(decimal.Parse(report.LamaAngsuran.ToString())));
            template.SetAttribute("uangmuka", uangMuka.parsePrice());
            template.SetAttribute("uangmukaterbilang", uangMuka.parseTerbilang());
            template.SetAttribute("banyakcicilan", report.BanyakCicilan);
            template.SetAttribute(MERK, report.Merk.parseString());
            template.SetAttribute(Type, report.Type.parseString());
            template.SetAttribute(Tahun, report.Tahun.parseString());
            template.SetAttribute(Warna, report.Warna.parseString());
            template.SetAttribute(NoMesin, report.NoMesin.parseString());
            template.SetAttribute(NoPolisi, report.NoPolisi.parseString());
            template.SetAttribute(NoRangka, report.NoRangka.parseString());
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
            template.SetAttribute("merk", inv.Merk.parseString());
            template.SetAttribute("type", inv.Type.parseString());
            template.SetAttribute("warna", inv.Warna.parseString());
            template.SetAttribute("nomesin", inv.NoMesin.parseString());
            template.SetAttribute("norangka", inv.NoRangka.parseString());
            template.SetAttribute("nopolisi", inv.NoPolisi.parseString());
            template.SetAttribute("NoSuratPerjanjian", inv.NoSuratPerjanjian.parseString());
            template.SetAttribute("SuratPerjanjianDate", inv.SuratPerjanjianDate.ToString("dd MMMM yyyy"));
            template.SetAttribute("LamaAngsuran", inv.LamaAngsuran);
            template.SetAttribute("AngsuranBulanan", inv.AngsuranBulanan.ToString("###,###,###,##0.#0"));
            return template.ToString();
        }

        public string PrintKwitansiPelunasan(Guid invId, string branchid)
        {
            ReceivePelunasanReport inv = InvoiceReportRepository.GetReceivePelunasanReport(invId, branchid);
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(KwitansiPelunasanTemplate.DEFAULT);
            decimal sisatagihan = (inv.Total + inv.Discount) - inv.Denda;
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute(TRANSACTIONNUMBER, inv.ReceiveNo);
            template.SetAttribute(CUSTOMERNAME, inv.CustomerName);
            template.SetAttribute(Terbilang, SayNumber.Terbilang(inv.Total) + " Rupiah");
            template.SetAttribute(Total, inv.Total.parsePrice());
            template.SetAttribute(MERK, inv.Merk.parseString());
            template.SetAttribute("type", inv.Type.parseString());
            template.SetAttribute(Warna, inv.Warna.parseString());
            template.SetAttribute(NoMesin, inv.NoMesin.parseString());
            template.SetAttribute(NoRangka, inv.NoRangka.parseString());
            template.SetAttribute(NoPolisi, inv.NoPolisi.parseString());
            template.SetAttribute("banyakcicilan", inv.BanyakCicilan);
            template.SetAttribute("cicilanyangtelahdibayar", inv.BanyakCicilanTerbayar);
            template.SetAttribute("angsuranbulanan", inv.AngsuranBulanan.parsePrice());
            template.SetAttribute("sisatagihan", sisatagihan.parsePrice());
            if(inv.Denda > 0){
                template.SetAttribute("totaldenda", "<tr><td>Total Denda</td><td>:</td><td>Rp " + inv.Denda.parsePrice() + "</td></tr>");
                template.SetAttribute("sisatagihanplusdenda", "<tr><td>Sisa tagihan + denda</td><td>:</td><td>Rp " + (inv.Total + inv.Discount).parsePrice() + "</td></tr>");
            }
            template.SetAttribute("discount", inv.Discount.parsePrice());
            return template.ToString();
        }

        public string PrintKwitansiAngsuranBulanan(Guid rcvId, string branchid)
        {
            ReceiveAngsuranReport rcv = InvoiceReportRepository.GetReceiveAngsuranReport(rcvId);
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(KwitansiAngsuranTemplate.DEFAULT);
            string dendaTemplate = (rcv.Denda > 0) ? "<tr><td colspan='3'>Denda Sebesar <b>Rp. " + rcv.Denda.ToString("###,###,###,##0.#0") + "</b></td></tr>" : "";
            string depositTemplate = "";
            if (rcv.Deposit > 0)
            {
                depositTemplate = "<tr><td colspan='3'>Sisa uang yang dibayar sebesar <b>Rp. " + rcv.Deposit.ToString("###,###,###,##0.#0") + "</b> akan dimasukan ke deposit "+ org.OrganizationName +"</td></tr>";
            }
            else if (rcv.Deposit < 0)
            {
                depositTemplate = "<tr><td colspan='3'>Sisa kekurangan uang yang dibayar sebesar <b>Rp. " + (rcv.Deposit * -1).ToString("###,###,###,##0.#0") + "</b> akan ditutupi dari deposit pelanggan "+ rcv.CustomerName +" yang ada di " + org.OrganizationName + "</td></tr>";
            }
            //DateTime bulanAngsuran = new DateTime(Int32.Parse(rcv.Month.Substring(2, 4)), Int32.Parse(rcv.Month.Substring(0, 2)), 1);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute("currentDate", DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute("terbilang", SayNumber.Terbilang(rcv.Total) + " Rupiah");
            template.SetAttribute("total", rcv.Total.ToString("###,###,###,##0.#0"));
            template.SetAttribute("SuratPerjanjianDate", rcv.SuratPerjanjianDate.ToString("dd MMMM yyyy"));
            template.SetAttribute("AngsuranBulanan", rcv.AngsuranBulanan.ToString("###,###,###,##0.#0"));
            template.SetAttribute("DendaTemplate", dendaTemplate);
            template.SetAttribute("Deposit", depositTemplate);
            //template.SetAttribute("BulanAngsuran", bulanAngsuran.ToString("MMMM yyyy"));
            template.SetAttribute("BulanAngsuran", rcv.MonthNumber);
            template.SetAttribute("BulanAngsuranFormated", rcv.MonthFormated);
            template.SetAttribute("rcv", rcv);
            return template.ToString();
        }

        public string PrintSuratPeringatan(Guid invId, DateTime date, string branchid)
        {
            SuratPeringatanReport spReport = InvoiceReportRepository.GetSuratPeringatanReport(invId, branchid);
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            string spNo = _spGen.GenerateSuratPeringatanNumber(DateTime.Now, branchid);
            StringTemplate template = new StringTemplate(SuratPeringatanTemplate.DEFAULT);
            IList<SuratPeringatanItem> items = new List<SuratPeringatanItem>();
            DateTime currDate = (DateTime.Now > spReport.StartDueDate.AddMonths(spReport.LamaAngsuran)) ? spReport.StartDueDate.AddMonths(spReport.LamaAngsuran) : DateTime.Now;
            for (int i = 0; i < spReport.DiffrentMonth; i++)
            {
                DateTime dueDate = spReport.DueDate.AddMonths(i);
                if (dueDate >= currDate) break;
                if ((i + 1) + spReport.AngsuranKe > spReport.LamaAngsuran) break;
                TimeSpan ts = new TimeSpan();
                ts = DateTime.Now.Subtract(dueDate);
                decimal denda = (spReport.AngsuranBulanan * decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["denda"])) * ts.Days;
                
                items.Add(new SuratPeringatanItem
                {
                    No = i +1,
                    AngsuranKe = (i + 1)+spReport.AngsuranKe,
                    DueDate = spReport.DueDate.AddMonths(i).ToString("dd MMMM yyyy"),
                    StringDenda = denda.ToString("###,###,###,##0.#0"),
                    Denda = denda,
                    StringAngsuran = spReport.AngsuranBulanan.ToString("###,###,###,##0.#0"),
                    Angsuran = spReport.AngsuranBulanan,
                    Total = spReport.AngsuranBulanan + denda,
                    StringTotal = (spReport.AngsuranBulanan + denda).ToString("###,###,###,##0.#0"),
                });
            }

            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("SuratPeringatanNo", spNo);
            template.SetAttribute("SuratPeringatanDate", date.ToString("dd MMMM yyyy"));
            template.SetAttribute("data", spReport);
            template.SetAttribute("currentDate", date.ToString("dd MMMM yyyy"));
            template.SetAttribute("SuratPerjanjianDate", spReport.SuratPerjanjianDate.ToString("dd MMMM yyyy"));
            template.SetAttribute("Items", items);
            template.SetAttribute("Warna", spReport.Warna == string.Empty ? "-": spReport.Warna);
            template.SetAttribute("NoRangka", spReport.NoRangka == string.Empty ? "-" : spReport.NoRangka);
            template.SetAttribute("NoMesin", spReport.NoMesin == string.Empty ? "-" : spReport.NoMesin);
            template.SetAttribute("NetTotal", items.Sum(i => i.Total).ToString("###,###,###,##0.#0"));
            return template.ToString();
        }

        public string PrintSuratPernyataan(Guid invId, string branchid)
        {
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(SuratPernyataanTemplate.DEFAULT);
            SuratPernyataanReport report = InvoiceReportRepository.GetSuratPernyataan(invId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute(CurrentDate, DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute(CUSTOMERNAME, report.CustomerName.parseString());
            template.SetAttribute(BILLINGADDRESS, report.BillingAddress.parseString());
            template.SetAttribute(CITY, report.City.parseString());
            template.SetAttribute(MERK, report.Merk.parseString());
            template.SetAttribute(Type, report.Type.parseString());
            template.SetAttribute(NoMesin, report.NoMesin.parseString());
            template.SetAttribute(NoPolisi, report.NoPolisi.parseString());
            template.SetAttribute(NoRangka, report.NoRangka.parseString());
            template.SetAttribute(GENDER, report.Gender.parseString());
            template.SetAttribute(BIRTHDAY, report.Birthday.parseStringDate());
            template.SetAttribute(SURATPERJANJIANNO, report.NoSuratPerjanjian.parseString());
            template.SetAttribute(SURATPERJANJIANDATE, report.SuratPerjanjianDate.parseStringDate());
            return template.ToString();
        }

        public string PrintSuratPernyataanMampu(Guid invId, string branchid)
        {
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(SuratPernyataanMampuTemplate.DEFAULT);
            SuratPernyataanMampu report = InvoiceReportRepository.GetSuratPernyataanMampu(invId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute(CurrentDate, DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute(CUSTOMERNAME, report.CustomerName.parseString());
            template.SetAttribute(BILLINGADDRESS, report.BillingAddress.parseString());
            template.SetAttribute(JOB, report.Job.parseString());
            template.SetAttribute(CITY, report.City.parseString());
            template.SetAttribute(MERK, report.Merk.parseString());
            template.SetAttribute(Type, report.Type.parseString());
            template.SetAttribute(NoMesin, report.NoMesin.parseString());
            template.SetAttribute(NoPolisi, report.NoPolisi.parseString());
            template.SetAttribute(NoRangka, report.NoRangka.parseString());
            template.SetAttribute(GENDER, report.Gender.parseString());
            template.SetAttribute(BIRTHDAY, report.Birthday.parseStringDate());
            template.SetAttribute(Warna, report.Warna.parseString());
            template.SetAttribute(Tahun, report.Tahun.parseString());
            return template.ToString();
        }

        public string PrintSuratKuasa(Guid invId, string branchid)
        {
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(SuratKuasaTemplate.DEFAULT);
            SuratKuasaReport report = InvoiceReportRepository.GetSuratKuasa(invId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute(CurrentDate, DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute(CUSTOMERNAME, report.CustomerName.parseString());
            template.SetAttribute(BILLINGADDRESS, report.BillingAddress.parseString());
            template.SetAttribute(JOB, report.Job.parseString());
            template.SetAttribute(CITY, report.City.parseString());
            template.SetAttribute(MERK, report.Merk.parseString());
            template.SetAttribute(Type, report.Type.parseString());
            template.SetAttribute(NoMesin, report.NoMesin.parseString());
            template.SetAttribute(NoPolisi, report.NoPolisi.parseString());
            template.SetAttribute(NoRangka, report.NoRangka.parseString());
            template.SetAttribute(GENDER, report.Gender.parseString());
            template.SetAttribute(BIRTHDAY, report.Birthday.parseStringDate());
            template.SetAttribute(Warna, report.Warna.parseString());
            template.SetAttribute(Tahun, report.Tahun.parseString());
            template.SetAttribute(NOKTP, report.NoKtp.parseString());
            template.SetAttribute(KTPDATE, report.KtpDate.parseStringDate());
            template.SetAttribute(SURATPERJANJIANNO, report.NoSuratPerjanjian.parseString());
            template.SetAttribute(SURATPERJANJIANDATE, report.SuratPerjanjianDate.parseStringDate());
            template.SetAttribute("ktppublisher", report.KtpPublisher.parseString());
            template.SetAttribute("ddddmmmmyyyy", DateTime.Now.ToString("dddd dd MMMM yyyy"));
            return template.ToString();
        }

        public string PrintJBAngsuran(Guid invId, string branchid)
        {
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(JSAngsuranTemplate.DEFAULT);
            JBAngsuranReport report = InvoiceReportRepository.GetJBAngsuran(invId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute(CurrentDate, DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute(CUSTOMERNAME, report.CustomerName.parseString());
            template.SetAttribute(BILLINGADDRESS, report.BillingAddress.parseString());
            template.SetAttribute(JOB, report.Job.parseString());
            template.SetAttribute(CITY, report.City.parseString());
            template.SetAttribute(MERK, report.Merk.parseString());
            template.SetAttribute(Type, report.Type.parseString());
            template.SetAttribute(NoMesin, report.NoMesin.parseString());
            template.SetAttribute(NoPolisi, report.NoPolisi.parseString());
            template.SetAttribute(NoRangka, report.NoRangka.parseString());
            template.SetAttribute(GENDER, report.Gender.parseString());
            template.SetAttribute(BIRTHDAY, report.Birthday.parseStringDate());
            template.SetAttribute(Warna, report.Warna.parseString());
            template.SetAttribute(Tahun, report.Tahun.parseString());
            template.SetAttribute(NOKTP, report.NoKtp.parseString());
            template.SetAttribute(KTPDATE, report.KtpDate.parseStringDate());
            template.SetAttribute(SURATPERJANJIANNO, report.NoSuratPerjanjian.parseString());
            template.SetAttribute(SURATPERJANJIANDATE, report.SuratPerjanjianDate.parseStringDate());
            template.SetAttribute("ktppublisher", report.KtpPublisher.parseString());
            template.SetAttribute("price", report.Price.parsePrice());
            template.SetAttribute("uangmuka", (report.UangMuka + report.Booking).parsePrice());
            template.SetAttribute("sisahutang", (report.Price - (report.UangMuka + report.Booking)).parsePrice());
            template.SetAttribute("sisahutangterbilang", (report.Price - (report.UangMuka + report.Booking)).parseTerbilang());
            template.SetAttribute("lamaangsuran", report.LamaAngsuran);
            template.SetAttribute("lamaangsuranterbilang", SayNumber.Terbilang(report.LamaAngsuran));
            template.SetAttribute("angsuranbulanan", report.AngsuranBulanan.parsePrice());
            template.SetAttribute("angsuranbulananterbilang", report.AngsuranBulanan.parseTerbilang());
            template.SetAttribute("banyakcicilan", report.BanyakCicilan);
            return template.ToString();
        }

        public string PrintJBFidusia(Guid invId, string branchid)
        {
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(JBFidusiaTemplate.DEFAULT);
            JBFidusiaReport report = InvoiceReportRepository.GetJBFidusia(invId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute(CurrentDate, DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute(CUSTOMERNAME, report.CustomerName.parseString());
            template.SetAttribute(BILLINGADDRESS, report.BillingAddress.parseString());
            template.SetAttribute(JOB, report.Job.parseString());
            template.SetAttribute(CITY, report.City.parseString());
            template.SetAttribute(MERK, report.Merk.parseString());
            template.SetAttribute(Type, report.Type.parseString());
            template.SetAttribute(NoMesin, report.NoMesin.parseString());
            template.SetAttribute(NoPolisi, report.NoPolisi.parseString());
            template.SetAttribute(NoRangka, report.NoRangka.parseString());
            template.SetAttribute(GENDER, report.Gender.parseString());
            template.SetAttribute(BIRTHDAY, report.Birthday.parseStringDate());
            template.SetAttribute(Warna, report.Warna.parseString());
            template.SetAttribute(Tahun, report.Tahun.parseString());
            template.SetAttribute(NOKTP, report.NoKtp.parseString());
            template.SetAttribute(KTPDATE, report.KtpDate.parseStringDate());
            template.SetAttribute(SURATPERJANJIANNO, report.NoSuratPerjanjian.parseString());
            template.SetAttribute(SURATPERJANJIANDATE, report.SuratPerjanjianDate.parseStringDate());
            template.SetAttribute("ktppublisher", report.KtpPublisher.parseString());
            template.SetAttribute("sisahutang", (report.Price - (report.UangMuka + report.Booking)).parsePrice());
            template.SetAttribute("sisahutangterbilang", (report.Price - (report.UangMuka + report.Booking)).parseTerbilang());
            template.SetAttribute("lamaangsuran", report.LamaAngsuran);
            template.SetAttribute("banyakcicilan", report.BanyakCicilan);
            template.SetAttribute("lamaangsuranterbilang", SayNumber.Terbilang(report.LamaAngsuran));
            return template.ToString();
        }

        public string PrintTandaTerima(Guid invId, string branchid)
        {
            Organization org = _orgRepo.GetOrganization(branchid);
            LogoOrganization logoOrg = _orgRepo.GetLogoOrganization(branchid);
            StringTemplate template = new StringTemplate(TandaTerimaTemplate.DEFAULT);
            JBFidusiaReport report = InvoiceReportRepository.GetJBFidusia(invId);
            template.SetAttribute("organization", org);
            template.SetAttribute("logodata", Convert.ToBase64String(logoOrg.Image));
            template.SetAttribute("OrganizationName", org.OrganizationName);
            template.SetAttribute(CurrentDate, DateTime.Now.ToString("dd MMMM yyyy"));
            template.SetAttribute(CUSTOMERNAME, report.CustomerName.parseString());
            template.SetAttribute(BILLINGADDRESS, report.BillingAddress.parseString());
            template.SetAttribute(JOB, report.Job.parseString());
            template.SetAttribute(MERK, report.Merk.parseString());
            template.SetAttribute(Type, report.Type.parseString());
            template.SetAttribute(NoMesin, report.NoMesin.parseString());
            template.SetAttribute(NoPolisi, report.NoPolisi.parseString());
            template.SetAttribute(NoRangka, report.NoRangka.parseString());
            template.SetAttribute(Warna, report.Warna.parseString());
            template.SetAttribute(Tahun, report.Tahun.parseString());
            template.SetAttribute(NOKTP, report.NoKtp.parseString());
            return template.ToString();
        }
    }

    public static class ObjectExtension
    {
        public static string parseString(this string text)
        {
            return (text == "") ? "-" : text;
        }
        public static string parseStringDate(this DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }
        public static string parsePrice(this decimal price)
        {
            return Math.Round(price,0, MidpointRounding.AwayFromZero).ToString("###,###,###,##0.#0");
        }
        public static string parseTerbilang(this decimal price)
        {
            return SayNumber.Terbilang(price) + " Rupiah";
        }
    }
}
