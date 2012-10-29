using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Organizations
{
    [NamedSqlQuery("findById", @"SELECT * FROM logoorganization where id = @branchid")]
    public class LogoOrganization : IViewModel
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
    }
}
