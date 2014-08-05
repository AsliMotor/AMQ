using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findMonthlySalesReport", @"SELECT
                    sum(price) AS Total,
                    date_trunc('month', invoicedate) AS SalesDate
                FROM invoicesnapshot
                where branchid = @branchid and 
	              extract(year from invoicedate) = @year
                  and status != 3
                GROUP BY SalesDate")]
    public class MonthlySalesReport : IViewModel
    {
        public DateTime SalesDate { get; set; }
        public decimal Total { get; set; }
        public long SalesDateTick { get { return (long)SalesDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds; } }
    }
}
