using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"SELECT 
	cust.name as CustomerName,
	cust.address as CustomerAddress,
	cust.city as CustomerCity,
	cust.region as CustomerRegion,
	cust.phone as CustomerPhone,
	inv.angsuranbulanan,
	inv.duedate,
    inv.startduedate,
    inv.lamaangsuran,
	(extract(month from age(inv.duedate)) + 1) as DiffrentMonth,
	prod.Merk,
	prod.Type,
	prod.Tahun,
	prod.Warna,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoPolisi,
	(select suratperjanjianno from suratperjanjian where invoiceid = inv.id) as NoSuratPerjanjian,
	(select suratperjanjiandate from suratperjanjian where invoiceid = inv.id) as SuratPerjanjianDate,
	(select count(*) from receive where invoiceid = inv.id and receivetype = 3) as AngsuranKe
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
    where inv.id = @id and inv.branchid = @branchid")]
    public class SuratPeringatanReport : IViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerRegion { get; set; }
        public string CustomerPhone { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDueDate { get; set; }
        public int LamaAngsuran { get; set; }
        public double DiffrentMonth { get; set; }
        public string NoSuratPerjanjian { get; set; }
        public DateTime SuratPerjanjianDate { get; set; }
        public long AngsuranKe { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Warna { get; set; }
        public string Tahun { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string NoPolisi { get; set; }
    }
}
