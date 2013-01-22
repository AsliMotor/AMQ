
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findListView", @"select  inv.id,
	                inv.status,
	                inv.invoicedate,
                    inv.invoiceno,
	                (inv.price + inv.totalkredit) - inv.discount as NetTotal,
	                inv.outstanding,
	                p.nopolisi,
	                p.type,
	                cust.name as CustomerName
	                from invoicesnapshot inv 
	                inner join product p on inv.productid = p.id
	                inner join customer cust on inv.customerid = cust.id
	                where inv.branchid = @branchid
	                ORDER BY inv.invoicedate desc, inv.invoiceno desc
	                LIMIT 10 OFFSET @offset")]
    [NamedSqlQuery("searchByKey", @"select  
                    inv.id,
	                inv.status,
	                inv.invoicedate,
                    inv.invoiceno,
	                (inv.price + inv.totalkredit)- inv.discount as NetTotal,
	                inv.outstanding,
	                p.nopolisi,
	                p.type,
	                cust.name as CustomerName
	                from invoicesnapshot inv 
	                inner join product p on inv.productid = p.id
	                inner join customer cust on inv.customerid = cust.id
	                where inv.branchid = @branchid and
		                (LOWER(p.nopolisi) like @key or LOWER(p.type) like @key or LOWER(cust.name) like @key)
	                ORDER BY inv.invoicedate desc, inv.invoiceno desc
	                LIMIT 10 OFFSET @offset")]
    public class InvoiceListViewReport:IViewModel
    {
        public Guid id { get; set; }
        public int Status { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string NoPolisi { get; set; }
        public string Type { get; set; }
        public decimal NetTotal { get; set; }
        public decimal Outstanding { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNo { get; set; }
    }
}
