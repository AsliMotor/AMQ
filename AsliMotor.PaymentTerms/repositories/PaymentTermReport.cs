using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.PaymentTerms
{
    [NamedSqlQuery("findAll", @"select id, termname as Name, value as Value, type as Type from paymentterm where ownerid = @ownerid")]
    [NamedSqlQuery("findById", @"select id, termname as Name, value as Value, type as Type from paymentterm where id = @id")]
    public class PaymentTermReport : IViewModel
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public TermType Type { get; set; }
    }
}
