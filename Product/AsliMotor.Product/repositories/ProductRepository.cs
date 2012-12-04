using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using AsliMotor.Products.Models;

namespace AsliMotor.Products
{
    public class ProductRepository : IProductRepository
    {
        QueryObjectMapper _qryObjectMapper;
        public ProductRepository()
        {
            _qryObjectMapper = ContextRegistry.GetContext().GetObject("QueryObjectMapper") as QueryObjectMapper;
        }

        public Product GetByNoPolisi(string nopolisi, string branchid)
        {
            Product product = _qryObjectMapper.Map<Product>("findByNoPolisi", new string[] { "nopolisi", "branchid" }, new object[] { nopolisi, branchid }).FirstOrDefault();
            return product;
        }

        public Product GetByNoRangka(string norangka, string branchid)
        {
            Product product = _qryObjectMapper.Map<Product>("findByNoRangka", new string[] { "norangka", "branchid" }, new object[] { norangka, branchid }).FirstOrDefault();
            return product;
        }

        public Product GetByNoMesin(string nomesin, string branchid)
        {
            Product product = _qryObjectMapper.Map<Product>("findByNoMesin", new string[] { "nomesin", "branchid" }, new object[] { nomesin, branchid }).FirstOrDefault();
            return product;
        }

        public Product GetByNoBpkb(string nobpkp, string branchid)
        {
            Product product = _qryObjectMapper.Map<Product>("findByNoBpkb", new string[] { "nobpkb", "branchid" }, new object[] { nobpkp, branchid }).FirstOrDefault();
            return product;
        }

        public TypeProduct GetTypeByName(string type, string branchid)
        {
            return _qryObjectMapper.Map<TypeProduct>("findByName", new string[] { "name", "branchid" }, new object[] { type, branchid }).FirstOrDefault();
        }

        public Product GetProductById(Guid id, string branchId)
        {
            Product product = _qryObjectMapper.Map<Product>("findById", new string[] { "id", "branchid" }, new object[] { id, branchId }).FirstOrDefault();
            return product;
        }

        public IList<ProductReport> GetListView(string branchid, int offset, string status)
        {
            IList<ProductReport> listView = _qryObjectMapper.Map<ProductReport>("findAllByOffset", new string[] { "branchid", "offset", "status" }, new object[] { branchid, (offset * 10), status }).ToList();
            return listView;
        }

        public IList<ProductReport> SearchListView(string branchId, int offset, string key)
        {
            key = "%" + key.ToLower() + "%";
            IList<ProductReport> listView = _qryObjectMapper.Map<ProductReport>("searchByKey", new string[] { "branchid", "offset", "key" }, new object[] { branchId, (offset * 10), key }).ToList();
            return listView;
        }

        public TotalProduct GetTotalList(string branchid, string status)
        {
            TotalProduct total = _qryObjectMapper.Map<TotalProduct>("count", new string[] { "branchid","status" }, new object[] { branchid, status }).FirstOrDefault();
            return total;
        }
        public IList<ProductSearch> Search(string branchid, string key)
        {
            key = "%" + key.ToLower() + "%";
            IList<ProductSearch> results = _qryObjectMapper.Map<ProductSearch>("findByKey", new string[] { "branchid", "key" }, new object[] { branchid, key});
            return results;
        }
    }
}
