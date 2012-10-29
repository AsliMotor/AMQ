using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Invoices.Command
{
    public class UpdateToCashCommand
    {
        public string BranchId { get; set; }
        public Guid InvoiceId { get; set; }
    }
}
