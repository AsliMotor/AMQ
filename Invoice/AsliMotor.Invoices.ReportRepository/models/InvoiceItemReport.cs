using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"
    select 
	r.id,
	r.receivedate,
	r.total,
	r.denda,
    r.month as Month,
	inv.angsuranbulanan,
	r.receiveno
	from receive r inner join invoicesnapshot inv on r.invoiceid = inv.id 
	where r.receivetype = 3 and inv.id = @id and inv.branchid = @branchid
	order by r.receiveno asc
    ")]
    public class InvoiceItemReport : IViewModel
    {
        public Guid id { get; set; }
        public DateTime ReceiveDate { get; set; }
        public decimal Total { get; set; }
        public decimal Denda { get; set; }
        public string Month { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public string ReceiveNo { get; set; }
    }
}
