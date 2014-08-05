using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Customers
{
    [NamedSqlQuery("findAllByOffset", @"SELECT id, 
                                        name, 
                                        address,
                                        city,
                                        phone,
                                        gender,
                                        deposit,
                                        (select sum(outstanding) from invoicesnapshot where customerid = cust.id) as Outstanding
                                        FROM customer cust 
                                        where cust.branchid = @branchid
                                        ORDER BY name ASC limit 10 offset @offset")]
    [NamedSqlQuery("searchByKey", @"SELECT id, 
                                        name, 
                                        address,
                                        city,
                                        phone,
                                        gender,
                                        deposit,
                                        (select sum(outstanding) from invoicesnapshot where customerid = cust.id) as Outstanding
                                        FROM customer cust 
                                        where cust.branchid = @branchid and
                                        (LOWER(name) like @key or LOWER(city) like @key or LOWER(address) like @key)
                                        ORDER BY name ASC limit 10 offset @offset")]
    public class CustomerReport : IViewModel
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public decimal Outstanding { get; set; }
        public decimal Deposit { get; set; }
    }
}
