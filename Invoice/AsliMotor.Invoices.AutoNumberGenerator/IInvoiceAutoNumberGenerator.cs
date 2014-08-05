using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Invoices.AutoNumberGenerator
{
    public interface IInvoiceAutoNumberGenerator
    {
        void SetupInvoiceAutoMumber(int mode, string prefix, string branchId);
        string GenerateInvoiceNumber(DateTime transactionDate, string branchId);
        InvoiceAutoNumberConfig GetInvoiceAutoNumberConfig(string id);
    }
}
