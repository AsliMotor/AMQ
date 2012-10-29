using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using BonaStoco.Inf.Reporting;

namespace AsliMotor.SI.AutoNumberGenerator
{
    public class SIAutoNumberGenerator : ISIAutoNumberGenerator
    {
        const string DEFAULT_PREFIX = "SI-AM6";
        DateTime transactionDate;
        string branchId;
        QueryObjectMapper _qryObjectMapper;
        IReportingRepository _reportingRepository;
        public SIAutoNumberGenerator()
        {
            _qryObjectMapper = ContextRegistry.GetContext().GetObject("QueryObjectMapper") as QueryObjectMapper;
            _reportingRepository = ContextRegistry.GetContext().GetObject("ReportingRepository") as IReportingRepository;
        }

        public void SetupSIAutoMumber(int mode, string prefix, string branchId)
        {
            this.branchId = branchId;
            var config = GetSIAutoNumberConfig();
            config.SetupAutoNumber(mode, prefix);
            _reportingRepository.Update<SIAutoNumberConfig>(config, new { id = config.id });
        }

        public string GenerateSINumber(DateTime transactionDate, string branchId)
        {
            this.transactionDate = transactionDate;
            this.branchId = branchId;
            SIAutoNumberConfig cfg = GetSIAutoNumberConfig();

            switch (cfg.Mode)
            {
                case AutoNumberMode.YEARLYMODE:
                    {
                        SIAutoNumberYearly autoNumberYearly = GetSIAutoNumberYearly();
                        if (autoNumberYearly == null)
                        {
                            autoNumberYearly = CreateSIAutoNumberYearly().Next();
                            _reportingRepository.Save<SIAutoNumberYearly>(autoNumberYearly);
                        }
                        else
                        {
                            _reportingRepository.Update<SIAutoNumberYearly>(autoNumberYearly.Next(), new { id = autoNumberYearly.id });
                        }
                        return autoNumberYearly.SINumberInStringFormat(cfg.Prefix);
                    }
                default:
                    {
                        SIAutoNumberMonthly autoNumberMonthly = GetSIAutoNumberMonthly();
                        if (autoNumberMonthly == null)
                        {
                            autoNumberMonthly = CreateSIAutoNumberMonthly().Next();
                            _reportingRepository.Save<SIAutoNumberMonthly>(autoNumberMonthly);
                        }
                        else
                        {
                            _reportingRepository.Update<SIAutoNumberMonthly>(autoNumberMonthly.Next(), new { id = autoNumberMonthly.id });
                        }
                        return autoNumberMonthly.SINumberInStringFormat(cfg.Prefix);
                    }
            }
        }
        private SIAutoNumberConfig GetSIAutoNumberConfig()
        {
            SIAutoNumberConfig cfg = _qryObjectMapper.Map<SIAutoNumberConfig>("findByIdAndBranchId", 
                new string[] { "id", "branchid" }, 
                new object[] { branchId.AutoNumberConfigId(), branchId }).FirstOrDefault();
            if (cfg == null)
            {
                cfg = new SIAutoNumberConfig()
                {
                    id = branchId.AutoNumberConfigId(),
                    Mode = AutoNumberMode.MONTHLYMODE,
                    Prefix = DEFAULT_PREFIX,
                    BranchId = branchId
                };
                _reportingRepository.Save<SIAutoNumberConfig>(cfg);
            }
            return cfg;
        }
        private SIAutoNumberMonthly CreateSIAutoNumberMonthly()
        {
            SIAutoNumberMonthly siAutoNumber = new SIAutoNumberMonthly()
            {
                id = transactionDate.SIAutoNumberMonthlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year,
                Month = transactionDate.Month
            };
            return siAutoNumber;
        }
        private SIAutoNumberMonthly GetSIAutoNumberMonthly()
        {
            SIAutoNumberMonthly siAutoNumber = _qryObjectMapper.Map<SIAutoNumberMonthly>("findByIdAndBranchId", 
                new string[] { "id", "branchid" }, 
                new object[] { transactionDate.SIAutoNumberMonthlyId(branchId), branchId }).FirstOrDefault();
            return siAutoNumber;
        }
        private SIAutoNumberYearly CreateSIAutoNumberYearly()
        {
            SIAutoNumberYearly siAutoNumber = new SIAutoNumberYearly()
            {
                id = transactionDate.SIAutoNumberYearlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year
            };
            return siAutoNumber;
        }
        private SIAutoNumberYearly GetSIAutoNumberYearly()
        {
            SIAutoNumberYearly siAutoNumber = _qryObjectMapper.Map<SIAutoNumberYearly>("findByIdAndBranchId",
                new string[] { "id", "branchid" },
                new object[] { transactionDate.SIAutoNumberYearlyId(branchId), branchId }).FirstOrDefault();
            return siAutoNumber;
        }
    }
}
