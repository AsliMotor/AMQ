using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace AsliMotor.ImportDataInvoice.Fixture
{
    public class TestContext
    {
        static IBus _bus;
        public static IBus Bus
        {
            get
            {
                if (_bus == null)
                    InitBus();
                return _bus;
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
                        .MsmqSubscriptionStorage()
                        .UnicastBus()
                            .LoadMessageHandlers()
                            .ImpersonateSender(true)
                        .CreateBus()
                        .Start();
        }
    }
}
