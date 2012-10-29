using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.SI.AutoNumberGenerator;

namespace AsliMotor.SI.Fixture
{
    [Subject("Generate SI number yearly")]
    public class when_generate_auto_number_yearly
    {
        static ISIAutoNumberGenerator gen;
        static string result;
        Establish context = () =>
        {
            gen = new SIAutoNumberGenerator();
        };
        Because of = () =>
        {
            gen.SetupSIAutoMumber(AutoNumberMode.YEARLYMODE, "SI-AM6", "dny@gmail.com");
            result = gen.GenerateSINumber(new DateTime(2012, 10, 12), "dny@gmail.com");
        };
        It should_be_generate_si_number = () =>
        {
            result.ShouldEqual("0000001/SI-AM6/2012");
        };
    }
}
