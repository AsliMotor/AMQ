using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Reporting
{
    [NamedSqlQuery("getTransactionListingByCoaKas", @"
        select 
	        Id,TransactionNo,Debit,Kredit,Description,TransactionDate from
	        (select 
		        r.id as Id,
		        r.receiveno as TransactionNo,
		        r.receivedate as TransactionDate,
		        (r.total + r.creditnote) as Debit,
		        0 as Kredit,
		        r.transactiondate as TransactionDateTime,
		        CASE r.receivetype
			        WHEN 0 THEN 'Pembayaran uang tanda jadi'
			        WHEN 1 THEN 'Pembayaran uang muka'
			        WHEN 2 THEN 'Pembayaran cash'
			        WHEN 3 THEN 'Pembayaran angsuran bulanan'
			        WHEN 4 THEN 'Pembayaran pelunasan'
		        END as Description
	        from receive r where (receivedate between @startdate and @todate) and branchid = @branchid
	        union all
	        select 
		        si.id as Id,
		        si.supplierinvoiceno as TransactionNo,
		        si.supplierinvoicedate as TransactionDate,
		        0 as Debit,
		        (si.hargabeli + si.charge) as Kredit,
		        si.transactiondate as TransactionDateTime,
		        'Pembelian Kendaraan ' as Description
	        from supplierinvoice si where (supplierinvoicedate between @startdate and @todate) and branchid = @branchid) tmp
        order by TransactionDate asc
    ")]
    public class TransactionListing:IViewModel
    {
        public Guid Id { get; set; }
        public string TransactionNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Debit { get; set; }
        public decimal Kredit { get; set; }
        public string Description { get; set; }
    }
}
