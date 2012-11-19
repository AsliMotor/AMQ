using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.SalesReports.ReportRepository
{
    public interface IDashboardRepository
    {
        TotalTransactionReport GetTotalTransaction(string branchid);
        SalesReport GetSalesReport(string branchid);
        IList<PiutangTelahJatuhTempo> GetPiutangTelahJatuhTempo(string branchid, int offset);
    }
}
