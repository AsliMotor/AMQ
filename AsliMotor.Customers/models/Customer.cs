using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Customers
{
    [NamedSqlQuery("findByEmail",@"SELECT * FROM customer where email = @email and branchid = @branchid")]
    [NamedSqlQuery("findByKTPNo", @"SELECT * FROM customer where ktpno = @ktpno and branchid = @branchid")]
    [NamedSqlQuery("findById", @"SELECT * FROM customer where id = @id")]
    public class Customer : IViewModel
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string KTPNo { get; set; }
        public DateTime KTPDate { get; set; }
        public string KTPPublisher { get; set; }
        public DateTime Birthday { get; set; }
        public string Job { get; set; }
        public string Gender { get; set; }
        public string BranchId { get; set; }
        public decimal Outstanding { get; set; }
        public decimal Deposit { get; set; }
        public int Status { get; set; }
    }
}
