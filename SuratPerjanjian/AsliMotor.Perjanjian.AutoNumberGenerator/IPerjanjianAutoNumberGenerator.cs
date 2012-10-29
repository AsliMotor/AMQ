using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Perjanjian.AutoNumberGenerator
{
    public interface IPerjanjianAutoNumberGenerator
    {
        void SetupPerjanjianAutoMumber(int mode, string prefix, string branchId);
        string GeneratePerjanjianNumber(DateTime transactionDate, string branchId);
    }
}
