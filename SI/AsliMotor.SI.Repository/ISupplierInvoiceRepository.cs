using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.SI.Repository
{
    public interface ISupplierInvoiceRepository
    {
        SupplierInvoice GetById(Guid id, string branchid);
        IList<SupplierInvoiceReport> GetListView(string branchid, int offset);
        TotalSupplierInvoice GetTotalList(string branchid);
        IList<SupplierInvoiceReport> SearchListView(string branchId, int offset, string key);
    }
}
