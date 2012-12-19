using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.AuditLog.Repository
{
    [NamedSqlQuery("getList", @"
    select id, 
    supplierinvoiceid, 
    username, 
    supplierinvoiceno, 
    type, 
    nopolisi, 
    (hargabeli + charge) as Total, 
    (beforehargabeli + beforecharge) as beforetotal, 
    datetime from supplierinvoicelog where action = 3
    and branchid = @branchid
    order by datetime desc
    offset @offset
    limit 10
    ")]
    public class SupplierInvoicePriceChangedLog : IViewModel
    {
        public Guid id { get; set; }
        public Guid SupplierInvoiceId { get; set; }
        public string Username { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public string Type { get; set; }
        public string NoPolisi { get; set; }
        public decimal Total { get; set; }
        public decimal BeforeTotal { get; set; }
        public DateTime DateTime { get; set; }
        public long DateTimeTick
        {
            get
            {
                return (long)DateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            }
        }
    }
}
