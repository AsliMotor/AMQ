using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Customers
{
    [NamedSqlQuery("findByKey", @"SELECT id, 
                                         name, 
                                         address, 
                                         city,
                                         ktpno,
                                        ktppublisher,
                                        ktpdate,
                                        birthday,
                                        job,
                                        gender
                                         from customer where (LOWER(name) like @key or LOWER(address) like @key) and branchid = @branchid limit 5")]
    public class CustomerSearch : IViewModel
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string KTPNo { get; set; }
        public DateTime KTPDate { get; set; }
        public string KTPPublisher { get; set; }
        public DateTime Birthday { get; set; }
        public string Job { get; set; }
        public string Gender { get; set; }
    }
}
