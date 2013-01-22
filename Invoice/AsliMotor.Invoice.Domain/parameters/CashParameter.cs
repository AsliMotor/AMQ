using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Invoices.Snapshots;

namespace AsliMotor.Invoices.Domain
{
    public class CashParameter
    {
        public string BranchId { get; set; }
        public Guid id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public StatusInvoice Status { get; set; }
        public decimal Price { get; set; }
        public string InvoiceNo { get; set; }
    }
}
