using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Invoices.Snapshots;

namespace AsliMotor.Invoices.Domain
{
    public class Invoice
    {
        private InvoiceSnapshot _snapshot;
        public Invoice(InvoiceSnapshot snap)
        {
            _snapshot = snap;
        }
        public Invoice(CreateParameter p)
        {
            InvoiceSnapshot snapshot = new InvoiceSnapshot
            {
                BranchId = p.BranchId,
                CustomerId = p.CustomerId,
                id = p.id,
                InvoiceDate = p.InvoiceDate,
                Price = p .Price,
                ProductId = p.ProductId,
                TransactionDate = DateTime.Now,
                Status = (int) p.Status,
                LamaAngsuran = p.LamaAngsuran,
                SukuBunga = p.SukuBunga,
                Charge = BiayaAdministration(p.LamaAngsuran),
                DueDate = p.DueDate,
                AngsuranBulanan = CalculateAngsuranBulanan(p.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, 0),
                TotalKredit = CalculateTotalKredit(p.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, p.Status, 0)
            };
            _snapshot = snapshot;
        }

        public void UpdateToCash()
        {
            _snapshot.Status = (int)StatusInvoice.PAID;
        }

        public void UpdateToCredit(UpdateToCreditParameter p)
        {
            _snapshot.Status = (int)StatusInvoice.CREDIT;
            _snapshot.LamaAngsuran = p.LamaAngsuran;
            _snapshot.SukuBunga = p.SukuBunga;
            _snapshot.DueDate = p.DueDate.AddMonths(1);
            _snapshot.Charge = BiayaAdministration(p.LamaAngsuran);
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, p.UangTandaJadi);
            _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, StatusInvoice.CREDIT, p.UangTandaJadi);
        }

        public void BayarAngsuran()
        {
            _snapshot.DueDate = _snapshot.DueDate.AddMonths(1);
        }

        public void Cancel()
        {
            if (_snapshot.Status == (int)StatusInvoice.BOOKING)
                _snapshot.Status = (int)StatusInvoice.CANCELED;
        }

        public InvoiceSnapshot CreateSnapshot()
        {
            return _snapshot;
        }

        private decimal CalculateAngsuranBulanan(decimal price, decimal uangmuka, int lamaAngsuran, decimal sukuBunga, decimal uangtandajadi)
        {
            decimal totalyangdikredit = (price - uangmuka - uangtandajadi);
            int totalTahunAngsuran = lamaAngsuran / 12;
            decimal totalbunga = (totalyangdikredit * (sukuBunga / 100)) * totalTahunAngsuran;
            decimal angsuran = (lamaAngsuran == 0) ? 0 : (totalyangdikredit + totalbunga) / lamaAngsuran;
            return Math.Round(angsuran);
        }

        private decimal CalculateTotalKredit(decimal price, decimal uangmuka, int lamaAngsuran, decimal sukuBunga, StatusInvoice status, decimal uangtandajadi)
        {
            decimal totalyangdikredit = (price - uangmuka - uangtandajadi);
            int totalTahunAngsuran = lamaAngsuran / 12;
            decimal totalbunga = (totalyangdikredit * (sukuBunga / 100)) * totalTahunAngsuran;
            decimal total = (status == StatusInvoice.CREDIT) ? (totalyangdikredit + totalbunga) : 0;
            return Math.Round(total);
        }

        private decimal BiayaAdministration(int lamaAngsuran)
        {
            int totalTahun = lamaAngsuran / 12;
            decimal biayaAdministrasi = totalTahun * decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["biayaadministrasi"]);
            return biayaAdministrasi;
        }
    }
}
