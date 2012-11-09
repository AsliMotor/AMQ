using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Receives.Models;
using AsliMotor.Receives.Commands;
using AsliMotor.Receives.AutoNumberGenerator;
using BonaStoco.Inf.Reporting;
using AsliMotor.Receives.Repository;

namespace AsliMotor.Receives.Services
{
    public class ReceiveService : IReceiveService
    {
        public IReceiveRepository ReceiveRepository { get; set; }
        public IReceiveAutoNumberGenerator ReceiveAutoNumberGenerator { get; set; }
        public void CreateBooking(CreateBookingReceive cmd)
        {
            FailIfBookingExist(cmd.InvoiceId);
            Receive rcv = new Receive
            {
                id = Guid.NewGuid(),
                InvoiceId = cmd.InvoiceId,
                ReceiveDate = DateTime.Now,
                ReceiveNo = ReceiveAutoNumberGenerator.GenerateReceiveNumber(DateTime.Now, cmd.BranchId),
                ReceiveType = (int)ReceiveTypes.UANGTANDAJADI,
                Total = cmd.Total,
                BranchId = cmd.BranchId
            };
            ReceiveRepository.Save(rcv);
        }

        public void CreateCash(CreateCashReceive cmd)
        {
            FailIfCashExist(cmd.InvoiceId);
            Receive rcv = new Receive
            {
                id = Guid.NewGuid(),
                InvoiceId = cmd.InvoiceId,
                ReceiveDate = DateTime.Now,
                ReceiveNo = ReceiveAutoNumberGenerator.GenerateReceiveNumber(DateTime.Now, cmd.BranchId),
                ReceiveType = (int)ReceiveTypes.CASH,
                Total = cmd.Total,
                BranchId = cmd.BranchId
            };
            ReceiveRepository.Save(rcv);
        }

        public void CreateUangMuka(CreateUangMukaReceive cmd)
        {
            Receive rcv = new Receive
            {
                id = Guid.NewGuid(),
                InvoiceId = cmd.InvoiceId,
                ReceiveDate = DateTime.Now,
                ReceiveNo = ReceiveAutoNumberGenerator.GenerateReceiveNumber(DateTime.Now, cmd.BranchId),
                ReceiveType = (int)ReceiveTypes.UANGMUKA,
                Total = cmd.Total,
                BranchId = cmd.BranchId
            };
            ReceiveRepository.Save(rcv);
        }

        public void ChangeUangMuka(Guid invoiceId, decimal uangMuka)
        {
            Receive rcv = ReceiveRepository.GetByInvoiceIdAndPaymentType(invoiceId, (int)ReceiveTypes.UANGMUKA);
            rcv.ReceiveDate = DateTime.Now;
            rcv.Total = uangMuka;
            ReceiveRepository.Update(rcv);
        }

        public void CreateAngsuran(CreateAngsuranReceive cmd)
        {
            Receive rcv = new Receive
            {
                id = Guid.NewGuid(),
                InvoiceId = cmd.InvoiceId,
                ReceiveDate = cmd.PaymentDate,
                ReceiveNo = ReceiveAutoNumberGenerator.GenerateReceiveNumber(DateTime.Now, cmd.BranchId),
                ReceiveType = (int)ReceiveTypes.ANGSURAN,
                Total = cmd.Total,
                BranchId = cmd.BranchId,
                Denda = cmd.Denda,
                Month = cmd.BulanAngsuran
            };
            ReceiveRepository.Save(rcv);
        }

        private void FailIfBookingExist(Guid invId)
        {
            Receive rcv = ReceiveRepository.GetByInvoiceIdAndPaymentType(invId, (int) ReceiveTypes.UANGTANDAJADI);
            if (rcv != null)
                throw new ApplicationException("Invoice ini telah mempunyai uang tanda jadi");
        }

        private void FailIfCashExist(Guid invId)
        {
            Receive rcv = ReceiveRepository.GetByInvoiceIdAndPaymentType(invId, (int)ReceiveTypes.CASH);
            if (rcv != null)
                throw new ApplicationException("Invoice ini telah dibayar cash");
        }
    }
}
