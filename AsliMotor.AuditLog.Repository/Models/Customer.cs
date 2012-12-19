using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.AuditLog.Repository
{
    [NamedSqlQuery("findById", @"SELECT 
                                name, 
                                address,
                                city
                                FROM customer
                                where id=@id")]
    public class Customer : IViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
