using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;

namespace AsliMotor.SI.Repository
{
    public class SupplierInvoiceRepository : ISupplierInvoiceRepository
    {
        QueryObjectMapper _qryObjectMapper;
        public SupplierInvoiceRepository()
        {
            _qryObjectMapper = ContextRegistry.GetContext().GetObject("QueryObjectMapper") as QueryObjectMapper;
        }

        public SupplierInvoice GetById(Guid id, string branchid)
        {
            SupplierInvoice si = _qryObjectMapper.Map<SupplierInvoice>("findById", new string[] { "id", "branchid" }, new object[] { id, branchid }).FirstOrDefault();
            return si;
        }

        public IList<SupplierInvoiceReport> GetListView(string branchid, int offset)
        {
            IList<SupplierInvoiceReport> listView = _qryObjectMapper.Map<SupplierInvoiceReport>("findAllByOffset", new string[] { "branchid", "offset" }, new object[] { branchid, (offset * 10) }).ToList();
            return listView;
        }

        public TotalSupplierInvoice GetTotalList(string branchid)
        {
            TotalSupplierInvoice total = _qryObjectMapper.Map<TotalSupplierInvoice>("count", new string[] { "branchid" }, new object[] { branchid }).FirstOrDefault();
            return total;
        }

        public IList<SupplierInvoiceReport> SearchListView(string branchId, int offset, string key)
        {
            key = "%" + key.ToLower() + "%";
            IList<SupplierInvoiceReport> listView = _qryObjectMapper.Map<SupplierInvoiceReport>("searchByKey", new string[] { "branchid", "offset", "key" }, new object[] { branchId, (offset * 10), key }).ToList();
            return listView;
        }
    }
}
