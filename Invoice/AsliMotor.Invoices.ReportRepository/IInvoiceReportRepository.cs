using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Invoices.ReportRepository
{
    public interface IInvoiceReportRepository
    {
        IList<InvoiceListViewReport> GetListViewReport(string branchid, int offset);
        InvoiceHeaderReport GetHeader(Guid invoiceId, string branchId);
        IList<InvoiceItemReport> GetItems(Guid invoiceId, string branchid);
        InvoiceBookingReport GetInvoiceBookingReport(Guid invoiceId, string branchid);
        InvoiceCashReport GetInvoiceCashReport(Guid invoiceId, string branchid);
        ReceiveUangMukaReport GetReceiveUangMukaReport(Guid invoiceId, string branchid);
        ReceiveAngsuranReport GetReceiveAngsuranReport(Guid rcvId);
        TotalInvoice GetTotalListView(string branchId);
    }
}
