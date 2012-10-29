using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Receives.AutoNumberGenerator;
using Spring.Context.Support;

namespace AsliMotor.Receives.Fixture
{
    [Subject("Generate Receive number monthly")]
    public class when_generate_auto_number_monthly
    {
        static IReceiveAutoNumberGenerator gen;
        static string result;
        Establish context = () =>
        {
            gen = ContextRegistry.GetContext().GetObject("ReceiveAutoNumberGenerator") as IReceiveAutoNumberGenerator;
        };
        Because of = () =>
        {
            result = gen.GenerateReceiveNumber(new DateTime(2012, 10, 12), "dny@gmail.com");
        };
        It should_be_generate_si_number = () =>
        {
            result.ShouldEqual("00001/10/RCV-AM6/2012");
        };
    }
}
