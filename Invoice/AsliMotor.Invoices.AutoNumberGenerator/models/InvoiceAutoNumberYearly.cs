using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.AutoNumberGenerator
{
    [NamedSqlQuery("findByIdAndBranchId", @"SELECT * FROM invoiceautonumberyearly WHERE id = @id AND branchid = @branchid")]
    public class InvoiceAutoNumberYearly : IViewModel
    {
        public string id { get; set; }
        public string BranchId { get; set; }
        public int Year { get; set; }
        public int Value { get; private set; }

        public InvoiceAutoNumberYearly Next()
        {
            Value++;
            return this;
        }
        public void Reset()
        {
            Value = 0;
        }
        public string InvoiceNumberInStringFormat(string prefix)
        {
            return string.Format("{0}/{1}/{2}",
                Value.ToString().PadLeft(5, '0'),
                prefix,
                Year.ToString());
        }
    }
}
