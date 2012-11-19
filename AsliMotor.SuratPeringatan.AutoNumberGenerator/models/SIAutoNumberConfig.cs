using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SuratPeringatan.AutoNumberGenerator
{
    [NamedSqlQuery("findByIdAndBranchId",@"SELECT * FROM suratperingatanautonumberconfig WHERE id = @id AND branchid = @branchid")]
    public class SuratPeringatanAutoNumberConfig:IViewModel
    {
        public string id { get; set; }
        public int Mode { get; set; }
        public string Prefix { get; set; }
        public string BranchId { get; set; }

        public void SetupAutoNumber(int mode, string prefix)
        {
            this.Mode = mode;
            this.Prefix = prefix;
        }
    }
}
