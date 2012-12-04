using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SalesReports.ReportRepository
{
    [NamedSqlQuery("findSales", @"select 
	(select sum(price + totalkredit) from invoicesnapshot where invoicedate = date 'today' and branchid = @branchid) as TodaySales,
	(select sum(price + totalkredit) from invoicesnapshot where invoicedate = date 'yesterday' and branchid = @branchid) as YesterdaySales,
	(select sum(price + totalkredit) from invoicesnapshot where extract(month from invoicedate) = extract(month from NOW()) and extract(year from invoicedate) = extract(year from NOW()) and branchid = @branchid) as ThisMonthSales,
	(select sum(price + totalkredit) from invoicesnapshot where extract(month from invoicedate) = extract(month from NOW())-1 and extract(year from invoicedate) = extract(year from NOW()) and branchid = @branchid) as YesterdayMonthSales,
	(select sum(price + totalkredit) from invoicesnapshot where extract(year from invoicedate) = extract(year from NOW()) and branchid = @branchid) as ThisYearSales,
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
