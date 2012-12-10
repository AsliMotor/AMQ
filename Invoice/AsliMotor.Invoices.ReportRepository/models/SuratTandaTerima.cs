using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.ReportRepository
{
    [NamedSqlQuery("findById", @"SELECT 
	cust.name as CustomerName,
	cust.address as BillingAddress,
    cust.job as Job,
    cust.ktpno as NoKtp,
	cust.city as City,
	prod.Merk,
	prod.Type,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoPolisi
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
	where inv.id = @id")]
    public class SuratTandaTerima : BasePrintReport, IViewModel
    {
    }
}
