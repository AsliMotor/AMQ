using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Receives.Commands
{
    public class CreateUangMukaReceive
    {
        public string BranchId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal Total { get; set; }
        public decimal Charge { get; set; }
    }
}
