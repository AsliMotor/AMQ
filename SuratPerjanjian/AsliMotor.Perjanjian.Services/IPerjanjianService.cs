using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Perjanjian.Services
{
    public interface IPerjanjianService
    {
        void CreateSuratPerjanjian(Guid invId, string branchId);
    }
}
