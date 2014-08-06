using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"
    select 
	r.id,
	r.receivedate,
	r.total,
	r.denda,
	r.receivetype as Status,
	r.month as Month,
	r.monthnumber as MonthNumber,
	inv.angsuranbulanan,
	r.receiveno
	from receive r inner join invoicesnapshot inv on r.invoiceid = inv.id 
	where r.invoiceid = @id and r.receivetype = 4 or r.receivetype = 3 and inv.id = @id and inv.branchid = @branchid
	order by r.monthnumber asc, r.receivedate asc
    ")]
    public class InvoiceItemReport : IViewModel
    {
        public Guid id { get; set; }
        public DateTime ReceiveDate { get; set; }
        public decimal Total { get; set; }
        public decimal Denda { get; set; }
        public string Month { get; set; }
        public long MonthNumber { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public string ReceiveNo { get; set; }
        public int Status { get; set; }
        public string MonthFormated
        {
            get
            {
                if (Month.Equals(string.Empty))
                    return string.Empty;
                int month = Convert.ToInt32(Month.Substring(0, 2));
                int year = Convert.ToInt32(Month.Substring(2, 4));
                return new DateTime(year, month, 1).ToString("MMMM yyyy");
            }
        }
    }
}
