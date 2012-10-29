using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SI.Repository
{
    [NamedSqlQuery("count",@"select count(*) as Total from supplierinvoice where branchid = @branchid")]
    public class TotalSupplierInvoice:IViewModel
    {
        public long Total { get; set; }
    }
}
