using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using AsliMotor.Invoices.Services;
using Spring.Context.Support;

namespace AsliMotor.Invoices.Fixture
{
    public class TestContext
    {
        static Guid invoiceId = new Guid("a2a5c5af-724a-4f8a-9152-ee6ac3ea103c");
        static IBus _bus;
        public static IInvoiceService InvoiceService
        {
            get
            {
                InitBus();
                return ContextRegistry.GetContext().GetObject("InvoiceService") as IInvoiceService;
            }
        }
        public static Guid InvoiceId
        {
            get
            {
                return invoiceId;
            }
        }
        public static void InitBus()
        {
            _bus = Configure.With()
                        .Log4Net()
                        .DefineEndpointName("aslimotor")
                        .DefaultBuilder()
                        .BinarySerializer()
                        .MsmqTransport()
                            .IsTransactional(true)
                            .PurgeOnStartup(false)
                        .UnicastBus()
                            .LoadMessageHandlers()
                            .ImpersonateSender(true)
                        .CreateBus()
                        .Start();
        }
    }
}
