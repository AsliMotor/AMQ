using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.AuditLog.Repository
{
    [NamedSqlQuery("count", @"select count(*)
    from invoicelog where (action = 3 or action = 5 or action = 6 or action = 7) and branchid = @branchid")]
    public class TotalInvoicePriceChangedLog : IViewModel
    {
        public long Total { get; set; }
    }
}
