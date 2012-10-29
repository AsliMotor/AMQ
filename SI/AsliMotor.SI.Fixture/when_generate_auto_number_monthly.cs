using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.SI.AutoNumberGenerator;

namespace AsliMotor.SI.Fixture
{
    [Subject("Generate SI number monthly")]
    public class when_generate_auto_number_monthly
    {
        static ISIAutoNumberGenerator gen;
        static string result;
        Establish context = () =>
        {
            gen = new SIAutoNumberGenerator();
        };
        Because of = () =>
        {
            result = gen.GenerateSINumber(new DateTime(2012, 10, 12), "dny@gmail.com");
        };
        It should_be_generate_si_number = () =>
        {
            result.ShouldEqual("00002/10/SI-AM6/2012");
        };
    }
}
