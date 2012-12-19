using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.AuditLog.Repository
{
    public class InvoicePriceChangedLog
    {
        public Guid id { get; set; }
        public Guid InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string NoPolisi { get; set; }
        public string NoMesin { get; set; }
        public string NoRangka { get; set; }
        public decimal TotalKredit { get; set; }
        public int LamaAngsuran { get; set; }
        public decimal SukuBunga { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public int Action { get; set; }
    }
}
