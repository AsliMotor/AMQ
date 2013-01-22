using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findDailySalesReport", @"select 
            sum(price) as Total, 
            invoicedate as SalesDate
        from invoicesnapshot 
        where (invoicedate between @fromDate and @toDate) and branchid = @branchid and status != 3 group by invoicedate
        order by invoicedate asc")]
    public class DailySalesReport : IViewModel
    {
        public DateTime SalesDate { get; set; }
        public decimal Total { get; set; }
        public long SalesDateTick { get { return (long)SalesDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds; } }
    }
}
