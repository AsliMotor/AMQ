using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Invoices.Services;

namespace AsliMotor.Invoices.Fixture
{
    [Subject("cancel invoice")]
    public class when_cancel_invoice
    {
        Because of = () =>
        {
            TestContext.InvoiceService.Cancel(TestContext.InvoiceId, "dny");
        };
        It should_be_canceled_invoice = () =>
        {
        };
    }
}
