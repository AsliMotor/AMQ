using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.AuditLog.Repository
{
    public interface IPriceChangedRepository
    {
        IList<SupplierInvoicePriceChangedLog> GetListSIPriceChangedLog(string branchid, int offset);
        TotalSupplierInvoicePriceChangedLog GetTotalSIPriceChangeLog(string branchid);
        IList<InvoicePriceChangedLog> GetListInvoicePriceChangedLog(string branchid, int offset);
        TotalInvoicePriceChangedLog GetTotalInvoicePriceChangeLog(string branchid);
    }
}
