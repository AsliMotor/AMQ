using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Invoices.Command
{
    public class CreditCommand
    {
        public string BranchId { get; set; }
        public Guid id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Price { get; set; }
        public decimal UangMuka { get; set; }
        public int LamaAngsuran { get; set; }
        public decimal SukuBunga { get; set; }
        public DateTime DueDate { get; set; }
        public Guid TermId { get; set; }
    }
}
