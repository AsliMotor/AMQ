using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Customers;
using Spring.Context.Support;

namespace AsliMotor.Fixture.customers
{
    [Subject("update customer")]
    public class when_update_customer
    {
        static ICustomerService _service;
        Establish context = () =>
        {
            _service = ContextRegistry.GetContext().GetObject("CustomerService") as ICustomerService;
        };
        Because of = () =>
        {
            _service.Update(new Customer
            {
                id = new Guid("caae2d23-e352-4e23-a027-bb6dad17eea8"),
                Address = "Jln Soedirman No 1",
                BranchId = "dny@gmail.com",
                City = "Dabo Singkeep",
                Email = "deny@inforsys.co.id",
                Name = "Denny Wu",
                Outstanding = 0M,
                Phone = "0856658914",
                Region = "Lingga",
                KTPNo = "34589634087569",
                KTPPublisher = "Noordin",
                Gender = "Laki-laki",
                Job = "Wiraswasta",
                KTPDate = new DateTime(2009,9,29),
                Status = (int) StatusCustomer.AKTIF
            });
        };
        It should_be_customer_created = () =>
        {
        };
    }
}
