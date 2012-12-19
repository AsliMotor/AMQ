using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using BonaStoco.Inf.Reporting;

namespace AsliMotor.Perjanjian.AutoNumberGenerator
{
    public class PerjanjianAutoNumberGenerator : IPerjanjianAutoNumberGenerator
    {
        const string DEFAULT_PREFIX = "SP-AM6";
        DateTime transactionDate;
        string branchId;
        public QueryObjectMapper QueryObjectMapper { get; set; }
        public IReportingRepository ReportingRepository { get; set; }

        public void SetupPerjanjianAutoMumber(int mode, string prefix, string branchId)
        {
            this.branchId = branchId;
            var config = GetPerjanjianAutoNumberConfig();
            config.SetupAutoNumber(mode, prefix);
            ReportingRepository.Update<PerjanjianAutoNumberConfig>(config, new { id = config.id });
        }

        public string GeneratePerjanjianNumber(DateTime transactionDate, string branchId)
        {
            this.transactionDate = transactionDate;
            this.branchId = branchId;
            PerjanjianAutoNumberConfig cfg = GetPerjanjianAutoNumberConfig();

            switch (cfg.Mode)
            {
                case AutoNumberMode.YEARLYMODE:
                    {
                        PerjanjianAutoNumberYearly autoNumberYearly = GetPerjanjianAutoNumberYearly();
                        if (autoNumberYearly == null)
                        {
                            autoNumberYearly = CreatePerjanjianAutoNumberYearly().Next();
                            ReportingRepository.Save<PerjanjianAutoNumberYearly>(autoNumberYearly);
                        }
                        else
                        {
                            ReportingRepository.Update<PerjanjianAutoNumberYearly>(autoNumberYearly.Next(), new { id = autoNumberYearly.id });
                        }
                        return autoNumberYearly.PerjanjianNumberInStringFormat(cfg.Prefix);
                    }
                default:
                    {
                        PerjanjianAutoNumberMonthly autoNumberMonthly = GetPerjanjianAutoNumberMonthly();
                        if (autoNumberMonthly == null)
                        {
                            autoNumberMonthly = CreatePerjanjianAutoNumberMonthly().Next();
                            ReportingRepository.Save<PerjanjianAutoNumberMonthly>(autoNumberMonthly);
                        }
                        else
                        {
                            ReportingRepository.Update<PerjanjianAutoNumberMonthly>(autoNumberMonthly.Next(), new { id = autoNumberMonthly.id });
                        }
                        return autoNumberMonthly.PerjanjianNumberInStringFormat(cfg.Prefix);
                    }
            }
        }

        public PerjanjianAutoNumberConfig GetPerjanjianAutoNumberConfig(string id)
        {
            PerjanjianAutoNumberConfig cfg = QueryObjectMapper.Map<PerjanjianAutoNumberConfig>("findByIdAndBranchId",
                new string[] { "id", "branchid" },
                new object[] { id.AutoNumberConfigId(), id }).FirstOrDefault();
            return cfg;
        }

        private PerjanjianAutoNumberConfig GetPerjanjianAutoNumberConfig()
        {
            PerjanjianAutoNumberConfig cfg = QueryObjectMapper.Map<PerjanjianAutoNumberConfig>("findByIdAndBranchId", 
                new string[] { "id", "branchid" }, 
                new object[] { branchId.AutoNumberConfigId(), branchId }).FirstOrDefault();
            if (cfg == null)
            {
                cfg = new PerjanjianAutoNumberConfig()
                {
                    id = branchId.AutoNumberConfigId(),
                    Mode = AutoNumberMode.MONTHLYMODE,
                    Prefix = DEFAULT_PREFIX,
                    BranchId = branchId
                };
                ReportingRepository.Save<PerjanjianAutoNumberConfig>(cfg);
            }
            return cfg;
        }
        private PerjanjianAutoNumberMonthly CreatePerjanjianAutoNumberMonthly()
        {
            PerjanjianAutoNumberMonthly perjanjianAutoNumber = new PerjanjianAutoNumberMonthly()
            {
                id = transactionDate.PerjanjianAutoNumberMonthlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year,
                Month = transactionDate.Month
            };
            return perjanjianAutoNumber;
        }
        private PerjanjianAutoNumberMonthly GetPerjanjianAutoNumberMonthly()
        {
            PerjanjianAutoNumberMonthly perjanjianAutoNumber = QueryObjectMapper.Map<PerjanjianAutoNumberMonthly>("findByIdAndBranchId", 
                new string[] { "id", "branchid" },
                new object[] { transactionDate.PerjanjianAutoNumberMonthlyId(branchId), branchId }).FirstOrDefault();
            return perjanjianAutoNumber;
        }
        private PerjanjianAutoNumberYearly CreatePerjanjianAutoNumberYearly()
        {
            PerjanjianAutoNumberYearly perjanjianAutoNumber = new PerjanjianAutoNumberYearly()
            {
                id = transactionDate.PerjanjianAutoNumberYearlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year
            };
            return perjanjianAutoNumber;
        }
        private PerjanjianAutoNumberYearly GetPerjanjianAutoNumberYearly()
        {
            PerjanjianAutoNumberYearly perjanjianAutoNumber = QueryObjectMapper.Map<PerjanjianAutoNumberYearly>("findByIdAndBranchId",
                new string[] { "id", "branchid" },
                new object[] { transactionDate.PerjanjianAutoNumberYearlyId(branchId), branchId }).FirstOrDefault();
            return perjanjianAutoNumber;
        }
    }
}
