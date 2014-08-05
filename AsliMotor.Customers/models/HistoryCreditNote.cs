using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Customers
{
    [NamedSqlQuery("findById", @"select 
id,
receiveno,
receivedate,
total,
(total + deposit) as totalyangdibayar,
CASE WHEN deposit < 0  THEN (deposit * -1) END as Debit,
CASE WHEN deposit > 0  THEN deposit END as Kredit,
(select sum(r2.deposit) from receive r2 where r2.invoiceid IN 
	(select id from invoicesnapshot where customerid = @custid) and r2.transactiondate <= r1.transactiondate) as Saldo
from receive r1
   where invoiceid IN 
	(select id from invoicesnapshot where customerid = @custid)
and deposit != 0
order by transactiondate asc")]
    public class HistoryCreditNote:IViewModel
    {
        public Guid id { get; set; }
        public string ReceiveNo { get; set; }
        public DateTime ReceiveDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalYangDiBayar { get; set; }
        public decimal Debit { get; set; }
        public decimal Kredit { get; set; }
        public decimal Saldo { get; set; }
    }
}
