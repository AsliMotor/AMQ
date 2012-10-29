using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.SI.AutoNumberGenerator
{
    public interface ISIAutoNumberGenerator
    {
        void SetupSIAutoMumber(int mode, string prefix, string branchId);
        string GenerateSINumber(DateTime transactionDate, string branchId);
    }
}
