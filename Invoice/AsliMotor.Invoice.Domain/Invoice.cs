﻿using System;
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
                AngsuranBulanan = CalculateAngsuranBulanan(p.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, 0),
                TotalKredit = totalKredit,
                Outstanding = (p.Price + totalKredit) - (p.UangMuka + p.UangTandaJadi)
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
            _snapshot.DueDate = p.DueDate.AddMonths(1);
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, p.UangTandaJadi);
            _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, StatusInvoice.CREDIT, p.UangTandaJadi);
            _snapshot.Outstanding = (_snapshot.Price + _snapshot.TotalKredit) - (p.UangMuka + p.UangTandaJadi);
        }

        public void BayarAngsuran()
        {
            _snapshot.DueDate = _snapshot.DueDate.AddMonths(1);
            _snapshot.Outstanding -= _snapshot.AngsuranBulanan;
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

        public void Cancel()
        {
            if (_snapshot.Status == (int)StatusInvoice.BOOKING)
            {
                _snapshot.Status = (int)StatusInvoice.CANCELED;
                _snapshot.Outstanding = 0;
            }
        }

        public InvoiceSnapshot CreateSnapshot()
        {
            return _snapshot;
        }

        private decimal CalculateAngsuranBulanan(decimal price, decimal uangmuka, int lamaAngsuran, decimal sukuBunga, decimal uangtandajadi)
        {
            decimal totalyangdikredit = (price - uangmuka - uangtandajadi + BiayaAdministration(lamaAngsuran));
            int totalTahunAngsuran = lamaAngsuran / 12;
            decimal totalbunga = (totalyangdikredit * (sukuBunga / 100)) * totalTahunAngsuran;
            decimal angsuran = (lamaAngsuran == 0) ? 0 : (totalyangdikredit + totalbunga) / lamaAngsuran;
            return Math.Round(angsuran);
        }

        private decimal CalculateTotalKredit(decimal price, decimal uangmuka, int lamaAngsuran, decimal sukuBunga, StatusInvoice status, decimal uangtandajadi)
        {
            decimal totalyangdikredit = (price - uangmuka - uangtandajadi + BiayaAdministration(lamaAngsuran));
            int totalTahunAngsuran = lamaAngsuran / 12;
            decimal totalbunga = (totalyangdikredit * (sukuBunga / 100)) * totalTahunAngsuran;
            //decimal total = (status == StatusInvoice.CREDIT) ? (totalyangdikredit + totalbunga) : 0;
            return Math.Round(totalbunga);
        }

        private decimal BiayaAdministration(int lamaAngsuran)
        {
            int totalTahun = lamaAngsuran / 12;
            decimal biayaAdministrasi = totalTahun * decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["biayaadministrasi"]);
            return biayaAdministrasi;
        }
    }
}
