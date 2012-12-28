using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Receives.Models
{
    [NamedSqlQuery("findByInvoiceIdAndPaymentType", @"SELECT * FROM receive where invoiceid = @invoiceid and receivetype = @receivetype")]
    [NamedSqlQuery("findByInvoiceAndBookingType", @"SELECT * FROM receive where invoiceid = @invoiceid and receivetype = 0")]
    public class Receive : IViewModel
    {
        public Guid id { get; set; }
        public Guid InvoiceId { get; set; }
        public string ReceiveNo { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int ReceiveType { get; set; }
        public decimal Charge { get; set; }
        public decimal Total { get; set; }
        public decimal Denda { get; set; }
        public string BranchId { get; set; }
        public string Month { get; set; }
        public long MonthNumber { get; set; }
    }
}
