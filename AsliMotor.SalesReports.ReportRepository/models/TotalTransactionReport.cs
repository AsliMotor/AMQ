using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findTotalTransaction", @"select
	(select count(*) from invoicesnapshot where invoicedate = date 'today' and branchid=@branchid)as TotalSoldTransactionToday,
	(select count(*) from invoicesnapshot where invoicedate = date 'yesterday' and branchid=@branchid) as TotalSoldTransactionYesterday,
    (select count(*) from invoicesnapshot where extract(month from invoicedate) = extract(month from NOW()) and extract(year from invoicedate) = extract(year from NOW()) and branchid=@branchid) as TotalSoldTransactionThisMonth,
	(select count(*) from supplierinvoice where supplierinvoicedate = date 'today' and branchid=@branchid)as TotalPurchaseTransactionToday,
	(select count(*) from supplierinvoice where supplierinvoicedate = date 'yesterday' and branchid=@branchid) as TotalPurchaseTransactionYesterday,
    (select count(*) from supplierinvoice where extract(month from supplierinvoicedate) = extract(month from NOW()) and extract(year from supplierinvoicedate) = extract(year from NOW()) and branchid=@branchid) as TotalPurchaseTransactionThisMonth")]
    public class TotalTransactionReport:IViewModel
    {
        public long TotalSoldTransactionToday { get; set; }
        public long TotalSoldTransactionYesterday { get; set; }
        public long TotalSoldTransactionThisMonth { get; set; }
        public long TotalPurchaseTransactionToday { get; set; }
        public long TotalPurchaseTransactionYesterday { get; set; }
        public long TotalPurchaseTransactionThisMonth { get; set; }
    }
}
