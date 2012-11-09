using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.Snapshots
{
    [NamedSqlQuery("findById", @"select total from receive where invoiceid = @invid and receivetype = 0")]
    public class UangTandaJadi:IViewModel
    {
        public decimal Total { get; set; }
    }
}
