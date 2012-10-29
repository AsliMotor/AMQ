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
    [Subject("create invoice with status kredit")]
    public class when_create_invoice_with_status_credit
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
            _service.Credit(new CreditCommand
            {
                BranchId = "dny@gmail.com",
                CustomerId = custId,
                id = invoiceId,
                InvoiceDate = DateTime.Now,
                Price = 9500000M,
                ProductId = productId,
                LamaAngsuran = 24,
                SukuBunga = 24,
                UangMuka = 2000000,
                DueDate = new DateTime(2012, 10, 29)
            }, username);
        };
        It should_be_invoice_created = () =>
        {
        };
    }
}
