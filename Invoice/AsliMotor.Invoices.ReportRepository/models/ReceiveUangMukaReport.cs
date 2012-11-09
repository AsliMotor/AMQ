using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"SELECT 
	cust.name as CustomerName,
	inv.lamaangsuran,
	inv.angsuranbulanan,
	prod.Merk,
	prod.Type,
	prod.Tahun,
	prod.Warna,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoPolisi,
	(select (total + charge) from receive where invoiceid = inv.id and receivetype = 1) as Total,
	(select receiveno from receive where invoiceid = inv.id and receivetype = 1) as ReceiveNo,
	(select suratperjanjianno from suratperjanjian where invoiceid = inv.id) as NoSuratPerjanjian,
	(select suratperjanjiandate from suratperjanjian where invoiceid = inv.id) as SuratPerjanjianDate
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
    where inv.id = @id and inv.branchid = @branchid")]
    public class ReceiveUangMukaReport:IViewModel
    {
        public string ReceiveNo { get; set; }
        public string CustomerName { get; set; }
        public int LamaAngsuran { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public string NoSuratPerjanjian { get; set; }
        public DateTime SuratPerjanjianDate { get; set; }
        public decimal Total { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Warna { get; set; }
        public string Tahun { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string NoPolisi { get; set; }
    }
}
