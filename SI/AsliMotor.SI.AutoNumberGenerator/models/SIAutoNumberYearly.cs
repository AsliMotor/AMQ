using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SI.AutoNumberGenerator
{
    [NamedSqlQuery("findByIdAndBranchId", @"SELECT * FROM siautonumberyearly WHERE id = @id AND branchid = @branchid")]
    public class SIAutoNumberYearly:IViewModel
    {
        public string id { get; set; }
        public string BranchId { get; set; }
        public int Year { get; set; }
        public int Value { get; private set; }

        public SIAutoNumberYearly Next()
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
            return string.Format("{0}/{1}/{2}",
                Value.ToString().PadLeft(7, '0'),
                prefix,
                Year.ToString());
        }
    }
}
