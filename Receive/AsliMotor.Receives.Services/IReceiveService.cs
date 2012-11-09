using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Receives.Commands;

namespace AsliMotor.Receives.Services
{
    public interface IReceiveService
    {
        void CreateBooking(CreateBookingReceive cmd);
        void CreateCash(CreateCashReceive cmd);
        void CreateUangMuka(CreateUangMukaReceive cmd);
        void CreateAngsuran(CreateAngsuranReceive cmd);
        void ChangeUangMuka(Guid invoiceId, decimal uangMuka);
    }
}
