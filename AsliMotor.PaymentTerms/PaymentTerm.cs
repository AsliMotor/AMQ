using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.PaymentTerms
{
    public class PaymentTerm : IViewModel
    {
        public Guid id { get; set; }
        public string TermName { get; set; }
        public int Value { get; set; }
        public string OwnerId { get; set; }
    }
}
