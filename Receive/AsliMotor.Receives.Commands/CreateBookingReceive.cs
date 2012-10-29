using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Receives.Commands
{
    public class CreateBookingReceive
    {
        public string BranchId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal Total { get; set; }
    }
}
