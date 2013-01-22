using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper;

namespace AsliMotor.Invoices.ReportRepository
{
    public class InvoiceReportRepository:IInvoiceReportRepository
    {
        public IQueryObjectMapper QueryObjectMapper { get; set; }
        public InvoiceHeaderReport GetHeader(Guid invoiceId, string branchId)
        {
            InvoiceHeaderReport inv = QueryObjectMapper.Map<InvoiceHeaderReport>("findById", new string[] { "id", "branchid" }, new object[] { invoiceId, branchId }).FirstOrDefault();
            return inv;
        }

        public IList<InvoiceListViewReport> SearchListViewReport(string branchid, int offset, string key)
        {
            key = "%" + key.ToLower() + "%";
            IList<InvoiceListViewReport> results = QueryObjectMapper.Map<InvoiceListViewReport>("searchByKey", new string[] { "branchid", "key", "offset" }, new object[] { branchid, key, (offset * 10) }).ToList();
            return results;
        }

        public IList<InvoiceListViewReport> GetListViewReport(string branchid, int offset)
        {
            IList<InvoiceListViewReport> listView = QueryObjectMapper.Map<InvoiceListViewReport>("findListView", new string[] { "branchid", "offset" }, new object[] { branchid, (offset * 10) });
            return listView;
        }

        public InvoiceBookingReport GetInvoiceBookingReport(Guid invoiceId, string branchid)
        {
            InvoiceBookingReport inv = QueryObjectMapper.Map<InvoiceBookingReport>("findById", new string[] { "id", "branchid" }, new object[] { invoiceId, branchid }).FirstOrDefault();
            return inv;
        }

        public InvoiceCashReport GetInvoiceCashReport(Guid invoiceId, string branchid)
        {
            InvoiceCashReport inv = QueryObjectMapper.Map<InvoiceCashReport>("findById", new string[] { "id", "branchid" }, new object[] { invoiceId, branchid }).FirstOrDefault();
            return inv;
        }

        public ReceiveUangMukaReport GetReceiveUangMukaReport(Guid invoiceId, string branchid)
        {
            ReceiveUangMukaReport rcv = QueryObjectMapper.Map<ReceiveUangMukaReport>("findById", new string[] { "id", "branchid" }, new object[] { invoiceId, branchid }).FirstOrDefault();
            return rcv;
        }

        public ReceiveAngsuranReport GetReceiveAngsuranReport(Guid rcvId)
        {
            ReceiveAngsuranReport rcv = QueryObjectMapper.Map<ReceiveAngsuranReport>("findById", new string[] { "rcvid" }, new object[] { rcvId }).FirstOrDefault();
            return rcv;
        }

        public IList<InvoiceItemReport> GetItems(Guid invoiceId, string branchid)
        {
            IList<InvoiceItemReport> items = QueryObjectMapper.Map<InvoiceItemReport>("findById", new string[] { "id", "branchid" }, new object[] { invoiceId, branchid }).ToList();
            return items;
        }

        public TotalInvoice GetTotalListView(string branchId)
        {
            TotalInvoice total = QueryObjectMapper.Map<TotalInvoice>("count", new string[] { "branchid" }, new object[] { branchId }).FirstOrDefault();
            return total;
        }

        public SuratPeringatanReport GetSuratPeringatanReport(Guid invId, string branchid)
        {
            SuratPeringatanReport report = QueryObjectMapper.Map<SuratPeringatanReport>("findById", new string[] { "id", "branchid" }, new object[] { invId, branchid }).FirstOrDefault();
            return report;
        }

        public PernyataanKreditReport GetPernyataanKredit(Guid invId)
        {
            PernyataanKreditReport report = QueryObjectMapper.Map<PernyataanKreditReport>("findById", new string[] { "id" }, new object[] { invId }).FirstOrDefault();
            return report;
        }

        public SuratPernyataanReport GetSuratPernyataan(Guid invId)
        {
            SuratPernyataanReport report = QueryObjectMapper.Map<SuratPernyataanReport>("findById", new string[] { "id" }, new object[] { invId }).FirstOrDefault();
            return report;
        }

        public SuratPernyataanMampu GetSuratPernyataanMampu(Guid invId)
        {
            SuratPernyataanMampu report = QueryObjectMapper.Map<SuratPernyataanMampu>("findById", new string[] { "id" }, new object[] { invId }).FirstOrDefault();
            return report;
        }

        public SuratKuasaReport GetSuratKuasa(Guid invId)
        {
            SuratKuasaReport report = QueryObjectMapper.Map<SuratKuasaReport>("findById", new string[] { "id" }, new object[] { invId }).FirstOrDefault();
            return report;
        }

        public JBAngsuranReport GetJBAngsuran(Guid invId)
        {
            JBAngsuranReport report = QueryObjectMapper.Map<JBAngsuranReport>("findById", new string[] { "id" }, new object[] { invId }).FirstOrDefault();
            return report;
        }

        public JBFidusiaReport GetJBFidusia(Guid invId)
        {
            JBFidusiaReport report = QueryObjectMapper.Map<JBFidusiaReport>("findById", new string[] { "id" }, new object[] { invId }).FirstOrDefault();
            return report;
        }

        public SuratTandaTerima GetSuratTandaTerima(Guid invId)
        {
            SuratTandaTerima report = QueryObjectMapper.Map<SuratTandaTerima>("findById", new string[] { "id" }, new object[] { invId }).FirstOrDefault();
            return report;
        }

        public ReceivePelunasanReport GetReceivePelunasanReport(Guid invoiceId, string branchId)
        {
            ReceivePelunasanReport report = QueryObjectMapper.Map<ReceivePelunasanReport>("findById", new string[] { "id","branchid" }, new object[] { invoiceId, branchId }).FirstOrDefault();
            return report;
        }
    }
}
