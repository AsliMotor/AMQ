using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"SELECT 
	cust.name as CustomerName,
	inv.banyakcicilan,
    inv.outstanding,
	inv.angsuranbulanan,
    inv.duedate,
    inv.termvalue,
    inv.discount,
	prod.Merk,
	prod.Type,
	prod.Warna,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoPolisi,
	r.total as Total,
	r.denda as Denda,
	r.receiveno as ReceiveNo,
	r.receivedate as ReceiveDate,
	(select count(*) from receive where invoiceid = inv.id and receivetype = 3) as BanyakCicilanTerbayar
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
				 inner join (select * from receive where receivetype = 4) r on r.invoiceid = inv.id
    where inv.id = @id and inv.branchid = @branchid")]
    public class ReceivePelunasanReport : BasePrintReport, IViewModel
    {
        public string ReceiveNo { get; set; }
        public int BanyakCicilan { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public decimal Total { get; set; }
        public decimal Denda { get; set; }
        public decimal Discount { get; set; }
        public decimal Outstanding { get; set; }
        public long BanyakCicilanTerbayar { get; set; }
        public int TermValue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReceiveDate { get; set; }
    }
}
