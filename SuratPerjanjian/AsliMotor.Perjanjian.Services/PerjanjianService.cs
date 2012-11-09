using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Reporting;
using AsliMotor.Perjanjian.AutoNumberGenerator;

namespace AsliMotor.Perjanjian.Services
{
    public class PerjanjianService:IPerjanjianService
    {
        public IReportingRepository ReportingRepository { get; set; }
        public IPerjanjianAutoNumberGenerator PerjanjianAutoNumberGenerator { get; set; }
        public void CreateSuratPerjanjian(Guid invId, string branchId)
        {
            ReportingRepository.Save<SuratPerjanjian>(new SuratPerjanjian
            {
                InvoiceId = invId,
                SuratPerjanjianNo = PerjanjianAutoNumberGenerator.GeneratePerjanjianNumber(DateTime.Now, branchId),
                SuratPerjanjianDate = DateTime.Now
            });
        }
    }
}
