using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Invoices.Command;
using BonaStoco.Inf.DataMapper;
using AsliMotor.Invoices.Domain;
using AsliMotor.Invoices.Snapshots;
using AsliMotor.Invoices.Repository;
using AsliMotor.Customers;
using AsliMotor.Products;
using AsliMotor.Products.Models;
using NServiceBus;
using AsliMotor.Receives.Services;
using AsliMotor.Receives.Commands;
using AsliMotor.Receives.Repository;
using AsliMotor.Receives.Models;
using AsliMotor.Perjanjian.Services;
using AsliMotor.Invoices.Events;
using AsliMotor.PaymentTerms;
using AsliMotor.Invoices.AutoNumberGenerator;

namespace AsliMotor.Invoices.Services
{
    public class InvoiceService : IInvoiceService
    {
        public IInvoiceAutoNumberGenerator InvoiceAutoNumberGenerator { get; set; }
        public IQueryObjectMapper QueryObjectMapper { get; set; }
        public IInvoiceRepository Repository { get; set; }
        public ICustomerService CustomerService { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IProductService ProductService { get; set; }
        public IReceiveService ReceiveService { get; set; }
        public IReceiveRepository ReceiveRepository { get; set; }
        public IPerjanjianService PerjanjianService { get; set; }
        public IPaymentTermRepository PaymentTermRepository { get; set; }
        IBus _bus;

        public InvoiceService()
        {
            _bus = NServiceBus.Configure.Instance.Builder.Build<IBus>();
        }

        public void Booking(BookingCommand cmd, string username)
        {
            FailIfCustomerNotFound(cmd.CustomerId);
            FailIfProductCantSale(cmd.ProductId, cmd.BranchId);
            if (cmd.DebitNote <= 0)
                throw new ApplicationException("Uang Tanda Jadi harus diisi");
            Invoice inv = new Invoice(new BookingParameter
            {
                BranchId = cmd.BranchId,
                CustomerId = cmd.CustomerId,
                id = cmd.id,
                InvoiceNo = InvoiceAutoNumberGenerator.GenerateInvoiceNumber(DateTime.Now, cmd.BranchId),
                InvoiceDate = cmd.InvoiceDate,
                DueDate = cmd.InvoiceDate,
                Price = cmd.Price,
                ProductId = cmd.ProductId,
                UangTandaJadi = cmd.DebitNote,
                Status = StatusInvoice.BOOKING
            });
            Repository.Save(inv);
            ProductService.ChangeStatus(cmd.ProductId, cmd.BranchId, StatusProduct.TERJUAL, username);
            CreateBookingReceive(inv, cmd.DebitNote);
            PublishInvoiceCreated(inv, username);
        }

        public void Cash(CashCommand cmd, string username)
        {
            FailIfCustomerNotFound(cmd.CustomerId);
            FailIfProductCantSale(cmd.ProductId, cmd.BranchId);
            Invoice inv = new Invoice(new CashParameter
            {
                BranchId = cmd.BranchId,
                CustomerId = cmd.CustomerId,
                id = cmd.id,
                InvoiceNo = InvoiceAutoNumberGenerator.GenerateInvoiceNumber(DateTime.Now, cmd.BranchId),
                InvoiceDate = cmd.InvoiceDate,
                Price = cmd.Price,
                ProductId = cmd.ProductId,
                Status = StatusInvoice.PAID
            });
            Repository.Save(inv);
            ProductService.ChangeStatus(cmd.ProductId, cmd.BranchId, StatusProduct.TERJUAL, username);
            CreateCashReceive(inv, inv.CreateSnapshot().Price);
            PublishInvoiceCreated(inv, username);
        }

        public void UpdateToCash(UpdateToCashCommand cmd, string username)
        {
            Invoice inv = Repository.Get(cmd.InvoiceId);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            if (invSnap.Status == (int)StatusInvoice.BOOKING)
            {
                inv.UpdateToCash();
                Repository.Update(inv);
                Receive bookingRcv = ReceiveRepository.GetBooking(invSnap.id);
                decimal outstanding = (invSnap.Price - bookingRcv.Total);
                CreateCashReceive(inv, outstanding);
                PublishPaymentTypeChanged(inv, username);
            }
        }

        public void Credit(CreditCommand cmd, string username)
        {
            FailIfCustomerNotFound(cmd.CustomerId);
            FailIfProductCantSale(cmd.ProductId, cmd.BranchId);
            PaymentTermReport term = PaymentTermRepository.GetById(cmd.TermId);
            Invoice inv = new Invoice(new CreateParameter
            {
                BranchId = cmd.BranchId,
                CustomerId = cmd.CustomerId,
                id = cmd.id,
                InvoiceNo = InvoiceAutoNumberGenerator.GenerateInvoiceNumber(DateTime.Now, cmd.BranchId),
                InvoiceDate = cmd.InvoiceDate,
                ProductId = cmd.ProductId,
                Status = StatusInvoice.CREDIT,
                Price = cmd.Price,
                UangMuka = cmd.UangMuka,
                SukuBunga = cmd.SukuBunga,
                LamaAngsuran = cmd.LamaAngsuran,
                DueDate = cmd.DueDate,
                TermId = term.id,
                TermValue = term.Value,
                TermType = term.Type
            });
            Repository.Save(inv);
            ProductService.ChangeStatus(cmd.ProductId, cmd.BranchId, StatusProduct.TERJUAL, username);
            CreateUangMukaReceive(inv, cmd.UangMuka);
            PerjanjianService.CreateSuratPerjanjian(inv.CreateSnapshot().id, inv.CreateSnapshot().BranchId, inv.CreateSnapshot().InvoiceDate);
            PublishInvoiceCreated(inv, username);
        }

        public void UpdateToCredit(UpdateToCreditCommand cmd, string username)
        {
            Invoice inv = Repository.Get(cmd.InvoiceId);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            if (invSnap.Status == (int)StatusInvoice.BOOKING)
            {
                Receive bookingRcv = ReceiveRepository.GetBooking(invSnap.id);
                PaymentTermReport term = PaymentTermRepository.GetById(cmd.TermId);
                inv.UpdateToCredit(new UpdateToCreditParameter
                {
                    DueDate = cmd.DueDate,
                    LamaAngsuran = cmd.LamaAngsuran,
                    SukuBunga = cmd.SukuBunga,
                    UangMuka = cmd.UangMuka,
                    UangTandaJadi = bookingRcv.Total,
                    TermId = cmd.TermId,
                    TermValue = term.Value,
                    TermType = term.Type
                });
                Repository.Update(inv);
                CreateUangMukaReceive(inv, cmd.UangMuka);
                PerjanjianService.CreateSuratPerjanjian(inv.CreateSnapshot().id, inv.CreateSnapshot().BranchId, DateTime.Now);
                PublishPaymentTypeChanged(inv, username);
            }
        }

        public void BayarAngsuran(Guid id, DateTime date, int totalBulanYangDiBayar, decimal payAmount, string username)
        {
            Invoice invoice = Repository.Get(id);
            Customer cust = CustomerRepository.GetById(invoice.CreateSnapshot().CustomerId);
            decimal creditNoteOld = cust.Deposit;
            decimal creditNoteNew = payAmount + cust.Deposit;
            Guid custId = invoice.CreateSnapshot().CustomerId;
            for (var i = 0; i < totalBulanYangDiBayar; i++)
            {
                Invoice inv = Repository.Get(id);
                InvoiceSnapshot invSnap = inv.CreateSnapshot();
                FailIfInvoiceNotFound(invSnap);
                if (invSnap.Status == (int)StatusInvoice.CREDIT)
                {
                    DateTime dueDate = invSnap.DueDate;
                    int hariTelat = 0;
                    if (date > dueDate)
                    {
                        TimeSpan ts = new TimeSpan();
                        ts = date.Subtract(dueDate);
                        hariTelat = ts.Days;
                    }
                    decimal denda = (invSnap.AngsuranBulanan * decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["denda"])) * hariTelat;
                    long totalAngsuran = Repository.CountAngsuranBulanan(invSnap.id);
                    payAmount = payAmount - (denda + invSnap.AngsuranBulanan);
                    creditNoteNew -= (denda + invSnap.AngsuranBulanan);
                    if ((payAmount + creditNoteOld) < 0)
                    {
                        throw new ApplicationException("Uang Anda Tidak Cukup Untuk Membayar Angsuran ke-" + (totalAngsuran + 1));
                    }
                    StatusInvoice status = inv.BayarAngsuran(totalAngsuran);
                    Repository.Update(inv);

                    if (i == (totalBulanYangDiBayar - 1))
                    {
                        CreateAngsuranReceive(inv, date, denda, totalAngsuran, payAmount);
                    }
                    else
                    {
                        CreateAngsuranReceive(inv, date, denda, totalAngsuran, 0);
                    }

                    PublishAngsuranPaid(inv, username);
                    if (status == StatusInvoice.PAID)
                        ProductService.ChangeStatus(invSnap.ProductId, invSnap.BranchId, StatusProduct.TERJUAL_LUNAS, username);
                }
            }
            UpdateCreditNoteCustomer(custId, creditNoteNew);
        }

        public void Pelunasan(Guid id, DateTime date, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            long cicilanYangTelahDibayar = Repository.CountAngsuranBulanan(invSnap.id);
            decimal uangmuka = ReceiveRepository.GetByInvoiceIdAndPaymentType(invSnap.id, 1).Total;
            PelunasanCallback callback = inv.Pelunasan(date, cicilanYangTelahDibayar, uangmuka);
            Repository.Update(inv);
            CreatePelunasanReceive(invSnap, callback.TotalYangHarusDiBayar, callback.Denda, (cicilanYangTelahDibayar + 1));
            if (inv.CreateSnapshot().Status == (int)StatusInvoice.PAID)
                ProductService.ChangeStatus(invSnap.ProductId, invSnap.BranchId, StatusProduct.TERJUAL_LUNAS, username);
        }

        public void ChangeUangMuka(Guid id, decimal uangmuka, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            decimal debitnote = Repository.GetUangTandaJadi(id);
            inv.ChangeUangMuka(uangmuka, debitnote);
            Repository.Update(inv);
            UpdateUangMukaReceive(inv, uangmuka);
            PublishUangMukaChanged(inv, username);
        }

        public void ChangeHargaJual(Guid id, decimal hargaJual, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            decimal debitnote = Repository.GetUangTandaJadi(id);
            Receive uangMukaRcv = ReceiveRepository.GetByInvoiceIdAndPaymentType(invSnap.id, 1);
            inv.ChangeHargaJual(hargaJual, uangMukaRcv.Total, debitnote);
            Repository.Update(inv);
            PublishHargaJualChanged(inv, username);
        }

        public void UpdateUangAngsuran(Guid id, decimal angsuran, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            decimal debitnote = Repository.GetUangTandaJadi(id);
            Receive uangMukaRcv = ReceiveRepository.GetByInvoiceIdAndPaymentType(invSnap.id, 1);
            inv.ChangeAngsuran(angsuran, (debitnote + uangMukaRcv.Total));
            Repository.Update(inv);
            PublishUangAngsuranChanged(inv, username);
        }

        public void ChangeSukuBunga(Guid id, decimal sukubunga, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            decimal debitnote = Repository.GetUangTandaJadi(id);
            decimal uangmuka = ReceiveRepository.GetByInvoiceIdAndPaymentType(invSnap.id, 1).Total;
            inv.ChangeSukuBunga(sukubunga, uangmuka, debitnote);
            Repository.Update(inv);
            PublishSukuBungaChanged(inv, username);
        }

        public void ChangeLamaAngsuran(Guid id, int lamaAngsuran, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            decimal debitnote = Repository.GetUangTandaJadi(id);
            decimal uangmuka = ReceiveRepository.GetByInvoiceIdAndPaymentType(invSnap.id, 1).Total;
            inv.ChangeLamaAngsuran(lamaAngsuran, uangmuka, debitnote);
            Repository.Update(inv);
            PublishLamaAngsuranChanged(inv, username);
        }

        public void ChangeDueDate(Guid id, DateTime dueDate, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfInvoiceIsNotCredit(invSnap);
            inv.ChangeDueDate(dueDate);
            Repository.Update(inv);
            PublishDueDateChanged(inv, username);
        }

        public void ChangeInvoiceDate(Guid id, DateTime invoiceDate, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            inv.ChangeInvoiceDate(invoiceDate);
            Repository.Update(inv);
            PublishInvoiceDateChanged(inv, username);
        }

        public void Cancel(Guid id, string username)
        {
            Invoice inv = Repository.Get(id);
            inv.Cancel();
            Repository.Update(inv);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            ProductService.ChangeStatus(invSnap.ProductId, invSnap.BranchId, StatusProduct.AKTIF, username);
            PublishInvoiceCanceled(inv, username);
        }

        public void Pull(Guid id, string username)
        {
            Invoice inv = Repository.Get(id);
            inv.Pull();
            Repository.Update(inv);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            ProductService.ChangeStatus(invSnap.ProductId, invSnap.BranchId, StatusProduct.AKTIF, username);
            PublishInvoicePulled(inv, username);
        }

        public void ChangeProduct(Guid id, Guid productId, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            FailIfProductCantSale(productId, invSnap.BranchId);
            Guid oldProductId = invSnap.ProductId;
            inv.ChangeProduct(productId);
            Repository.Update(inv);
            ProductService.ChangeStatus(oldProductId, invSnap.BranchId, StatusProduct.AKTIF, username);
            ProductService.ChangeStatus(productId, invSnap.BranchId, StatusProduct.TERJUAL, username);
            PublishProductChanged(inv, username);
        }

        public void ChangeCustomer(Guid id, Guid custId, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            FailIfCustomerNotFound(custId);
            inv.ChangeCustomer(custId);
            Repository.Update(inv);
            PublishCustomerChanged(inv, username);
        }

        public void ChangeTerm(Guid id, Guid termId, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            PaymentTermReport newTerm = PaymentTermRepository.GetById(termId);
            decimal debitnote = Repository.GetUangTandaJadi(id);
            decimal uangmuka = ReceiveRepository.GetByInvoiceIdAndPaymentType(invSnap.id, 1).Total;
            inv.ChangeTerm(uangmuka, debitnote, newTerm.id, newTerm.Value, newTerm.Type);
            Repository.Update(inv);
            PublishTermChanged(inv, username);
        }

        public void Remove(Guid id, string username)
        {
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            FailIfCantChange(invSnap);
            Repository.Remove(inv);
            ProductService.ChangeStatus(invSnap.ProductId, invSnap.BranchId, StatusProduct.AKTIF, username);
        }

        private void UpdateCreditNoteCustomer(Guid custId, decimal creditNote)
        {
            CustomerService.UpdateCreditNote(custId, creditNote);
        }

        #region create receive

        private void CreateAngsuranReceive(Invoice inv, DateTime date, decimal denda, long totalangsuran, decimal creditNote)
        {
            InvoiceSnapshot invSnapshot = inv.CreateSnapshot();
            var perbayaranBulanKe = (Convert.ToInt32(totalangsuran) + 1) * invSnapshot.TermValue;
            string bulanAngsuran = string.Empty;
            if (invSnapshot.TermType.Equals((int)TermType.Day))
                bulanAngsuran = invSnapshot.InvoiceDate.AddDays(perbayaranBulanKe).ToString("MMyyyy");
            else if (invSnapshot.TermType.Equals((int)TermType.Month))
                bulanAngsuran = invSnapshot.InvoiceDate.AddMonths(perbayaranBulanKe).ToString("MMyyyy");
            else
                throw new Exception("Type termin pembayaran tidak terdefinisi");

            ReceiveService.CreateAngsuran(new CreateAngsuranReceive
            {
                BranchId = invSnapshot.BranchId,
                Denda = Math.Round(denda),
                Total = Math.Round(invSnapshot.AngsuranBulanan + denda),
                InvoiceId = invSnapshot.id,
                BulanAngsuran = bulanAngsuran,
                PaymentDate = date,
                BulanAngsuranNumber = totalangsuran + 1,
                CreditNote = creditNote
            });
        }

        private void CreateBookingReceive(Invoice inv, decimal debitnote)
        {
            InvoiceSnapshot invSnapshot = inv.CreateSnapshot();
            if (invSnapshot.Status == (int)StatusInvoice.BOOKING && debitnote > 0)
            {
                ReceiveService.CreateBooking(new CreateBookingReceive
                {
                    InvoiceId = invSnapshot.id,
                    BranchId = invSnapshot.BranchId,
                    Total = debitnote
                });
            }
        }

        private void CreateCashReceive(Invoice inv, decimal total)
        {
            InvoiceSnapshot invSnapshot = inv.CreateSnapshot();
            ReceiveService.CreateCash(new CreateCashReceive
            {
                InvoiceId = invSnapshot.id,
                BranchId = invSnapshot.BranchId,
                Total = total
            });
        }

        private void UpdateUangMukaReceive(Invoice inv, decimal totalUangMuka)
        {
            InvoiceSnapshot invSnapshot = inv.CreateSnapshot();
            ReceiveService.ChangeUangMuka(invSnapshot.id, totalUangMuka);
        }

        private void CreateUangMukaReceive(Invoice inv, decimal totalUangMuka)
        {
            InvoiceSnapshot invSnapshot = inv.CreateSnapshot();
            ReceiveService.CreateUangMuka(new CreateUangMukaReceive
            {
                InvoiceId = invSnapshot.id,
                BranchId = invSnapshot.BranchId,
                Total = totalUangMuka
            });
        }

        private void CreatePelunasanReceive(InvoiceSnapshot invSnap, decimal totalYangHarusDiBayar, decimal denda, long banyakCicilanYangTelahDibayar)
        {
            ReceiveService.CreatePelunasan(invSnap.id, invSnap.BranchId, totalYangHarusDiBayar, denda, banyakCicilanYangTelahDibayar);
        }

        #endregion

        #region Exception

        private void FailIfCantChange(InvoiceSnapshot invSnap)
        {
            if (invSnap.Status == (int)StatusInvoice.CREDIT)
            {
                if (Repository.CountAngsuranBulanan(invSnap.id) > 0)
                    throw new ApplicationException("Angsuran bulanan transaksi ini telah dibayar, sehingga tidak bisa mengubah data");
            }
            else if (invSnap.Status == (int)StatusInvoice.PAID)
            {
                throw new ApplicationException("Transaksi ini telah lunas, sehingga tidak bisa mengubah data");
            }
        }

        private void FailIfInvoiceIsNotCredit(InvoiceSnapshot invSnap)
        {
            if (invSnap.Status != (int)StatusInvoice.CREDIT)
            {
                throw new ApplicationException("Hanya jenis transaksi kredit yang bisa mengubah data");
            }
        }

        private void FailIfCustomerNotFound(Guid customerid)
        {
            if (CustomerRepository.GetById(customerid) == null)
                throw new ApplicationException("Customer tidak ditemukan");
        }
        private void FailIfInvoiceNotFound(InvoiceSnapshot snap)
        {
            if (snap == null)
                throw new ApplicationException("Invoice tidak ditemukan");
        }
        private void FailIfProductCantSale(Guid id, string branchid)
        {
            Product product = ProductRepository.GetProductById(id, branchid);
            if (product == null)
                throw new ApplicationException("Product belum terdaftar");
            else if (product.Status == StatusProduct.NONAKTIF)
                throw new ApplicationException("Product sudah discontinue");
            else if (product.Status == StatusProduct.TERJUAL)
                throw new ApplicationException("Product sudah terjual");
        }

        #endregion

        #region BUS
        private void PublishInvoiceCreated(Invoice inv, string username)
        {
            _bus.Publish(new InvoiceCreated { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishDueDateChanged(Invoice inv, string username)
        {
            _bus.Publish(new DueDateChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishInvoiceDateChanged(Invoice inv, string username)
        {
            _bus.Publish(new InvoiceDateChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishUangMukaChanged(Invoice inv, string username)
        {
            _bus.Publish(new UangMukaChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishUangAngsuranChanged(Invoice inv, string username)
        {
            _bus.Publish(new UangAngsuranChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishSukuBungaChanged(Invoice inv, string username)
        {
            _bus.Publish(new SukuBungaChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishLamaAngsuranChanged(Invoice inv, string username)
        {
            _bus.Publish(new LamaAngsuranChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishAngsuranPaid(Invoice inv, string username)
        {
            _bus.Publish(new AngsuranPaid { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishPaymentTypeChanged(Invoice inv, string username)
        {
            _bus.Publish(new PaymentTypeChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishInvoiceCanceled(Invoice inv, string username)
        {
            _bus.Publish(new InvoiceCanceled { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishInvoicePulled(Invoice inv, string username)
        {
            _bus.Publish(new InvoicePulled { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishProductChanged(Invoice inv, string username)
        {
            _bus.Publish(new ProductChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishCustomerChanged(Invoice inv, string username)
        {
            _bus.Publish(new CustomerChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishHargaJualChanged(Invoice inv, string username)
        {
            _bus.Publish(new HargaJualChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        private void PublishTermChanged(Invoice inv, string username)
        {
            _bus.Publish(new TermChanged { Payload = inv.CreateSnapshot(), Username = username });
        }
        #endregion
    }
}
