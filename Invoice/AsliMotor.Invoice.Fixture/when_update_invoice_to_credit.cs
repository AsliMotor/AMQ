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
    [Subject("update invoice to credit")]
    public class when_update_invoice_to_credit
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
            _service.UpdateToCredit(new UpdateToCreditCommand
            {
                BranchId = "dny@gmail.com",
                InvoiceId = invoiceId,
                DueDate = new DateTime(2012,10,29),
                LamaAngsuran = 24,
                SukuBunga = 25,
                UangMuka = 2000000M
            }, "dny");
        };
        It should_be_invoice_created = () =>
        {
        };
    }
}
