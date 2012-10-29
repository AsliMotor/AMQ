using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using AsliMotor.SI.Repository;

namespace AsliMotor.SI.Events
{
    [Serializable]
    public class SupplierInvoiceCreated : IMessage
    {
        public SupplierInvoice Payload { get; set; }
        public string UserName { get; set; }
    }
}
