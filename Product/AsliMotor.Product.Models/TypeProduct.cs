using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Products.Models
{
    [NamedSqlQuery("findByName", @"SELECT * FROM typeproduct WHERE name = @name and branchid=@branchid")]
    public class TypeProduct : IViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BranchId { get; set; }
    }
}
