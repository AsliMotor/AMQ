using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Reporting;
using AsliMotor.Invoices.Domain;
using AsliMotor.Invoices.Snapshots;
using BonaStoco.Inf.DataMapper;

namespace AsliMotor.Invoices.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public IReportingRepository ReportingRepository { get; set; }
        public IQueryObjectMapper QueryObjectMapper { get; set; }
        public void Save(Invoice inv)
        {
            ReportingRepository.Save<InvoiceSnapshot>(inv.CreateSnapshot());
        }

        public void Update(Invoice inv)
        {
            ReportingRepository.Update<InvoiceSnapshot>(inv.CreateSnapshot(), new { id = inv.CreateSnapshot().id });
        }

        public Invoice Get(Guid id)
        {
            InvoiceSnapshot inv = QueryObjectMapper.Map<InvoiceSnapshot>("findById", new string[] { "id" }, new object[] { id }).FirstOrDefault();
            return new Invoice(inv);
        }

        public decimal GetUangTandaJadi(Guid id)
        {
            UangTandaJadi uangtandajadi = QueryObjectMapper.Map<UangTandaJadi>("findById", new string[] { "invid" }, new object[] { id }).FirstOrDefault();
            if (uangtandajadi == null)
                return 0;
            return uangtandajadi.Total;
        }

        public long CountAngsuranBulanan(Guid id)
        {
            CountAngsuranBulanan count = QueryObjectMapper.Map<CountAngsuranBulanan>("count", new string[] { "invid" }, new object[] { id }).FirstOrDefault();
            return count.Total;
        }

        public void Remove(Invoice inv)
        {
            ReportingRepository.Delete<InvoiceSnapshot>(new { id = inv.CreateSnapshot().id });
        }
    }
}
