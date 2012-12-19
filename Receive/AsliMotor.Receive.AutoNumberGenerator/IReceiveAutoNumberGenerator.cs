using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Receives.AutoNumberGenerator
{
    public interface IReceiveAutoNumberGenerator
    {
        void SetupReceiveAutoMumber(int mode, string prefix, string branchId);
        string GenerateReceiveNumber(DateTime transactionDate, string branchId);
        ReceiveAutoNumberConfig GetReceiveAutoNumberConfig(string id);
    }
}
