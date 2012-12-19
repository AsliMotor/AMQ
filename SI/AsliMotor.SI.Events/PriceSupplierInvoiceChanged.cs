using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using AsliMotor.SI.Repository;

namespace AsliMotor.SI.Events
{
    [Serializable]
    public class PriceSupplierInvoiceChanged : IMessage
    {
        public SupplierInvoice Payload { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get { return DateTime.Now; } }
        public decimal BeforeCharge { get; set; }
        public decimal BeforeHargaBeli { get; set; }
    }
}
