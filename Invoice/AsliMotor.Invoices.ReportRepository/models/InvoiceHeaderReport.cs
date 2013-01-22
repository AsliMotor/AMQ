using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"SELECT 
	inv.id as id ,
	inv.invoicedate as InvoiceDate,
	inv.price as Price,
    inv.invoiceno,
	inv.status as Status,
    inv.duedate as DueDate,
    inv.outstanding as Outstanding,
    inv.sukubunga as SukuBunga,
    inv.lamaangsuran as LamaAngsuran,
    inv.banyakcicilan as BanyakCicilan,
	inv.angsuranbulanan as AngsuranBulanan,
	cust.name as CustomerName,
	cust.address as CustomerAddress,
	cust.city as CustomerCity,
	cust.region as CustomerRegion,
	cust.phone as CustomerPhone,
    inv.termid as TermId,
	(select termname from paymentterm where id = inv.termid) as TermName,
    (select value from paymentterm where id = inv.termid) as TermValue,
	prod.Merk,
	prod.Type,
	prod.Tahun,
	prod.Warna,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoPolisi,
	(select total from receive where invoiceid = inv.id and receivetype = 0) as DebitNote,
	(select total from receive where invoiceid = inv.id and receivetype = 1) as UangMuka,
	(select suratperjanjianno from suratperjanjian where invoiceid = inv.id) as SuratPerjanjianNo,
    inv.banyakcicilan - (select count(*) as Total from receive where invoiceid = id and receivetype = 3) as SisaCicilan,
    (select count(*) as Total from receive where invoiceid = inv.id and receivetype = 3) as BanyakCicilanTerbayar
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
	where inv.id = @id and inv.branchid = @branchid")]
    public class InvoiceHeaderReport:IViewModel
    {
        public Guid id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Price { get; set; }
        public decimal SukuBunga { get; set; }
        public int Status { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public decimal Outstanding { get; set; }
        public int LamaAngsuran { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerRegion { get; set; }
        public string CustomerPhone { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Tahun { get; set; }
        public string Warna { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string NoPolisi { get; set; }
        public decimal DebitNote { get; set; }
        public decimal UangMuka { get; set; }
        public string SuratPerjanjianNo { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public int TermValue { get; set; }
        public int BanyakCicilan { get; set; }
        public long SisaCicilan { get; set; }
        public long BanyakCicilanTerbayar { get; set; }
    }
}
