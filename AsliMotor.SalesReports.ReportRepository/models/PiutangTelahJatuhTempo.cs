using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findPiutang", @"select  inv.id,
	inv.invoicedate,
	(inv.price + inv.totalkredit) as NetTotal,
	(extract(month from age(inv.duedate)) + 1) as DiffrentMonth,
	inv.outstanding,
    inv.DueDate,
	p.nopolisi,
	p.type,
	cust.name as CustomerName
	from invoicesnapshot inv 
	inner join product p on inv.productid = p.id
	inner join customer cust on inv.customerid = cust.id
	where inv.branchid = @branchid and inv.status = 1 and inv.duedate < NOW()::date
	ORDER BY DiffrentMonth desc, inv.duedate asc")]
	//LIMIT 20 OFFSET @offset")]
    public class PiutangTelahJatuhTempo : IViewModel
    {
        public Guid id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string NoPolisi { get; set; }
        public string Type { get; set; }
        public decimal NetTotal { get; set; }
        public decimal Outstanding { get; set; }
        public string CustomerName { get; set; }
        public DateTime DueDate { get; set; }
        public double DiffrentMonth { get; set; }
    }
}
