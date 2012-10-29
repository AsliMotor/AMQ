using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Receives.Services;
using Spring.Context.Support;
using AsliMotor.Receives.Commands;

namespace AsliMotor.Receives.Fixture
{
    [Subject("Create Booking Receive")]
    public class when_create_booking_receive
    {
        static IReceiveService _service;
        static Guid invoiceId;
        Establish context = () =>
        {
            invoiceId = Guid.NewGuid();
            _service = ContextRegistry.GetContext().GetObject("ReceiveService") as IReceiveService;
        };
        Because of = () =>
        {
            _service.CreateBooking(new CreateBookingReceive
            {
                InvoiceId = invoiceId,
                BranchId = "dny@gmail.com",
                Total = 300000M
            });
        };
        It should_be_receive_created = () =>
        {
        };
    }
}
