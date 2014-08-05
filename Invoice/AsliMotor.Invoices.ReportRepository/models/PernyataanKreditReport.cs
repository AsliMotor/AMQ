using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"SELECT 
	cust.name as CustomerName,
	cust.ktpno as NoKtp,
	cust.ktpdate as KtpDate,
	cust.address as BillingAddress,
	cust.city as City,
	cust.job as Job,
	prod.Merk,
	prod.Type,
	prod.Tahun,
	prod.Warna,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoPolisi,
	inv.lamaangsuran as LamaAngsuran,
	inv.angsuranbulanan as AngsuranBulanan,
    inv.banyakcicilan as BanyakCicilan,
	(select (total + charge) from receive where invoiceid = inv.id and receivetype = 1) as UangMuka,
    (select total from receive where invoiceid = inv.id and receivetype = 0) as DebitNote
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
	where inv.id = @id")]
    public class PernyataanKreditReport : BasePrintReport, IViewModel
    {
        public int LamaAngsuran { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public decimal UangMuka { get; set; }
        public decimal DebitNote { get; set; }
        public int BanyakCicilan { get; set; }
    }
}
