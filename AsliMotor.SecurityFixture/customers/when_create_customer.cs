using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Customers;
using Spring.Context.Support;

namespace AsliMotor.Fixture.customers
{
    [Subject("create customer")]
    public class when_create_customer
    {
        static ICustomerService _service;
        Establish context = () =>
        {
            _service = ContextRegistry.GetContext().GetObject("CustomerService") as ICustomerService;
        };
        Because of = () =>
        {
            _service.Create(new Customer
            {
                id = Guid.NewGuid(),
                Address = "Jln Soedirman No 1",
                BranchId = "dny@gmail.com",
                City = "Dabo Singkep",
                Email = "denny@inforsys.co.id",
                Name = "Denny Wu",
                Outstanding = 0M,
                Phone = "0856658915",
                Status = (int) StatusCustomer.AKTIF
            });
        };
        It should_be_customer_created = () =>
        {
        };
    }
}
