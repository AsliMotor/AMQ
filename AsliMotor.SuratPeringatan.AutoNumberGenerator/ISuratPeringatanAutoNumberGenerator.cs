using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.SuratPeringatan.AutoNumberGenerator
{
    public interface ISuratPeringatanAutoNumberGenerator
    {
        void SetupSuratPeringatanAutoMumber(int mode, string prefix, string branchId);
        string GenerateSuratPeringatanNumber(DateTime transactionDate, string branchId);
    }
}
