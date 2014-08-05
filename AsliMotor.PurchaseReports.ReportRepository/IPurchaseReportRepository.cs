using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PurchaseReports.ReportRepository
{
    public interface IPurchaseReportRepository
    {
        IList<DailyPurchaseReport> FindDailyPurchaseReport(string branchid, DateTime fromDate, DateTime toDate);
        IList<MonthlyPurchaseReport> FindMonthlyPurchaseReport(string branchid, int year);
        IList<YearlyPurchaseReport> FindYearlyPurchaseReport(string branchid, int fromYear, int toYear);
        IList<RateProductPurchaseReport> FindRateProductPurchaseReport(string branchid, DateTime fromDate, DateTime toDate, int periority);
        IList<GrafikProductPurchaseReport> FindGrafikProductPurchaseReport(string branchid, DateTime fromDate, DateTime toDate);
    }
}
