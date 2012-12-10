using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Customers
{
    [NamedSqlQuery("findById", @"SELECT * FROM customerimage where id = @id")]
    public class CustomerImage : IViewModel
    {
        public Guid id { get; set; }
        public byte[] Image { get; set; }
    }
}
