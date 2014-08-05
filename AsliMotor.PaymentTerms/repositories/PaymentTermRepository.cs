using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper;

namespace AsliMotor.PaymentTerms
{
    public class PaymentTermRepository:IPaymentTermRepository
    {
        public IQueryObjectMapper QueryObjectMapper { get; set; }
        public IList<PaymentTermReport> FindAll(string ownerId)
        {
            IList<PaymentTermReport> result = QueryObjectMapper.Map<PaymentTermReport>("findAll", new string[] { "ownerid" }, new object[] { ownerId }).ToList();
            return result;
        }

        public PaymentTermReport GetById(Guid id)
        {
            PaymentTermReport result = QueryObjectMapper.Map<PaymentTermReport>("findById", new string[] { "id" }, new object[] { id }).FirstOrDefault();
            return result;
        }
    }
}
