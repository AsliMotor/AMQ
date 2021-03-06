﻿using System;
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
            key = "%" + key.ToLower() + "%";
            IList<CustomerSearch> results = QueryObjectMapper.Map<CustomerSearch>("findByKey", new string[] { "key", "branchid" }, new object[] { key.ToLower(), branchid });
            return results;
        }

        public Customer GetByKTPNo(string ktpno, string branchid)
        {
            Customer cust = QueryObjectMapper.Map<Customer>("findByKTPNo", new string[] { "ktpno", "branchid" }, new object[] { ktpno, branchid }).FirstOrDefault();
            return cust;
        }

        public IList<CustomerReport> GetListView(string branchid, int offset)
        {
            IList<CustomerReport> results = QueryObjectMapper.Map<CustomerReport>("findAllByOffset", new string[] { "branchid", "offset" }, new object[] { branchid, (offset * 10) });
            return results;
        }

        public TotalCustomer GetTotalList(string branchid)
        {
            TotalCustomer result = QueryObjectMapper.Map<TotalCustomer>("count", new string[] { "branchid" }, new object[] { branchid }).FirstOrDefault();
            return result;
        }

        public IList<CustomerReport> SearchListView(string branchid, int offset, string key)
        {
            key = "%" + key.ToLower() + "%";
            IList<CustomerReport> results = QueryObjectMapper.Map<CustomerReport>("searchByKey", new string[] { "branchid", "offset", "key" }, new object[] { branchid, (offset * 10), key }).ToList();
            return results;
        }

        public CustomerImage GetImage(Guid id)
        {
            CustomerImage image = QueryObjectMapper.Map<CustomerImage>("findById", new string[] { "id" }, new object[] { id }).FirstOrDefault();
            return image;
        }

        public IList<HistoryCreditNote> GetHistoryCreditNote(Guid custId)
        {
            IList<HistoryCreditNote> histories = QueryObjectMapper.Map<HistoryCreditNote>("findById", new string[] { "custid" }, new object[] { custId }).ToList();
            return histories;
        }
    }
}
