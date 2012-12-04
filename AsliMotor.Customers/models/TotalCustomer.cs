using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Customers
{
    [NamedSqlQuery("count",@"SELECT count(*) as Total from customer where branchid = @branchid")]
    public class TotalCustomer : IViewModel
    {
        public long Total { get; set; }
    }
}
