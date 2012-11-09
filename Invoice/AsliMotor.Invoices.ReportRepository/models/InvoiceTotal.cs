using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("count", @"select count(*) as Total from invoicesnapshot where branchid = @branchid")]
    public class TotalInvoice : IViewModel
    {
        public long Total { get; set; }
    }
}
