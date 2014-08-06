using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Invoices.Snapshots;
using AsliMotor.PaymentTerms;

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
            int banyakCicilan = CalculateBanyakCicilan(p.LamaAngsuran, p.TermValue, p.TermType);
            decimal totalKredit = CalculateTotalKredit(p.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, p.Status, 0);
            var angsuranBulanan = CalculateAngsuranBulanan(p.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, 0, banyakCicilan);

            var dueDate = new DateTime();
            if (p.DueDate.Date == p.InvoiceDate.Date)
            {
                if (p.TermType.Equals(TermType.Day))
                    dueDate = p.DueDate.AddDays(p.TermValue);
                else if (p.TermType.Equals(TermType.Month))
                    dueDate = p.DueDate.AddMonths(p.TermValue);
                else
                    throw new Exception("Type termin pembayaran tidak terdefinisi");
            }
            else
                dueDate = p.DueDate;

            InvoiceSnapshot snapshot = new InvoiceSnapshot
            {
                BranchId = p.BranchId,
                CustomerId = p.CustomerId,
                id = p.id,
                InvoiceNo = p.InvoiceNo,
                InvoiceDate = p.InvoiceDate,
                Price = p.Price,
                ProductId = p.ProductId,
                TransactionDate = DateTime.Now,
                Status = (int)p.Status,
                TermId = p.TermId,
                TermType = (int)p.TermType,
                TermValue = p.TermValue,
                LamaAngsuran = p.LamaAngsuran,
                BanyakCicilan = banyakCicilan,
                SukuBunga = p.SukuBunga,
                DueDate = dueDate,
                StartDueDate = p.DueDate,
                AngsuranBulanan = angsuranBulanan,
                TotalKredit = totalKredit,
                Outstanding = angsuranBulanan * banyakCicilan
            };
            _snapshot = snapshot;
        }

        public Invoice(BookingParameter p)
        {
            InvoiceSnapshot snapshot = new InvoiceSnapshot
            {
                BranchId = p.BranchId,
                CustomerId = p.CustomerId,
                id = p.id,
                InvoiceNo = p.InvoiceNo,
                InvoiceDate = p.InvoiceDate,
                Price = p.Price,
                ProductId = p.ProductId,
                TransactionDate = DateTime.Now,
                Status = (int)p.Status,
                DueDate = p.DueDate,
                StartDueDate = p.DueDate,
                Outstanding = p.Price - p.UangTandaJadi
            };
            _snapshot = snapshot;
        }
        public Invoice(CashParameter p)
        {
            InvoiceSnapshot snapshot = new InvoiceSnapshot
            {
                BranchId = p.BranchId,
                CustomerId = p.CustomerId,
                id = p.id,
                InvoiceNo = p.InvoiceNo,
                InvoiceDate = p.InvoiceDate,
                Price = p.Price,
                ProductId = p.ProductId,
                TransactionDate = DateTime.Now,
                Status = (int)p.Status
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
            int banyakCicilan = CalculateBanyakCicilan(p.LamaAngsuran, p.TermValue, p.TermType);
            decimal totalKredit = CalculateTotalKredit(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, StatusInvoice.CREDIT, 0);
            _snapshot.TermId = p.TermId;
            _snapshot.TermType = (int)p.TermType;
            _snapshot.TermValue = p.TermValue;
            _snapshot.LamaAngsuran = p.LamaAngsuran;
            _snapshot.BanyakCicilan = banyakCicilan;
            _snapshot.SukuBunga = p.SukuBunga;
            _snapshot.DueDate = p.DueDate;
            _snapshot.StartDueDate = p.DueDate;
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, p.UangTandaJadi, _snapshot.BanyakCicilan);
            _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, p.UangMuka, p.LamaAngsuran, p.SukuBunga, StatusInvoice.CREDIT, p.UangTandaJadi);
            _snapshot.Outstanding = CalculateOutstanding(_snapshot);
        }

        public StatusInvoice BayarAngsuran(long totalAngsuran)
        {
            CalculateDueDate();
            _snapshot.Outstanding -= _snapshot.AngsuranBulanan;
            if (totalAngsuran == _snapshot.BanyakCicilan - 1)
            {
                _snapshot.Outstanding = 0;
                _snapshot.Status = (int)StatusInvoice.PAID;
            }
            return (StatusInvoice)_snapshot.Status;
        }

        public PelunasanCallback Pelunasan(DateTime date, long cicilanYangTelahDibayar)
        {
            DateTime dueDate = _snapshot.DueDate;
            decimal totalDenda = 0;
            if (dueDate < date)
            {
                int i = 1;
                while (dueDate < date && (cicilanYangTelahDibayar + i) <= _snapshot.BanyakCicilan)
                {
                    int hariTelat = 0;
                    TimeSpan ts = new TimeSpan();
                    ts = date.Subtract(dueDate);
                    hariTelat = ts.Days;
                    var denda = (_snapshot.AngsuranBulanan * decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["denda"])) * hariTelat;
                    totalDenda += denda;
                    dueDate = dueDate.AddDays(_snapshot.TermValue);
                    i++;
                }
            }
            decimal sisaTagihanPlusDenda = _snapshot.Outstanding + totalDenda;
            decimal diskon = (_snapshot.Outstanding * decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["discount"]));
            decimal totalYangHarusDibayar = sisaTagihanPlusDenda - diskon;

            _snapshot.Discount = diskon;
            _snapshot.Status = (int)StatusInvoice.PAID;
            _snapshot.Outstanding = 0M;
            return new PelunasanCallback
            {
                TotalYangHarusDiBayar = totalYangHarusDibayar,
                Denda = totalDenda
            };
        }

        public void ChangeUangMuka(decimal uangmuka, decimal uangtandajadi)
        {
            if (_snapshot.Status == (int)StatusInvoice.CREDIT)
            {
                _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, _snapshot.SukuBunga, uangtandajadi, _snapshot.BanyakCicilan);
                _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, _snapshot.SukuBunga, StatusInvoice.CREDIT, uangtandajadi);
                _snapshot.Outstanding = CalculateOutstanding(_snapshot);
            }
        }

        public void ChangeTerm(decimal uangmuka, decimal uangtandajadi, Guid termId, int newtermvalue, TermType termType)
        {
            int banyakCicilan = CalculateBanyakCicilan(_snapshot.LamaAngsuran, newtermvalue, termType);

            if (_snapshot.TermType.Equals((int)TermType.Day))
                _snapshot.DueDate = _snapshot.DueDate.AddDays(-_snapshot.TermValue);
            else if (_snapshot.TermType.Equals((int)TermType.Month))
                _snapshot.DueDate = _snapshot.DueDate.AddMonths(-_snapshot.TermValue);
            else
                throw new Exception("Type termin pembayaran tidak terdefinisi");

            _snapshot.TermId = termId;
            _snapshot.TermType = (int)termType;
            _snapshot.TermValue = newtermvalue;
            _snapshot.BanyakCicilan = banyakCicilan;
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, _snapshot.SukuBunga, uangtandajadi, banyakCicilan);
            CalculateDueDate();
        }

        public void ChangeHargaJual(decimal hargajual, decimal uangmuka, decimal debitNote)
        {
            if (_snapshot.Status == (int)StatusInvoice.CREDIT)
            {
                _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(hargajual, uangmuka, _snapshot.LamaAngsuran, _snapshot.SukuBunga, debitNote, _snapshot.BanyakCicilan);
                _snapshot.TotalKredit = CalculateTotalKredit(hargajual, uangmuka, _snapshot.LamaAngsuran, _snapshot.SukuBunga, StatusInvoice.CREDIT, debitNote);
                _snapshot.Outstanding = CalculateOutstanding(_snapshot);
                _snapshot.Price = hargajual;
            }
        }

        public void ChangeAngsuran(decimal angsuran, decimal uangmuka)
        {
            decimal total = _snapshot.BanyakCicilan * angsuran;
            _snapshot.TotalKredit = (total / (1 + ((_snapshot.SukuBunga / 100) * (_snapshot.LamaAngsuran / 12))) - BiayaAdministration);
            _snapshot.Price = (_snapshot.TotalKredit) + uangmuka;
            _snapshot.AngsuranBulanan = angsuran;
            _snapshot.Outstanding = CalculateOutstanding(_snapshot);
        }

        public void ChangeSukuBunga(decimal sukuBunga, decimal uangmuka, decimal uangtandajadi)
        {
            _snapshot.SukuBunga = sukuBunga;
            _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, sukuBunga, StatusInvoice.CREDIT, uangtandajadi);
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, uangmuka, _snapshot.LamaAngsuran, sukuBunga, uangtandajadi, _snapshot.BanyakCicilan);
            _snapshot.Outstanding = CalculateOutstanding(_snapshot);
        }

        public void ChangeLamaAngsuran(int lamaAngsuran, decimal uangmuka, decimal uangtandajadi)
        {
            _snapshot.LamaAngsuran = lamaAngsuran;
            _snapshot.BanyakCicilan = CalculateBanyakCicilan(lamaAngsuran, _snapshot.TermValue, (TermType) _snapshot.TermType);
            _snapshot.TotalKredit = CalculateTotalKredit(_snapshot.Price, uangmuka, lamaAngsuran, _snapshot.SukuBunga, StatusInvoice.CREDIT, uangtandajadi);
            _snapshot.AngsuranBulanan = CalculateAngsuranBulanan(_snapshot.Price, uangmuka, lamaAngsuran, _snapshot.SukuBunga, uangtandajadi, _snapshot.BanyakCicilan);
            _snapshot.Outstanding = CalculateOutstanding(_snapshot);
        }

        public void ChangeDueDate(DateTime duedate)
        {
            _snapshot.DueDate = duedate;
            _snapshot.StartDueDate = duedate;
        }

        public void ChangeInvoiceDate(DateTime duedate)
        {
            _snapshot.InvoiceDate = duedate;
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

        private decimal CalculateAngsuranBulanan(decimal price, decimal uangmuka, int lamaAngsuran, decimal sukuBunga, decimal uangtandajadi, int banyakCicilan)
        {
            decimal totalyangdikredit = (price - uangmuka - uangtandajadi + BiayaAdministration);
            decimal totalTahunAngsuran = decimal.Parse((lamaAngsuran / (double)12).ToString());
            decimal totalbunga = (totalyangdikredit * (sukuBunga / 100)) * totalTahunAngsuran;
            decimal angsuran = (lamaAngsuran == 0) ? 0 : (totalyangdikredit + totalbunga) / banyakCicilan;
            return Math.Round(angsuran / 1000) * 1000;
        }

        private decimal CalculateTotalKredit(decimal price, decimal uangmuka, int lamaAngsuran, decimal sukuBunga, StatusInvoice status, decimal uangtandajadi)
        {
            decimal totalyangdikredit = (price - uangmuka - uangtandajadi + BiayaAdministration);
            decimal totalTahunAngsuran = decimal.Parse((lamaAngsuran / (double)12).ToString());
            decimal totalbunga = (totalyangdikredit * (sukuBunga / 100)) * totalTahunAngsuran;
            return Math.Round(totalbunga + BiayaAdministration);
        }

        private decimal BiayaAdministration
        {
            get
            {
                decimal biayaAdministrasi = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["biayaadministrasi"]);
                return biayaAdministrasi;
            }
        }

        private decimal CalculateOutstanding(InvoiceSnapshot snap)
        {
            return snap.AngsuranBulanan * snap.LamaAngsuran;
        }

        private static int CalculateBanyakCicilan(int lamaAngsuran, int termValue, TermType termType)
        {

            int banyakCicilan = 0;
            if (termType.Equals(TermType.Day))
                banyakCicilan = (lamaAngsuran * 30) / termValue;
            else if (termType.Equals(TermType.Month))
                banyakCicilan = lamaAngsuran / termValue;
            else
                throw new Exception("Type termin pembayaran tidak terdefinisi");
            return banyakCicilan;
        }

        private void CalculateDueDate()
        {
            if (_snapshot.TermType.Equals((int)TermType.Day))
                _snapshot.DueDate = _snapshot.DueDate.AddDays(_snapshot.TermValue);
            else if (_snapshot.TermType.Equals((int)TermType.Month))
                _snapshot.DueDate = _snapshot.DueDate.AddMonths(_snapshot.TermValue);
            else
                throw new Exception("Type termin pembayaran tidak terdefinisi");
        }
    }
}
