using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Invoices.Command
{
    public class UpdateToCreditCommand
    {
        public string BranchId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal UangMuka { get; set; }
        public int LamaAngsuran { get; set; }
        public decimal SukuBunga { get; set; }
        public DateTime DueDate { get; set; }
    }
}
