using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Receives.Commands
{
    public class CreateAngsuranReceive
    {
        public string BranchId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal Total { get; set; }
        public decimal Denda { get; set; }
        public string BulanAngsuran { get; set; }
        public long BulanAngsuranNumber { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
