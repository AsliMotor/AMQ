using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;

namespace AsliMotor.SalesReports.ReportRepository
{
    public class DashboardRepository:IDashboardRepository
    {
        public QueryObjectMapper QueryObjectMapper { get; set; }
        public TotalTransactionReport GetTotalTransaction(string branchid)
        {
            TotalTransactionReport report = QueryObjectMapper.Map<TotalTransactionReport>("findTotalTransaction", new string[] { "branchid" }, new object[] { branchid }).FirstOrDefault();
            return report;
        }
        public SalesReport GetSalesReport(string branchid)
        {
            SalesReport report = QueryObjectMapper.Map<SalesReport>("findSales", new string[] { "branchid" }, new object[] { branchid }).FirstOrDefault();
            return report;
        }
        public IList<PiutangTelahJatuhTempo> GetPiutangTelahJatuhTempo(string branchid, int offset)
        {
            IList<PiutangTelahJatuhTempo> results = QueryObjectMapper.Map<PiutangTelahJatuhTempo>("findPiutang", new string[] { "branchid", "offset" }, new object[] { branchid, offset * 20 }).ToList();
            return results;
        }
        public TotalPiutangTelahJatuhTempo GetTotalPiutangTelahJatuhTempo(string branchid)
        {
            TotalPiutangTelahJatuhTempo result = QueryObjectMapper.Map<TotalPiutangTelahJatuhTempo>("count", new string[] { "branchid" }, new object[] { branchid }).FirstOrDefault();
            return result;
        }
    }
}
