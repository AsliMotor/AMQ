using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Perjanjian.AutoNumberGenerator
{
    [NamedSqlQuery("findByIdAndBranchId", @"SELECT * FROM perjanjianautonumbermonthly WHERE id = @id AND branchid = @branchid")]
    public class PerjanjianAutoNumberMonthly : IViewModel
    {
        public string id { get; set; }
        public string BranchId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Value { get; set; }

        public PerjanjianAutoNumberMonthly Next()
        {
            Value++;
            return this;
        }
        public void Reset()
        {
            Value = 0;
        }
        public string PerjanjianNumberInStringFormat(string prefix)
        {
            return string.Format("{0}/{1}/{2}/{3}",
                Value.ToString().PadLeft(5, '0'),
                Month.ToString().PadLeft(2, '0'),
                prefix,
                Year.ToString());
        }
    }
}
