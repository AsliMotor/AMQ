using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using BonaStoco.Inf.Reporting;

namespace AsliMotor.SuratPeringatan.AutoNumberGenerator
{
    public class SuratPeringatanAutoNumberGenerator : ISuratPeringatanAutoNumberGenerator
    {
        const string DEFAULT_PREFIX = "SPM-AM6";
        DateTime transactionDate;
        string branchId;
        QueryObjectMapper _qryObjectMapper;
        IReportingRepository _reportingRepository;
        public SuratPeringatanAutoNumberGenerator()
        {
            _qryObjectMapper = ContextRegistry.GetContext().GetObject("QueryObjectMapper") as QueryObjectMapper;
            _reportingRepository = ContextRegistry.GetContext().GetObject("ReportingRepository") as IReportingRepository;
        }

        public void SetupSuratPeringatanAutoMumber(int mode, string prefix, string branchId)
        {
            this.branchId = branchId;
            var config = GetSuratPeringatanAutoNumberConfig();
            config.SetupAutoNumber(mode, prefix);
            _reportingRepository.Update<SuratPeringatanAutoNumberConfig>(config, new { id = config.id });
        }

        public string GenerateSuratPeringatanNumber(DateTime transactionDate, string branchId)
        {
            this.transactionDate = transactionDate;
            this.branchId = branchId;
            SuratPeringatanAutoNumberConfig cfg = GetSuratPeringatanAutoNumberConfig();

            switch (cfg.Mode)
            {
                case AutoNumberMode.YEARLYMODE:
                    {
                        SuratPeringatanAutoNumberYearly autoNumberYearly = GetSuratPeringatanAutoNumberYearly();
                        if (autoNumberYearly == null)
                        {
                            autoNumberYearly = CreateSuratPeringatanAutoNumberYearly().Next();
                            _reportingRepository.Save<SuratPeringatanAutoNumberYearly>(autoNumberYearly);
                        }
                        else
                        {
                            _reportingRepository.Update<SuratPeringatanAutoNumberYearly>(autoNumberYearly.Next(), new { id = autoNumberYearly.id });
                        }
                        return autoNumberYearly.SuratPeringatanNumberInStringFormat(cfg.Prefix);
                    }
                default:
                    {
                        SuratPeringatanAutoNumberMonthly autoNumberMonthly = GetSuratPeringatanAutoNumberMonthly();
                        if (autoNumberMonthly == null)
                        {
                            autoNumberMonthly = CreateSuratPeringatanAutoNumberMonthly().Next();
                            _reportingRepository.Save<SuratPeringatanAutoNumberMonthly>(autoNumberMonthly);
                        }
                        else
                        {
                            _reportingRepository.Update<SuratPeringatanAutoNumberMonthly>(autoNumberMonthly.Next(), new { id = autoNumberMonthly.id });
                        }
                        return autoNumberMonthly.SuratPeringatanNumberInStringFormat(cfg.Prefix);
                    }
            }
        }
        private SuratPeringatanAutoNumberConfig GetSuratPeringatanAutoNumberConfig()
        {
            SuratPeringatanAutoNumberConfig cfg = _qryObjectMapper.Map<SuratPeringatanAutoNumberConfig>("findByIdAndBranchId", 
                new string[] { "id", "branchid" }, 
                new object[] { branchId.AutoNumberConfigId(), branchId }).FirstOrDefault();
            if (cfg == null)
            {
                cfg = new SuratPeringatanAutoNumberConfig()
                {
                    id = branchId.AutoNumberConfigId(),
                    Mode = AutoNumberMode.MONTHLYMODE,
                    Prefix = DEFAULT_PREFIX,
                    BranchId = branchId
                };
                _reportingRepository.Save<SuratPeringatanAutoNumberConfig>(cfg);
            }
            return cfg;
        }
        private SuratPeringatanAutoNumberMonthly CreateSuratPeringatanAutoNumberMonthly()
        {
            SuratPeringatanAutoNumberMonthly siAutoNumber = new SuratPeringatanAutoNumberMonthly()
            {
                id = transactionDate.SuratPeringatanAutoNumberMonthlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year,
                Month = transactionDate.Month
            };
            return siAutoNumber;
        }
        private SuratPeringatanAutoNumberMonthly GetSuratPeringatanAutoNumberMonthly()
        {
            SuratPeringatanAutoNumberMonthly siAutoNumber = _qryObjectMapper.Map<SuratPeringatanAutoNumberMonthly>("findByIdAndBranchId", 
                new string[] { "id", "branchid" },
                new object[] { transactionDate.SuratPeringatanAutoNumberMonthlyId(branchId), branchId }).FirstOrDefault();
            return siAutoNumber;
        }
        private SuratPeringatanAutoNumberYearly CreateSuratPeringatanAutoNumberYearly()
        {
            SuratPeringatanAutoNumberYearly siAutoNumber = new SuratPeringatanAutoNumberYearly()
            {
                id = transactionDate.SuratPeringatanAutoNumberYearlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year
            };
            return siAutoNumber;
        }
        private SuratPeringatanAutoNumberYearly GetSuratPeringatanAutoNumberYearly()
        {
            SuratPeringatanAutoNumberYearly siAutoNumber = _qryObjectMapper.Map<SuratPeringatanAutoNumberYearly>("findByIdAndBranchId",
                new string[] { "id", "branchid" },
                new object[] { transactionDate.SuratPeringatanAutoNumberYearlyId(branchId), branchId }).FirstOrDefault();
            return siAutoNumber;
        }
    }
}
