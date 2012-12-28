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
            decimal totalKredit = CalculateTotalKredit(p.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, p.Status, 0);
            InvoiceSnapshot snapshot = new InvoiceSnapshot
            {
                BranchId = p.BranchId,
                CustomerId = p.CustomerId,
                id = p.id,
                InvoiceDate = p.InvoiceDate,
                Price = p.Price,
                ProductId = p.ProductId,
                TransactionDate = DateTime.Now,
                Status = (int)p.Status,
                LamaAngsuran = p.LamaAngsuran,
                SukuBunga = p.SukuBunga,
                DueDate = p.DueDate,
                StartDueDate = p.DueDate,
                AngsuranBulanan = CalculateAngsuranBulanan(p.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, 0),
                TotalKredit = totalKredit,
                Outstanding = (p.Status == StatusInvoice.PAID) ? 0 : (p.Price + totalKredit) - (p.UangMuka + p.UangTandaJadi)
            };
            _snapshot = snapshot;
        }

        public void UpdateToCash()
        {
            _snapshot.Status = (int)StatusInvoice.PAID;
            _snapshot.Outstanding = 0;
        }

        public void UpdateToCredit(UpdateToCreditParameter p)
        {
            _snapshot.Status = (int)StatusInvoice.CREDIT;
            _snapshot.LamaAngsuran = p.LamaAngsuran;
            _snapshot.SukuBunga = p.SukuBunga;
            _snapshot.DueDate = p.DueDate;
            _snapshot.StartDueDate = p.DueDate;
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, p.UangTandaJadi);
            _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, StatusInvoice.CREDIT, p.UangTandaJadi);
            _snapshot.Outstanding = (_snapshot.Price + _snapshot.TotalKredit) - (p.UangMuka + p.UangTandaJadi);
        }

        public StatusInvoice BayarAngsuran(long totalAngsuran)
        {
            _snapshot.DueDate = _snapshot.DueDate.AddDays(30);
            _snapshot.Outstanding -= _snapshot.AngsuranBulanan;
            if (totalAngsuran == _snapshot.LamaAngsuran - 1)
            {
                _snapshot.Outstanding = 0;
                _snapshot.Status = (int)StatusInvoice.PAID;
            }
            return (StatusInvoice)_snapshot.Status;
        }

        public void ChangeUangMuka(decimal uangmuka, decimal uangtandajadi)
        {
            if (_snapshot.Status == (int)StatusInvoice.CREDIT)
            {
                _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, _snapshot.SukuBunga, uangtandajadi);
                _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, _snapshot.SukuBunga, StatusInvoice.CREDIT, uangtandajadi);
                _snapshot.Outstanding = (_snapshot.Price + _snapshot.TotalKredit) - (uangmuka + uangtandajadi);
            }
        }

        public void ChangeAngsuran(decimal angsuran, decimal uangmuka)
        {
            decimal total = _snapshot.LamaAngsuran * angsuran;
            _snapshot.TotalKredit = (total / (1 + ((_snapshot.SukuBunga / 100) * (_snapshot.LamaAngsuran / 12))) - BiayaAdministration(_snapshot.LamaAngsuran));
            _snapshot.Price = (_snapshot.TotalKredit) + uangmuka;
            _snapshot.AngsuranBulanan = angsuran;
            _snapshot.Outstanding = (_snapshot.Price + _snapshot.TotalKredit) - (uangmuka);
        }

        public void ChangeSukuBunga(decimal sukuBunga, decimal uangmuka, decimal uangtandajadi)
        {
            _snapshot.SukuBunga = sukuBunga;
            _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, sukuBunga, StatusInvoice.CREDIT, uangtandajadi);
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, sukuBunga, uangtandajadi);
            _snapshot.Outstanding = (_snapshot.Price + _snapshot.TotalKredit) - (uangmuka + uangtandajadi);
        }

        public void ChangeLamaAngsuran(int lamaAngsuran, decimal uangmuka, decimal uangtandajadi)
        {
            _snapshot.LamaAngsuran = lamaAngsuran;
            _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, uangmuka, lamaAngsuran, _snapshot.SukuBunga, StatusInvoice.CREDIT, uangtandajadi);
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, uangmuka, lamaAngsuran, _snapshot.SukuBunga, uangtandajadi);
            _snapshot.Outstanding = (_snapshot.Price + _snapshot.TotalKredit) - (uangmuka + uangtandajadi);
        }

        public void ChangeDueDate(DateTime duedate)
        {
            _snapshot.DueDate = duedate;
            _snapshot.StartDueDate = duedate;
        }

        public void Cancel()
        {
            if (_snapshot.Status == (int)StatusInvoice.BOOKING)
            {
                _snapshot.Status = (int)StatusInvoice.CANCELED;
                _snapshot.Outstanding = 0;
            }
        }

        public void Pull()
        {
            if (_snapshot.Status == (int)StatusInvoice.CREDIT)
            {
                _snapshot.Status = (int)StatusInvoice.PULL;
                _snapshot.Outstanding = 0;
            }
        }

        public void ChangeProduct(Guid productId)
        {
            _snapshot.ProductId = productId;
        }

        public void ChangeCustomer(Guid customerId)
        {
            _snapshot.CustomerId = customerId;
        }

        public InvoiceSnapshot CreateSnapshot()
        {
            return _snapshot;
        }

        private decimal CalculateAngsuranBulanan(decimal price, decimal uangmuka, int lamaAngsuran, decimal sukuBunga, decimal uangtandajadi)
        {
            decimal totalyangdikredit = (price - uangmuka - uangtandajadi + BiayaAdministration(lamaAngsuran));
            decimal totalTahunAngsuran = decimal.Parse((lamaAngsuran / (double)12).ToString());
            decimal totalbunga = (totalyangdikredit * (sukuBunga / 100)) * totalTahunAngsuran;
            decimal angsuran = (lamaAngsuran == 0) ? 0 : (totalyangdikredit + totalbunga) / lamaAngsuran;
            return Math.Round(angsuran);
        }

        private decimal CalculateTotalKredit(decimal price, decimal uangmuka, int lamaAngsuran, decimal sukuBunga, StatusInvoice status, decimal uangtandajadi)
        {
            decimal totalyangdikredit = (price - uangmuka - uangtandajadi + BiayaAdministration(lamaAngsuran));
            decimal totalTahunAngsuran = decimal.Parse((lamaAngsuran / (double)12).ToString());
            decimal totalbunga = (totalyangdikredit * (sukuBunga / 100)) * totalTahunAngsuran;
            //decimal total = (status == StatusInvoice.CREDIT) ? (totalyangdikredit + totalbunga) : 0;
            return Math.Round(totalbunga + BiayaAdministration(lamaAngsuran));
        }

        private decimal BiayaAdministration(int lamaAngsuran)
        {
            //decimal totalTahunAngsuran = decimal.Parse((lamaAngsuran / (double)12).ToString());
            decimal biayaAdministrasi = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["biayaadministrasi"]);
            return biayaAdministrasi;
        }
    }
}
