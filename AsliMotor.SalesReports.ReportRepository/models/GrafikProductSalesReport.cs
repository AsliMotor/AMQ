using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findGrafikProductSalesReport", @"
        select 
            p.type, 
            count(*) as Total 
        from invoicesnapshot inv inner join product p on inv.productid = p.id 
        where (invoicedate between @fromDate and @toDate) and inv.branchid = @branchid and inv.status != 3
        group by p.type")]
    public class GrafikProductSalesReport : IViewModel
    {
        public string Type { get; set; }
        public long Total { get; set; }
    }
}
