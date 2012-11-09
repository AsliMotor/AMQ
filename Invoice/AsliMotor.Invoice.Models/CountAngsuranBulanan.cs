using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.Snapshots
{
    [NamedSqlQuery("count", @"select count(*) as Total from receive where invoiceid = @invid and receivetype = 3")]
    public class CountAngsuranBulanan:IViewModel
    {
        public long Total { get; set; }
    }
}
