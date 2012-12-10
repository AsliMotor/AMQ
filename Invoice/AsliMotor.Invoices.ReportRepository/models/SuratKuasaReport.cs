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
	(select suratperjanjianno from suratperjanjian where invoiceid = inv.id) as NoSuratPerjanjian,
	(select suratperjanjiandate from suratperjanjian where invoiceid = inv.id) as SuratPerjanjianDate
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
	where inv.id = @id")]
    public class SuratKuasaReport:BasePrintReport,IViewModel
    {
        public string NoSuratPerjanjian { get; set; }
        public DateTime SuratPerjanjianDate { get; set; }
    }
}
