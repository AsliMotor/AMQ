using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Products.Models
{
    [NamedSqlQuery("findByName", @"SELECT * FROM typeproduct WHERE name = @name and branchid=@branchid")]
    [NamedSqlQuery("getAll", @"SELECT * FROM typeproduct WHERE branchid=@branchid")]
    [NamedSqlQuery("findByKey", @"select * from typeproduct
       where (LOWER(name) like @key)
       and branchid = @branchid
       limit 10")]
    public class TypeProduct : IViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BranchId { get; set; }
    }
}
