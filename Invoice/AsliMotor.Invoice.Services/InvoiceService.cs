﻿using System;
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

namespace AsliMotor.Invoices.Services
{
    public class InvoiceService:IInvoiceService
    {
        public IQueryObjectMapper QueryObjectMapper { get; set; }
        public IInvoiceRepository Repository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IProductService ProductService { get; set; }
        public IReceiveService ReceiveService { get; set; }
        public IReceiveRepository ReceiveRepository { get; set; }
        public IPerjanjianService PerjanjianService { get; set; }
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
            Invoice inv = new Invoice(new CreateParameter
            {
                BranchId = cmd.BranchId,
                CustomerId = cmd.CustomerId,
                id = cmd.id,
                InvoiceDate = cmd.InvoiceDate,
                Price = cmd.Price,
                ProductId = cmd.ProductId,
                UangTandaJadi = cmd.DebitNote,
                Status = StatusInvoice.BOOKING
            });
            Repository.Save(inv);
            ProductService.ChangeStatus(cmd.ProductId, cmd.BranchId, StatusProduct.TERJUAL);
            CreateBookingReceive(inv, cmd.DebitNote);
            PublishInvoiceCreated(inv, username);
        }

        public void Cash(CashCommand cmd, string username)
        {
            FailIfCustomerNotFound(cmd.CustomerId);
            FailIfProductCantSale(cmd.ProductId, cmd.BranchId);
            Invoice inv = new Invoice(new CreateParameter
            {
                BranchId = cmd.BranchId,
                CustomerId = cmd.CustomerId,
                id = cmd.id,
                InvoiceDate = cmd.InvoiceDate,
                Price = cmd.Price,
                ProductId = cmd.ProductId,
                Status = StatusInvoice.PAID
            });
            Repository.Save(inv);
            ProductService.ChangeStatus(cmd.ProductId, cmd.BranchId, StatusProduct.TERJUAL);
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
            }
        }

        public void Credit(CreditCommand cmd, string username)
        {
            FailIfCustomerNotFound(cmd.CustomerId);
            FailIfProductCantSale(cmd.ProductId, cmd.BranchId);
            Invoice inv = new Invoice(new CreateParameter
            {
                BranchId = cmd.BranchId,
                CustomerId = cmd.CustomerId,
                id = cmd.id,
                InvoiceDate = cmd.InvoiceDate,
                ProductId = cmd.ProductId,
                Status = StatusInvoice.CREDIT,
                Price = cmd.Price,
                UangMuka = cmd.UangMuka,
                SukuBunga = cmd.SukuBunga,
                LamaAngsuran = cmd.LamaAngsuran,
                DueDate = cmd.DueDate
            });
            Repository.Save(inv);
            ProductService.ChangeStatus(cmd.ProductId, cmd.BranchId, StatusProduct.TERJUAL);
            CreateUangMukaReceive(inv, cmd.UangMuka);
            PerjanjianService.CreateSuratPerjanjian(inv.CreateSnapshot().id, inv.CreateSnapshot().BranchId);
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
                inv.UpdateToCredit(new UpdateToCreditParameter
                {
                    DueDate = cmd.DueDate,
                    LamaAngsuran = cmd.LamaAngsuran,
                    SukuBunga = cmd.SukuBunga,
                    UangMuka = cmd.UangMuka,
                    UangTandaJadi = bookingRcv.Total
                });
                Repository.Update(inv);
                CreateUangMukaReceive(inv, cmd.UangMuka);
                PerjanjianService.CreateSuratPerjanjian(inv.CreateSnapshot().id, inv.CreateSnapshot().BranchId);
            }
        }

        public void BayarAngsuran(Guid id, DateTime date, string username)
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
                inv.BayarAngsuran();
                Repository.Update(inv);
                CreateAngsuranReceive(inv,date, denda);
            }
        }

        public void ChangeUangMuka(Guid id, decimal uangmuka, string username)
        {
            FailIfAnyAngsuranBulanan(id);
            Invoice inv = Repository.Get(id);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            FailIfInvoiceNotFound(invSnap);
            decimal debitnote = Repository.GetUangTandaJadi(id);
            inv.ChangeUangMuka(uangmuka, debitnote);
            Repository.Update(inv);
            UpdateUangMukaReceive(inv, uangmuka);
        }

        public void Cancel(Guid id, string username)
        {
            Invoice inv = Repository.Get(id);
            inv.Cancel();
            Repository.Update(inv);
            InvoiceSnapshot invSnap = inv.CreateSnapshot();
            ProductService.ChangeStatus(invSnap.ProductId, invSnap.BranchId, StatusProduct.AKTIF);
        }

        #region create receive

        private void CreateAngsuranReceive(Invoice inv,DateTime date, decimal denda)
        {
            InvoiceSnapshot invSnapshot = inv.CreateSnapshot();
            ReceiveService.CreateAngsuran(new CreateAngsuranReceive
            {
                BranchId = invSnapshot.BranchId,
                Denda = Math.Round(denda),
                Total = Math.Round(invSnapshot.AngsuranBulanan + denda),
                InvoiceId = invSnapshot.id,
                BulanAngsuran = invSnapshot.DueDate.AddMonths(-1).ToString("MMyyyy"),
                PaymentDate = date
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

        #endregion

        #region Exception

        private void FailIfAnyAngsuranBulanan(Guid id)
        {
            if (Repository.CountAngsuranBulanan(id) > 0)
                throw new ApplicationException("transaksi ini telah melakukan angsuran bulanan, sehingga tidak bisa merubah uang muka");
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
            else if(product.Status == StatusProduct.NONAKTIF)
                throw new ApplicationException("Product sudah discontinue");
            else if (product.Status == StatusProduct.TERJUAL)
                throw new ApplicationException("Product sudah terjual");
        }

        private void PublishInvoiceCreated(Invoice inv, string username)
        {
            if (_bus != null)
            {
                _bus.Publish(new InvoiceCreated
                {
                    Payload = inv.CreateSnapshot(),
                    Username = username
                });
            }
        }
        #endregion
    }
}
