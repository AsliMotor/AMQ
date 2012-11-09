using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using AsliMotor.Invoices.Snapshots;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.Events
{
    [Serializable]
    public class InvoiceCreated : IMessage, IViewModel
    {
        public InvoiceSnapshot Payload { get; set; }
        public string Username { get; set; }
    }
}
