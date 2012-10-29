using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using BonaStoco.Inf.Reporting;

namespace AsliMotor.Receives.AutoNumberGenerator
{
    public class ReceiveAutoNumberGenerator : IReceiveAutoNumberGenerator
    {
        const string DEFAULT_PREFIX = "RCV-AM6";
        DateTime transactionDate;
        string branchId;
        public QueryObjectMapper QueryObjectMapper { get; set; }
        public IReportingRepository ReportingRepository { get; set; }

        public void SetupReceiveAutoMumber(int mode, string prefix, string branchId)
        {
            this.branchId = branchId;
            var config = GetReceiveAutoNumberConfig();
            config.SetupAutoNumber(mode, prefix);
            ReportingRepository.Update<ReceiveAutoNumberConfig>(config, new { id = config.id });
        }

        public string GenerateReceiveNumber(DateTime transactionDate, string branchId)
        {
            this.transactionDate = transactionDate;
            this.branchId = branchId;
            ReceiveAutoNumberConfig cfg = GetReceiveAutoNumberConfig();

            switch (cfg.Mode)
            {
                case AutoNumberMode.YEARLYMODE:
                    {
                        ReceiveAutoNumberYearly autoNumberYearly = GetReceiveAutoNumberYearly();
                        if (autoNumberYearly == null)
                        {
                            autoNumberYearly = CreateReceiveAutoNumberYearly().Next();
                            ReportingRepository.Save<ReceiveAutoNumberYearly>(autoNumberYearly);
                        }
                        else
                        {
                            ReportingRepository.Update<ReceiveAutoNumberYearly>(autoNumberYearly.Next(), new { id = autoNumberYearly.id });
                        }
                        return autoNumberYearly.ReceiveNumberInStringFormat(cfg.Prefix);
                    }
                default:
                    {
                        ReceiveAutoNumberMonthly autoNumberMonthly = GetReceiveAutoNumberMonthly();
                        if (autoNumberMonthly == null)
                        {
                            autoNumberMonthly = CreateReceiveAutoNumberMonthly().Next();
                            ReportingRepository.Save<ReceiveAutoNumberMonthly>(autoNumberMonthly);
                        }
                        else
                        {
                            ReportingRepository.Update<ReceiveAutoNumberMonthly>(autoNumberMonthly.Next(), new { id = autoNumberMonthly.id });
                        }
                        return autoNumberMonthly.ReceiveNumberInStringFormat(cfg.Prefix);
                    }
            }
        }
        private ReceiveAutoNumberConfig GetReceiveAutoNumberConfig()
        {
            ReceiveAutoNumberConfig cfg = QueryObjectMapper.Map<ReceiveAutoNumberConfig>("findByIdAndBranchId", 
                new string[] { "id", "branchid" }, 
                new object[] { branchId.AutoNumberConfigId(), branchId }).FirstOrDefault();
            if (cfg == null)
            {
                cfg = new ReceiveAutoNumberConfig()
                {
                    id = branchId.AutoNumberConfigId(),
                    Mode = AutoNumberMode.MONTHLYMODE,
                    Prefix = DEFAULT_PREFIX,
                    BranchId = branchId
                };
                ReportingRepository.Save<ReceiveAutoNumberConfig>(cfg);
            }
            return cfg;
        }
        private ReceiveAutoNumberMonthly CreateReceiveAutoNumberMonthly()
        {
            ReceiveAutoNumberMonthly rcvAutoNumber = new ReceiveAutoNumberMonthly()
            {
                id = transactionDate.ReceiveAutoNumberMonthlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year,
                Month = transactionDate.Month
            };
            return rcvAutoNumber;
        }
        private ReceiveAutoNumberMonthly GetReceiveAutoNumberMonthly()
        {
            ReceiveAutoNumberMonthly rcvAutoNumber = QueryObjectMapper.Map<ReceiveAutoNumberMonthly>("findByIdAndBranchId", 
                new string[] { "id", "branchid" },
                new object[] { transactionDate.ReceiveAutoNumberMonthlyId(branchId), branchId }).FirstOrDefault();
            return rcvAutoNumber;
        }
        private ReceiveAutoNumberYearly CreateReceiveAutoNumberYearly()
        {
            ReceiveAutoNumberYearly rcvAutoNumber = new ReceiveAutoNumberYearly()
            {
                id = transactionDate.ReceiveAutoNumberYearlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year
            };
            return rcvAutoNumber;
        }
        private ReceiveAutoNumberYearly GetReceiveAutoNumberYearly()
        {
            ReceiveAutoNumberYearly rcvAutoNumber = QueryObjectMapper.Map<ReceiveAutoNumberYearly>("findByIdAndBranchId",
                new string[] { "id", "branchid" },
                new object[] { transactionDate.ReceiveAutoNumberYearlyId(branchId), branchId }).FirstOrDefault();
            return rcvAutoNumber;
        }
    }
}
