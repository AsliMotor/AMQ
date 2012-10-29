using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Invoices.Command
{
    public class CashCommand
    {
        public string BranchId { get; set; }
        public Guid id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Price { get; set; }
    }
}
