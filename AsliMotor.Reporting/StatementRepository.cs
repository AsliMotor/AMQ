using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;

namespace AsliMotor.Reporting
{
    public class StatementRepository:IStatementRepository
    {
        public QueryObjectMapper QueryObjectMapper { get; set; }
        public IList<TransactionListing> GetStatement(string branchid, DateTime startdate, DateTime todate, string coaId)
        {
            IList<TransactionListing> statement = QueryObjectMapper.Map<TransactionListing>("getTransactionListingByCoaKas",
                new string[] { "startdate", "todate", "branchid" },
                new object[] { startdate, todate, branchid }).ToList();
            return statement;
        }
    }
}
