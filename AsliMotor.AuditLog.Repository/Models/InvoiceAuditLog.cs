using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.AuditLog.Repository
{
    [NamedSqlQuery("getList", @"
    select 
    id,
    payload,
    datetime,
    invoiceid,
    action,
    username
    from invoicelog where (action = 3 or action = 5 or action = 6 or action = 7) and branchid = @branchid
    order by datetime desc
    offset @offset
    limit 10
    ")]
    public class InvoiceAuditLog : IViewModel
    {
        public Guid Id { get; set; }
        public string Payload { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public Guid InvoiceId { get; set; }
        public int Action { get; set; }
    }
}
