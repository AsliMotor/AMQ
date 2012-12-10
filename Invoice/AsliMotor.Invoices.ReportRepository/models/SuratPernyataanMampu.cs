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
	cust.city as City,
	cust.birthday as Birthday,
	cust.gender as Gender,
	cust.gender as Job,
	prod.Merk,
	prod.Warna,
	prod.Tahun,
	prod.Type,
	prod.NoRangka,
	prod.NoMesin,
	prod.NoPolisi
	FROM invoicesnapshot inv inner join customer cust on inv.customerid = cust.id 
				 inner join product prod on inv.productid = prod.id
	where inv.id = @id")]
    public class SuratPernyataanMampu : BasePrintReport, IViewModel
    {
    }
}
