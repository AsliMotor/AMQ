using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper;

namespace AsliMotor.SalesReports.ReportRepository
{
    public class SalesReportRepository : ISalesReportRepository
    {
        public IQueryObjectMapper QueryObjectMapper { get; set; }

        public IList<DailySalesReport> FindDailySalesReport(string branchid, DateTime fromDate, DateTime toDate)
        {
            IList<DailySalesReport> reports = QueryObjectMapper.Map<DailySalesReport>("findDailySalesReport",
                new string[] { "branchid", "fromDate", "toDate" },
                new object[] { branchid, fromDate, toDate }).ToList();
            int different = toDate.Subtract(fromDate).Days;
            for (int i = 0; i < different; i++)
            {
                DateTime currDate = fromDate.AddDays(i);
                bool isSame = false;
                foreach (DailySalesReport s in reports)
                {
                    if (s.SalesDate.Date == currDate.Date) { isSame = true; break; }
                }
                if (isSame == false)
                {
                    reports.Add(new DailySalesReport { SalesDate = currDate, Total = 0 });
                }
            }
            return reports.OrderBy(i => i.SalesDate).ToList();
        }

        public IList<MonthlySalesReport> FindMonthlySalesReport(string branchid, int year)
        {
            IList<MonthlySalesReport> reports = QueryObjectMapper.Map<MonthlySalesReport>("findMonthlySalesReport",
               new string[] { "branchid", "year" },
               new object[] { branchid, year }).ToList();
            for (int i = 0; i < 12; i++)
            {
                DateTime currYear = new DateTime(year, i + 1, 1);
                bool isSame = false;
                foreach (MonthlySalesReport s in reports)
                {
                    if (s.SalesDate == currYear) { isSame = true; break; }
                }
                if (isSame == false)
                {
                    reports.Add(new MonthlySalesReport { SalesDate = currYear, Total = 0 });
                }
            }
            return reports.OrderBy(i => i.SalesDate).ToList();
        }

        public IList<YearlySalesReport> FindYearlySalesReport(string branchid, int fromYear, int toYear)
        {
            IList<YearlySalesReport> reports = QueryObjectMapper.Map<YearlySalesReport>("findYearlySalesReport",
               new string[] { "branchid", "fromyear", "toyear" },
               new object[] { branchid, fromYear, toYear }).ToList();
            int different = toYear - fromYear;
            for (int i = 0; i < different; i++)
            {
                DateTime currentYear = new DateTime(fromYear, 1, 1).AddYears(i);
                bool isSame = false;
                foreach (YearlySalesReport s in reports)
                {
                    if (s.SalesDate == currentYear) { isSame = true; break; }
                }
                if (isSame == false)
                {
                    reports.Add(new YearlySalesReport { SalesDate = currentYear, Total = 0 });
                }
            }
            return reports.OrderBy(i => i.SalesDate).ToList();
        }
    }
}
