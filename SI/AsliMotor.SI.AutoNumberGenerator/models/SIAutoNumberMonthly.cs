using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SI.AutoNumberGenerator
{
    [NamedSqlQuery("findByIdAndBranchId", @"SELECT * FROM siautonumbermonthly WHERE id = @id AND branchid = @branchid")]
    public class SIAutoNumberMonthly:IViewModel
    {
        public string id { get; set; }
        public string BranchId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Value { get; set; }

        public SIAutoNumberMonthly Next()
        {
            Value++;
            return this;
        }
        public void Reset()
        {
            Value = 0;
        }
        public string SINumberInStringFormat(string prefix)
        {
            return string.Format("{0}/{1}/{2}/{3}",
                Value.ToString().PadLeft(5, '0'),
                Month.ToString().PadLeft(2, '0'),
                prefix,
                Year.ToString());
        }
    }
}
