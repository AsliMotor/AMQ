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
    [Subject("update invoice to cash")]
    public class when_update_invoice_to_cash
    {
        static Guid invoiceId;
        static IInvoiceService _service;
        Establish context = () =>
        {
            TestContext.InitBus();
            invoiceId = new Guid("3f9acca2-5b64-41f4-9a3c-9133867ebf59");
            _service = ContextRegistry.GetContext().GetObject("InvoiceService") as IInvoiceService;
        };
        Because of = () =>
        {
            _service.UpdateToCash(new UpdateToCashCommand
            {
                BranchId = "dny@gmail.com",
                InvoiceId = invoiceId
            }, "dny");
        };
        It should_be_invoice_created = () =>
        {
        };
    }
}
