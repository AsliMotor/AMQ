using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findYearlySalesReport", @"
                    SELECT
                        sum(price+totalkredit) AS Total,
                        date_trunc('year', invoicedate) AS SalesDate
                    FROM invoicesnapshot
                    where branchid = @branchid and 
	                   (extract(year from invoicedate) between  @fromyear and @toyear)
                        and status != 3
                    GROUP BY SalesDate
                ")]
    public class YearlySalesReport : IViewModel
    {
        public DateTime SalesDate { get; set; }
        public decimal Total { get; set; }
        public long SalesDateTick { get { return (long)SalesDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds; } }
    }
}
