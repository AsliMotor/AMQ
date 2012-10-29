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
    [Subject("create invoice with status cash")]
    public class when_create_invoice_with_status_cash
    {
        static Guid invoiceId;
        static Guid custId;
        static Guid productId;
        static string username;
        static IInvoiceService _service;
        Establish context = () =>
        {
            TestContext.InitBus();
            invoiceId = Guid.NewGuid();
            custId = new Guid("caae2d23-e352-4e23-a027-bb6dad17eea8");
            productId = new Guid("aef8500b-b143-4e61-badc-128c637458ec");
            username = "dny";
            _service = ContextRegistry.GetContext().GetObject("InvoiceService") as IInvoiceService;
        };
        Because of = () =>
        {
            _service.Cash(new CashCommand
            {
                BranchId = "dny@gmail.com",
                CustomerId = custId,
                id = invoiceId,
                InvoiceDate = DateTime.Now,
                Price = 9000000M,
                ProductId = productId
            }, username);
        };
        It should_be_invoice_created = () =>
        {
        };
    }
}
