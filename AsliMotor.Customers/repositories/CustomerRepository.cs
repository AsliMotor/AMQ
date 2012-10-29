using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper;

namespace AsliMotor.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        public IQueryObjectMapper QueryObjectMapper { get; set; }
        public Customer GetByEmail(string email, string branchid)
        {
            Customer cust = QueryObjectMapper.Map<Customer>("findByEmail", new string[] { "email", "branchid" }, new object[] { email, branchid }).FirstOrDefault();
            return cust;
        }

        public Customer GetById(Guid id)
        {
            Customer cust = QueryObjectMapper.Map<Customer>("findById", new string[] { "id" }, new object[] { id }).FirstOrDefault();
            return cust;
        }

        public IList<CustomerSearch> Search(string key, string branchid)
        {
            key = "%" + key + "%";
            IList<CustomerSearch> results = QueryObjectMapper.Map<CustomerSearch>("findByKey", new string[] { "key", "branchid" }, new object[] { key.ToLower(), branchid });
            return results;
        }

        public Customer GetByKTPNo(string ktpno, string branchid)
        {
            Customer cust = QueryObjectMapper.Map<Customer>("findByKTPNo", new string[] { "ktpno", "branchid" }, new object[] { ktpno, branchid }).FirstOrDefault();
            return cust;
        }
    }
}
