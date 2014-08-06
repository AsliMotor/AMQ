using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Invoices.Snapshots;
using AsliMotor.PaymentTerms;

namespace AsliMotor.Invoices.Domain
{
    public class UpdateToCreditParameter
    {
        public int LamaAngsuran { get; set; }
        public decimal UangMuka { get; set; }
        public decimal SukuBunga { get; set; }
        public DateTime DueDate { get; set; }
        public decimal UangTandaJadi { get; set; }
        public Guid TermId { get; set; }
        public TermType TermType { get; set; }
        public int TermValue { get; set; }
    }
}
