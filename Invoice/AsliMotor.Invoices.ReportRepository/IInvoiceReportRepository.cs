using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Invoices.ReportRepository
{
    public interface IInvoiceReportRepository
    {
        IList<InvoiceListViewReport> GetListViewReport(string branchid, int offset);
        InvoiceReport Get(Guid invoiceId, string branchId);
    }
}
