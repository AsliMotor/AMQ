using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("count", @"select count(*) as Total
	from invoicesnapshot inv 
	where inv.branchid = @branchid and inv.status = 1 and inv.duedate < NOW()::date")]
    public class TotalPiutangTelahJatuhTempo : IViewModel
    {
        public long Total { get; set; }
    }
}
