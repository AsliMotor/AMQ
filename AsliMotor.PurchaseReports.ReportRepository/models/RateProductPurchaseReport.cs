using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.PurchaseReports.ReportRepository
{
    [NamedSqlQuery("findRateProductPurchaseReport", @"
        select 
            p.type, 
            count(*) as Total 
        from supplierinvoice inv inner join product p on inv.productid = p.id 
        where (inv.supplierinvoicedate between @fromDate and @toDate) and inv.branchid = @branchid
        group by p.type order by Total desc limit @periority")]
    public class RateProductPurchaseReport : IViewModel
    {
        public int No { get; set; }
        public string Type { get; set; }
        public long Total { get; set; }
    }
}
