using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Customers;
using Spring.Context.Support;

namespace AsliMotor.Fixture.customers
{
    [Subject("change status customer")]
    public class when_change_status_customer
    {
        static ICustomerService _service;
        Establish context = () =>
        {
            _service = ContextRegistry.GetContext().GetObject("CustomerService") as ICustomerService;
        };
        Because of = () =>
        {
            _service.ChangeStatus(new Guid("caae2d23-e352-4e23-a027-bb6dad17eea8"), StatusCustomer.AKTIF);
        };
        It should_be_customer_created = () =>
        {
        };
    }
}
