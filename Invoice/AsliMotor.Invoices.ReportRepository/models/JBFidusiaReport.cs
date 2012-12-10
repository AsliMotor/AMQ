using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"SELECT 
	cust.name as CustomerName,
	cust.address as BillingAddress,
	cust.city as City,
	cust.birthday as Birthday,
	cust.gender as Gender,
	cust.gender as Job,
	cust.ktpno as NoKtp,
	cust.ktpdate as KtpDate,
	cust.ktppublisher as KtpPublisher,
	prod.Merk,
	prod.Warna,
	prod.Tahun,
	prod.Type,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoPolisi,
    inv.lamaangsuran as LamaAngsuran,
	inv.price + inv.totalkredit as Price,
	(select suratperjanjianno from suratperjanjian where invoiceid = inv.id) as NoSuratPerjanjian,
	(select suratperjanjiandate from suratperjanjian where invoiceid = inv.id) as SuratPerjanjianDate,
	(select total from receive where invoiceid = inv.id and receivetype = 0) as Booking,
	(select (total + charge) from receive where invoiceid = inv.id and receivetype = 1) as UangMuka
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
	where inv.id = @id")]
    public class JBFidusiaReport:BasePrintReport,IViewModel
    {
        public string NoSuratPerjanjian { get; set; }
        public DateTime SuratPerjanjianDate { get; set; }
        public decimal UangMuka { get; set; }
        public int LamaAngsuran { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public decimal Price { get; set; }
        public decimal Booking { get; set; }
    }
}
