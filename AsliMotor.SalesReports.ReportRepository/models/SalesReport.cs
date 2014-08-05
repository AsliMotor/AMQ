using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findSales", @"select 
	(select sum(price) from invoicesnapshot where invoicedate = date 'today' and branchid = @branchid and status != 3) as TodaySales,
	(select sum(price) from invoicesnapshot where invoicedate = date 'yesterday' and branchid = @branchid and status != 3) as YesterdaySales,
	(select sum(price) from invoicesnapshot where extract(month from invoicedate) = extract(month from NOW()) and extract(year from invoicedate) = extract(year from NOW()) and branchid = @branchid and status != 3) as ThisMonthSales,
	(select sum(price) from invoicesnapshot where extract(month from invoicedate) = extract(month from NOW())-1 and extract(year from invoicedate) = extract(year from NOW()) and branchid = @branchid and status != 3) as YesterdayMonthSales,
	(select sum(price) from invoicesnapshot where extract(year from invoicedate) = extract(year from NOW()) and branchid = @branchid and status != 3) as ThisYearSales,
    (select sum(outstanding) from invoicesnapshot where branchid = @branchid and status != 3 and status != 2) as TotalOutstanding")]
    public class SalesReport : IViewModel
    {
        public decimal TodaySales { get; set; }
        public decimal YesterdaySales { get; set; }
        public decimal ThisMonthSales { get; set; }
        public decimal YesterdayMonthSales { get; set; }
        public decimal ThisYearSales { get; set; }
        public decimal TotalOutstanding { get; set; }
    }
}
