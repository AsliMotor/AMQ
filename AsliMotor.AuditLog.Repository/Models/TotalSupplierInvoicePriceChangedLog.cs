using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.AuditLog.Repository
{
    [NamedSqlQuery("count", @"select count(*) as Total from supplierinvoicelog where action = 3 and branchid = @branchid")]
    public class TotalSupplierInvoicePriceChangedLog : IViewModel
    {
        public long Total { get; set; }
    }
}
