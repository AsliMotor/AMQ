using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SuratPeringatan.AutoNumberGenerator
{
    [NamedSqlQuery("findByIdAndBranchId", @"SELECT * FROM suratperingatanautonumberyearly WHERE id = @id AND branchid = @branchid")]
    public class SuratPeringatanAutoNumberYearly:IViewModel
    {
        public string id { get; set; }
        public string BranchId { get; set; }
        public int Year { get; set; }
        public int Value { get; private set; }

        public SuratPeringatanAutoNumberYearly Next()
        {
            Value++;
            return this;
        }
        public void Reset()
        {
            Value = 0;
        }
        public string SuratPeringatanNumberInStringFormat(string prefix)
        {
            return string.Format("{0}/{1}/{2}",
                Value.ToString().PadLeft(5, '0'),
                prefix,
                Year.ToString());
        }
    }
}
