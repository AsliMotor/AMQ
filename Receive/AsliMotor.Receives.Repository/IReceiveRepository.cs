using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Receives.Models;

namespace AsliMotor.Receives.Repository
{
    public interface IReceiveRepository
    {
        void Save(Receive rcv);
        void Update(Receive rcv);
        Receive GetByInvoiceIdAndPaymentType(Guid invoiceId, int receiveType);
        Receive GetBooking(Guid invoiceId);
    }
}
