using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Invoices.Command;
using AsliMotor.Invoices.Snapshots;
using AsliMotor.Invoices.Services;
using Spring.Context.Support;

namespace AsliMotor.Invoices.Fixture
{
    [Subject("bayar angsuran")]
    public class when_bayar_angsuran
    {
        static Guid invoiceId;
        static IInvoiceService _service;
        Establish context = () =>
        {
            TestContext.InitBus();
            invoiceId = new Guid("415e7d2a-9455-427e-97aa-3b4466abfbb7");
            _service = ContextRegistry.GetContext().GetObject("InvoiceService") as IInvoiceService;
        };
        Because of = () =>
        {
            _service.BayarAngsuran(invoiceId, DateTime.Now, 1, 300000M, "dny");
        };
        It should_be_invoice_created = () =>
        {
        };
    }
}
