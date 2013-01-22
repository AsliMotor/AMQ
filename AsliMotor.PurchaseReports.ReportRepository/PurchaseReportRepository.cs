using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper;

namespace AsliMotor.PurchaseReports.ReportRepository
{
    public class PurchaseReportRepository:IPurchaseReportRepository
    {
        public IQueryObjectMapper QueryObjectMapper { get; set; }
        public IList<DailyPurchaseReport> FindDailyPurchaseReport(string branchid, DateTime fromDate, DateTime toDate)
        {
            IList<DailyPurchaseReport> reports = QueryObjectMapper.Map<DailyPurchaseReport>("findDailyPurchaseReport",
                new string[] { "branchid", "fromDate", "toDate" },
                new object[] { branchid, fromDate, toDate }).ToList();
            int different = toDate.Subtract(fromDate).Days;
            for (int i = 0; i < different; i++)
            {
                DateTime currDate = fromDate.AddDays(i);
                bool isSame = false;
                foreach (DailyPurchaseReport s in reports)
                {
                    if (s.PurchaseDate.Date == currDate.Date) { isSame = true; break; }
                }
                if (isSame == false)
                {
                    reports.Add(new DailyPurchaseReport { PurchaseDate = currDate, Total = 0 });
                }
            }
            return reports.OrderBy(i => i.PurchaseDate).ToList();
        }

        public IList<MonthlyPurchaseReport> FindMonthlyPurchaseReport(string branchid, int year)
        {
            IList<MonthlyPurchaseReport> reports = QueryObjectMapper.Map<MonthlyPurchaseReport>("findMonthlyPurchaseReport",
               new string[] { "branchid", "year" },
               new object[] { branchid, year }).ToList();
            for (int i = 0; i < 12; i++)
            {
                DateTime currYear = new DateTime(year, i + 1, 1);
                bool isSame = false;
                foreach (MonthlyPurchaseReport s in reports)
                {
                    if (s.PurchaseDate == currYear) { isSame = true; break; }
                }
                if (isSame == false)
                {
                    reports.Add(new MonthlyPurchaseReport { PurchaseDate = currYear, Total = 0 });
                }
            }
            return reports.OrderBy(i => i.PurchaseDate).ToList();
        }

        public IList<YearlyPurchaseReport> FindYearlyPurchaseReport(string branchid, int fromYear, int toYear)
        {
            IList<YearlyPurchaseReport> reports = QueryObjectMapper.Map<YearlyPurchaseReport>("findYearlyPurchaseReport",
               new string[] { "branchid", "fromyear", "toyear" },
               new object[] { branchid, fromYear, toYear }).ToList();
            int different = toYear - fromYear;
            for (int i = 0; i < different; i++)
            {
                DateTime currentYear = new DateTime(fromYear, 1, 1).AddYears(i);
                bool isSame = false;
                foreach (YearlyPurchaseReport s in reports)
                {
                    if (s.PurchaseDate == currentYear) { isSame = true; break; }
                }
                if (isSame == false)
                {
                    reports.Add(new YearlyPurchaseReport { PurchaseDate = currentYear, Total = 0 });
                }
            }
            return reports.OrderBy(i => i.PurchaseDate).ToList();
        }

        public IList<RateProductPurchaseReport> FindRateProductPurchaseReport(string branchid, DateTime fromDate, DateTime toDate, int periority)
        {
            IList<RateProductPurchaseReport> reports = QueryObjectMapper.Map<RateProductPurchaseReport>("findRateProductPurchaseReport",
                new string[] { "branchid", "fromDate", "toDate", "periority" },
                new object[] { branchid, fromDate, toDate, periority })
                .ToList();
            for (int i = 0; i < reports.Count; i++)
            {
                reports[i].No = i + 1;
            }
            return reports;
        }

        public IList<GrafikProductPurchaseReport> FindGrafikProductPurchaseReport(string branchid, DateTime fromDate, DateTime toDate)
        {
            IList<GrafikProductPurchaseReport> reports = QueryObjectMapper.Map<GrafikProductPurchaseReport>("findGrafikProductPurchaseReport",
                new string[] { "branchid", "fromDate", "toDate" },
                new object[] { branchid, fromDate, toDate })
                .ToList();
            return reports;
        }
    }
}
