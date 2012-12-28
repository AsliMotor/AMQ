using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Spring.Context.Support;

namespace AsliMotor.ImportDataInvoice.Fixture
{
    [Subject("import data")]
    public class when_import_data
    {
        static ImportDataInvoiceService importDataInvoiceService;
        Establish context = () =>
        {
            TestContext.InitBus();
            importDataInvoiceService = ContextRegistry.GetContext().GetObject("ImportDataInvoiceService") as ImportDataInvoiceService;
        };
        Because of = () =>
        {
            string fileLocation = "C:\\Users\\Denny Wu\\Desktop\\Data AsliMotor\\data-aslimotor(2012).csv";
            importDataInvoiceService.Import("aslimotor@aslimotorsiq.com", "aslimotor@aslimotorsiq.com", fileLocation);
        };
        It should_imported = () =>
        {
        };
    }
}
