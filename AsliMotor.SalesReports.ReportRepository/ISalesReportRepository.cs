using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.SalesReports.ReportRepository
{
    public interface ISalesReportRepository
    {
        IList<DailySalesReport> FindDailySalesReport(string branchid, DateTime fromDate, DateTime toDate);
        IList<MonthlySalesReport> FindMonthlySalesReport(string branchid, int year);
        IList<YearlySalesReport> FindYearlySalesReport(string branchid, int fromYear, int toYear);
    }
}
