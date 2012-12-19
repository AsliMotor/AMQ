using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;

namespace AsliMotor.AuditLog.Repository
{
    public class PriceChangedRepository:IPriceChangedRepository
    {
        public QueryObjectMapper QueryObjectMapper { get; set; }
        public IList<SupplierInvoicePriceChangedLog> GetListSIPriceChangedLog(string branchid, int offset)
        {
            IList<SupplierInvoicePriceChangedLog> result = QueryObjectMapper.Map<SupplierInvoicePriceChangedLog>("getList",
                new string[] { "branchid", "offset" },
                new object[] { branchid, offset * 10 }).ToList();
            return result;
        }

        public TotalSupplierInvoicePriceChangedLog GetTotalSIPriceChangeLog(string branchid)
        {
            TotalSupplierInvoicePriceChangedLog result = QueryObjectMapper.Map<TotalSupplierInvoicePriceChangedLog>("count",
                new string[] { "branchid" },
                new object[] { branchid }).FirstOrDefault();
            return result;
        }

        public IList<InvoicePriceChangedLog> GetListInvoicePriceChangedLog(string branchid, int offset)
        {
            IList<InvoiceAuditLog> result = QueryObjectMapper.Map<InvoiceAuditLog>("getList",
                new string[] { "branchid", "offset" },
                new object[] { branchid, offset }).ToList();
            IList<InvoicePriceChangedLog> logs = new List<InvoicePriceChangedLog>();
            foreach (InvoiceAuditLog i in result)
            {
                InvoicePayloadLog payload = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoicePayloadLog>(i.Payload);
                Product product = QueryObjectMapper.Map<Product>("findById", new string[] { "id" }, new object[] { payload.ProductId }).FirstOrDefault();
                Customer cust = QueryObjectMapper.Map<Customer>("findById", new string[] { "id" }, new object[] { payload.CustomerId }).FirstOrDefault();
                logs.Add(new InvoicePriceChangedLog()
                {
                    id = i.Id,
                    InvoiceId = i.InvoiceId,
                    Action = i.Action,
                    AngsuranBulanan = payload.AngsuranBulanan,
                    CustomerName = cust.Name,
                    NoPolisi = product.NoPolisi,
                    NoMesin = product.NoMesin,
                    NoRangka = product.NoRangka,
                    SukuBunga = payload.SukuBunga,
                    TotalKredit = payload.TotalKredit,
                    DateTime = i.DateTime,
                    LamaAngsuran = payload.LamaAngsuran,
                    UserName = i.UserName
                });
            }
            return logs;
        }


        public TotalInvoicePriceChangedLog GetTotalInvoicePriceChangeLog(string branchid)
        {
            TotalInvoicePriceChangedLog result = QueryObjectMapper.Map<TotalInvoicePriceChangedLog>("count",
                new string[] { "branchid" },
                new object[] { branchid }).FirstOrDefault();
            return result;
        }
    }
}
