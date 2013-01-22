using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.PurchaseReports.ReportRepository
{
    [NamedSqlQuery("findDailyPurchaseReport", @"select 
            sum(hargabeli) as Total, 
            supplierinvoicedate as PurchaseDate
        from supplierinvoice 
        where (supplierinvoicedate between @fromDate and @toDate) and branchid = @branchid group by supplierinvoicedate
        order by supplierinvoicedate asc")]
    public class DailyPurchaseReport : IViewModel
    {
        public DateTime PurchaseDate { get; set; }
        public decimal Total { get; set; }
        public long PurchaseDateTick { get { return (long)PurchaseDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds; } }
    }
}
