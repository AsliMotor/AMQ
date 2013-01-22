using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PaymentTerms
{
    public interface IPaymentTermRepository
    {
        IList<PaymentTermReport> FindAll(string ownerId);
        PaymentTermReport GetById(Guid id);
    }
}
