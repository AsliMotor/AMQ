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
        void Pull(Guid id, string username);
        void ChangeUangMuka(Guid id, decimal uangmuka, string username);
        void BayarAngsuran(Guid id, DateTime date, string username);
        void UpdateUangAngsuran(Guid id, decimal angsuran, string username);
        void ChangeSukuBunga(Guid id, decimal sukubunga, string username);
        void ChangeLamaAngsuran(Guid id, int lamaAngsuran, string username);
        void ChangeDueDate(Guid id, DateTime dueDate, string username);
        void ChangeProduct(Guid id, Guid productId, string username);
        void ChangeCustomer(Guid id, Guid custId, string username);
    }
}
