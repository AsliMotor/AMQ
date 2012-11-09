using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Receives.Models;
using BonaStoco.Inf.Reporting;
using BonaStoco.Inf.DataMapper;

namespace AsliMotor.Receives.Repository
{
    public class ReceiveRepository : IReceiveRepository
    {
        public IReportingRepository ReportingRepository { get; set; }
        public IQueryObjectMapper QueryObjectMapper { get; set; }

        public Receive GetByInvoiceIdAndPaymentType(Guid invoiceId, int receiveType)
        {
            Receive rcv = QueryObjectMapper.Map<Receive>("findByInvoiceIdAndPaymentType", new string[] { "invoiceid", "receivetype" }, new object[] { invoiceId, receiveType }).FirstOrDefault();
            return rcv;
        }

        public void Save(Receive rcv)
        {
            ReportingRepository.Save<Receive>(rcv);
        }

        public Receive GetBooking(Guid invoiceId)
        {
            Receive rcv = QueryObjectMapper.Map<Receive>("findByInvoiceAndBookingType", new string[] { "invoiceid" }, new object[] { invoiceId }).FirstOrDefault();
            return rcv;
        }

        public void Update(Receive rcv)
        {
            ReportingRepository.Update<Receive>(rcv, new { id = rcv.id });
        }
    }
}
