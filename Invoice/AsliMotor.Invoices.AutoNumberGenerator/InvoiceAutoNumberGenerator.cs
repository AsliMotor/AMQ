using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using Spring.Context.Support;
using BonaStoco.Inf.Reporting;

namespace AsliMotor.Invoices.AutoNumberGenerator
{
    public class InvoiceAutoNumberGenerator : IInvoiceAutoNumberGenerator
    {
        const string DEFAULT_PREFIX = "INV";
        DateTime transactionDate;
        string branchId;
        public QueryObjectMapper QueryObjectMapper { get; set; }
        public IReportingRepository ReportingRepository { get; set; }

        public void SetupInvoiceAutoMumber(int mode, string prefix, string branchId)
        {
            this.branchId = branchId;
            var config = GetInvoiceAutoNumberConfig();
            config.SetupAutoNumber(mode, prefix);
            ReportingRepository.Update<InvoiceAutoNumberConfig>(config, new { id = config.id });
        }

        public string GenerateInvoiceNumber(DateTime transactionDate, string branchId)
        {
            this.transactionDate = transactionDate;
            this.branchId = branchId;
            InvoiceAutoNumberConfig cfg = GetInvoiceAutoNumberConfig();

            switch (cfg.Mode)
            {
                case AutoNumberMode.YEARLYMODE:
                    {
                        InvoiceAutoNumberYearly autoNumberYearly = GetInvoiceAutoNumberYearly();
                        if (autoNumberYearly == null)
                        {
                            autoNumberYearly = CreateInvoiceAutoNumberYearly().Next();
                            ReportingRepository.Save<InvoiceAutoNumberYearly>(autoNumberYearly);
                        }
                        else
                        {
                            ReportingRepository.Update<InvoiceAutoNumberYearly>(autoNumberYearly.Next(), new { id = autoNumberYearly.id });
                        }
                        return autoNumberYearly.InvoiceNumberInStringFormat(cfg.Prefix);
                    }
                case AutoNumberMode.MONTHLYMODE:
                    {
                        InvoiceAutoNumberMonthly autoNumberMonthly = GetInvoiceAutoNumberMonthly();
                        if (autoNumberMonthly == null)
                        {
                            autoNumberMonthly = CreateInvoiceAutoNumberMonthly().Next();
                            ReportingRepository.Save<InvoiceAutoNumberMonthly>(autoNumberMonthly);
                        }
                        else
                        {
                            ReportingRepository.Update<InvoiceAutoNumberMonthly>(autoNumberMonthly.Next(), new { id = autoNumberMonthly.id });
                        }
                        return autoNumberMonthly.InvoiceNumberInStringFormat(cfg.Prefix);
                    }
                default:
                    {
                        InvoiceAutoNumberDefault autoNumberDefault = GetInvoiceAutoNumberDefault();
                        if (autoNumberDefault == null)
                        {
                            autoNumberDefault = CreateInvoiceAutoNumberDefault().Next();
                            ReportingRepository.Save<InvoiceAutoNumberDefault>(autoNumberDefault);
                        }
                        else
                        {
                            ReportingRepository.Update<InvoiceAutoNumberDefault>(autoNumberDefault.Next(), new { id = autoNumberDefault.id });
                        }
                        return autoNumberDefault.InvoiceNumberInStringFormat(cfg.Prefix);
                    }
            }
        }
        public InvoiceAutoNumberConfig GetInvoiceAutoNumberConfig(string id)
        {
            InvoiceAutoNumberConfig cfg = QueryObjectMapper.Map<InvoiceAutoNumberConfig>("findByIdAndBranchId",
                new string[] { "id", "branchid" },
                new object[] { id.AutoNumberConfigId(), id }).FirstOrDefault();
            return cfg;
        }
        private InvoiceAutoNumberConfig GetInvoiceAutoNumberConfig()
        {
            InvoiceAutoNumberConfig cfg = QueryObjectMapper.Map<InvoiceAutoNumberConfig>("findByIdAndBranchId", 
                new string[] { "id", "branchid" }, 
                new object[] { branchId.AutoNumberConfigId(), branchId }).FirstOrDefault();
            if (cfg == null)
            {
                cfg = new InvoiceAutoNumberConfig()
                {
                    id = branchId.AutoNumberConfigId(),
                    Mode = AutoNumberMode.DEFAULT,
                    Prefix = DEFAULT_PREFIX,
                    BranchId = branchId
                };
                ReportingRepository.Save<InvoiceAutoNumberConfig>(cfg);
            }
            return cfg;
        }
        private InvoiceAutoNumberMonthly CreateInvoiceAutoNumberMonthly()
        {
            InvoiceAutoNumberMonthly rcvAutoNumber = new InvoiceAutoNumberMonthly()
            {
                id = transactionDate.ReceiveAutoNumberMonthlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year,
                Month = transactionDate.Month
            };
            return rcvAutoNumber;
        }
        private InvoiceAutoNumberMonthly GetInvoiceAutoNumberMonthly()
        {
            InvoiceAutoNumberMonthly rcvAutoNumber = QueryObjectMapper.Map<InvoiceAutoNumberMonthly>("findByIdAndBranchId", 
                new string[] { "id", "branchid" },
                new object[] { transactionDate.ReceiveAutoNumberMonthlyId(branchId), branchId }).FirstOrDefault();
            return rcvAutoNumber;
        }
        private InvoiceAutoNumberYearly CreateInvoiceAutoNumberYearly()
        {
            InvoiceAutoNumberYearly rcvAutoNumber = new InvoiceAutoNumberYearly()
            {
                id = transactionDate.ReceiveAutoNumberYearlyId(branchId),
                BranchId = branchId,
                Year = transactionDate.Year
            };
            return rcvAutoNumber;
        }
        private InvoiceAutoNumberYearly GetInvoiceAutoNumberYearly()
        {
            InvoiceAutoNumberYearly rcvAutoNumber = QueryObjectMapper.Map<InvoiceAutoNumberYearly>("findByIdAndBranchId",
                new string[] { "id", "branchid" },
                new object[] { transactionDate.ReceiveAutoNumberYearlyId(branchId), branchId }).FirstOrDefault();
            return rcvAutoNumber;
        }

        private InvoiceAutoNumberDefault CreateInvoiceAutoNumberDefault()
        {
            InvoiceAutoNumberDefault rcvAutoNumber = new InvoiceAutoNumberDefault()
            {
                id = transactionDate.ReceiveAutoNumberYearlyId(branchId),
                BranchId = branchId,
                Value = 0
            };
            return rcvAutoNumber;
        }
        private InvoiceAutoNumberDefault GetInvoiceAutoNumberDefault()
        {
            InvoiceAutoNumberDefault rcvAutoNumber = QueryObjectMapper.Map<InvoiceAutoNumberDefault>("findByIdAndBranchId",
                new string[] { "id", "branchid" },
                new object[] { transactionDate.ReceiveAutoNumberYearlyId(branchId), branchId }).FirstOrDefault();
            return rcvAutoNumber;
        }
    }
}
