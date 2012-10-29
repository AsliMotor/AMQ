using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Products.Models
{
    [NamedSqlQuery("count", @"select count(*) as Total from product where branchid = @branchid and status = @status")]
    public class TotalProduct : IViewModel
    {
        public long Total { get; set; }
    }
}
