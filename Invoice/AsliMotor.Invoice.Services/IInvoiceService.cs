using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Invoices.Command;

namespace AsliMotor.Invoices.Services
{
    public interface IInvoiceService
    {
        void Booking(BookingCommand cmd, string username);
        void UpdateToCash(UpdateToCashCommand cmd, string username);
        void Cash(CashCommand cmd, string username);
        void Credit(CreditCommand cmd, string username);
        void UpdateToCredit(UpdateToCreditCommand cmd, string username);
        void Cancel(Guid id, string username);
        void BayarAngsuran(Guid id, string username);
    }
}
