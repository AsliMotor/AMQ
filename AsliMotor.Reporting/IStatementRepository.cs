using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Reporting
{
    public interface IStatementRepository
    {
        IList<TransactionListing> GetStatement(string branchid, DateTime startdate, DateTime todate, string coaId);
    }
}
