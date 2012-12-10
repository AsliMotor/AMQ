using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Organizations
{
    [NamedSqlQuery("findById",@"SELECT * FROM organization where branchid = @branchid")]
    public class Organization : IViewModel
    {
        public string BranchId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Telp { get; set; }
        public string Pimpinan { get; set; }
    }
}
