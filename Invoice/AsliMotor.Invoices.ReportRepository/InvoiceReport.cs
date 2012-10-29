using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"SELECT 
	inv.id as id ,
	inv.branchid as BranchId,
	inv.invoicedate as InvoiceDate,
	inv.price as Price,
	inv.status as Status,
	inv.customerid as CustomerId,
	cust.name as CustomerName,
	cust.address as CustomerAddress,
	cust.city as CustomerCity,
	cust.region as CustomerRegion,
	cust.phone as CustomerPhone,
	inv.productid as ProductId,
	prod.Merk,
	prod.Type,
	prod.Tahun,
	prod.Warna,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoBpkb,
	prod.NoPolisi,
	(select total from receive where invoiceid = inv.id and receivetype = 0) as DebitNote,
	(select total from receive where invoiceid = inv.id and receivetype = 2) as Cash
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
	where inv.id = @id and inv.branchid = @branchid")]
    public class InvoiceReport:IViewModel
    {
        public Guid id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerRegion { get; set; }
        public string CustomerPhone { get; set; }
        public Guid ProductId { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Tahun { get; set; }
        public string Warna { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string NoBpkb { get; set; }
        public string NoPolisi { get; set; }
        public string BranchId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Price { get; set; }
        public decimal DebitNote { get; set; }
        public decimal Cash { get; set; }
        public int Status { get; set; }
    }
}
