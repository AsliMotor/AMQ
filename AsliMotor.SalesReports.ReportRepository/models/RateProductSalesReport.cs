using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findRateProductSalesReport", @"
        select 
            p.type, 
            count(*) as Total 
        from invoicesnapshot inv inner join product p on inv.productid = p.id 
        where (inv.invoicedate between @fromDate and @toDate) and inv.branchid = @branchid and inv.status != 3
        group by p.type order by Total desc limit @periority")]
    public class RateProductSalesReport : IViewModel
    {
        public int No { get; set; }
        public string Type { get; set; }
        public long Total { get; set; }
    }
}
