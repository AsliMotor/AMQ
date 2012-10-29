using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper;

namespace AsliMotor.Invoices.ReportRepository
{
    public class InvoiceReportRepository:IInvoiceReportRepository
    {
        public IQueryObjectMapper QueryObjectMapper { get; set; }
        public InvoiceReport Get(Guid invoiceId, string branchId)
        {
            InvoiceReport inv = QueryObjectMapper.Map<InvoiceReport>("findById", new string[] { "id", "branchid" }, new object[] { invoiceId, branchId }).FirstOrDefault();
            return inv;
        }

        public IList<InvoiceListViewReport> GetListViewReport(string branchid, int offset)
        {
            IList<InvoiceListViewReport> listView = QueryObjectMapper.Map<InvoiceListViewReport>("findListView", new string[] { "branchid", "offset" }, new object[] { branchid, (offset * 10) });
            return listView;
        }
    }
}
