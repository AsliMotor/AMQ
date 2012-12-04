using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using BonaStoco.Inf.Data.ViewModel;
using AsliMotor.Invoices.Snapshots;

namespace AsliMotor.Invoices.Events
{
    [Serializable]
    public class UangAngsuranChanged : IMessage, IViewModel
    {
        public InvoiceSnapshot Payload { get; set; }
        public string Username { get; set; }
        public DateTime DateTime { get { return DateTime.Now; } }
    }
}
