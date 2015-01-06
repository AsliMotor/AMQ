using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Reporting;
using Spring.Context.Support;
using AsliMotor.SI.Repository;
using AsliMotor.Products;
using AsliMotor.SI.AutoNumberGenerator;
using AsliMotor.Products.Models;
using NServiceBus;
using AsliMotor.SI.Events;

namespace AsliMotor.SI.Services
{
    public class SupplierInvoiceService : ISupplierInvoiceService
    {
        public IReportingRepository ReportingRepository { get; set; }
        public IProductService ProductService { get; set; }
        ISIAutoNumberGenerator _autoNumberGenerator;
        ISupplierInvoiceRepository _repository;
        IBus _bus;
        public SupplierInvoiceService()
        {
            _autoNumberGenerator = new SIAutoNumberGenerator();
            _repository = new SupplierInvoiceRepository();
            _bus = NServiceBus.Configure.Instance.Builder.Build<IBus>();
        }

        public void Create(SupplierInvoice si, string username)
        {
            si.TransactionDate = DateTime.Now;
            si.SupplierInvoiceDate = si.SupplierInvoiceDate.UtcDate();
            Product productCreated = CreateProduct(si, username);
            si.SupplierInvoiceNo = _autoNumberGenerator.GenerateSINumber(si.TransactionDate, si.BranchId);
            try
            {
                ReportingRepository.Save<SupplierInvoice>(si);
                PublishSupplierInvoiceCreated(si, username);
            }
            catch
            {
                ProductService.Delete(productCreated);
            }
        }

        public void Update(SupplierInvoice si, string username)
        {
            SupplierInvoice exist = _repository.GetById(si.id, si.BranchId);
            if (exist == null)
                throw new Exception("Data tidak ditemukan.");
            SupplierInvoice siUpdated = new SupplierInvoice()
            {
                id = exist.id,
                ProductId = exist.ProductId,
                BranchId = exist.BranchId,
                SupplierInvoiceNo = exist.SupplierInvoiceNo,
                TransactionDate = exist.TransactionDate,
                HargaBeli = si.HargaBeli,
                Merk = si.Merk,
                NoBpkb = si.NoBpkb,
                NoMesin = si.NoMesin,
                NoPolisi = si.NoPolisi,
                NoRangka = si.NoRangka,
                SupplierBillingAddress = si.SupplierBillingAddress,
                SupplierInvoiceDate = si.SupplierInvoiceDate,
                SupplierName = si.SupplierName,
                Tahun = si.Tahun,
                Type = si.Type,
                Warna = si.Warna,
                Note = si.Note,
                NoTelp = si.NoTelp,
                Charge = si.Charge
            };
            UpdateProduct(siUpdated, username);
            ReportingRepository.Update<SupplierInvoice>(siUpdated, new { Id = si.id });
            PublishSupplierInvoiceChanged(siUpdated, username);
            PublishSupplierInvoicePriceChanged(siUpdated, exist, username);
        }

        public void ForceUpdate(SupplierInvoice si, string username)
        {
            SupplierInvoice exist = _repository.GetById(si.id, si.BranchId);
            if (exist == null)
                throw new Exception("Data tidak ditemukan.");
            SupplierInvoice siUpdated = new SupplierInvoice()
            {
                id = exist.id,
                ProductId = exist.ProductId,
                BranchId = exist.BranchId,
                SupplierInvoiceNo = exist.SupplierInvoiceNo,
                TransactionDate = exist.TransactionDate,
                HargaBeli = si.HargaBeli,
                Merk = si.Merk,
                NoBpkb = si.NoBpkb,
                NoMesin = si.NoMesin,
                NoPolisi = si.NoPolisi,
                NoRangka = si.NoRangka,
                SupplierBillingAddress = si.SupplierBillingAddress,
                SupplierInvoiceDate = si.SupplierInvoiceDate,
                SupplierName = si.SupplierName,
                Tahun = si.Tahun,
                Type = si.Type,
                Warna = si.Warna,
                Note = si.Note,
                NoTelp = si.NoTelp,
                Charge = si.Charge
            };
            ForceUpdateProduct(siUpdated, username);
            ReportingRepository.Update<SupplierInvoice>(siUpdated, new { Id = si.id });
            PublishSupplierInvoiceChanged(siUpdated, username);
            PublishSupplierInvoicePriceChanged(siUpdated, exist, username);
        }

        private Product CreateProduct(SupplierInvoice si, string username)
        {
            Product product = new Product()
            {
                id = si.ProductId,
                BranchId = si.BranchId,
                HargaBeli = si.HargaBeli,
                Merk = si.Merk,
                NoBpkb = si.NoBpkb,
                NoMesin = si.NoMesin,
                NoPolisi = si.NoPolisi,
                NoRangka = si.NoRangka,
                Status = StatusProduct.AKTIF,
                Tahun = si.Tahun,
                Type = si.Type,
                Warna = si.Warna
            };
            ProductService.Create(product, username);
            return product;
        }

        private void UpdateProduct(SupplierInvoice si, string username)
        {
            Product product = new Product()
            {
                id = si.ProductId,
                BranchId = si.BranchId,
                HargaBeli = si.HargaBeli,
                Merk = si.Merk,
                NoBpkb = si.NoBpkb,
                NoMesin = si.NoMesin,
                NoPolisi = si.NoPolisi,
                NoRangka = si.NoRangka,
                Status = StatusProduct.AKTIF,
                Tahun = si.Tahun,
                Type = si.Type,
                Warna = si.Warna
            };
            ProductService.Update(product, username);
        }

        private void ForceUpdateProduct(SupplierInvoice si, string username)
        {
            Product product = new Product()
            {
                id = si.ProductId,
                BranchId = si.BranchId,
                HargaBeli = si.HargaBeli,
                Merk = si.Merk,
                NoBpkb = si.NoBpkb,
                NoMesin = si.NoMesin,
                NoPolisi = si.NoPolisi,
                NoRangka = si.NoRangka,
                Status = StatusProduct.AKTIF,
                Tahun = si.Tahun,
                Type = si.Type,
                Warna = si.Warna
            };
            ProductService.ForceUpdate(product, username);
        }

        private void PublishSupplierInvoiceChanged(SupplierInvoice si, string username)
        {
            if (_bus != null)
            {
                _bus.Publish(new SupplierInvoiceChanged { Payload = si, UserName = username });
            }
        }
        private void PublishSupplierInvoiceCreated(SupplierInvoice si, string username)
        {
            if (_bus != null)
            {
                _bus.Publish(new SupplierInvoiceCreated { Payload = si, UserName = username });
            }
        }
        private void PublishSupplierInvoicePriceChanged(SupplierInvoice siUpdated, SupplierInvoice exist, string username)
        {
            if (_bus != null)
            {
                if (siUpdated.HargaBeli != exist.HargaBeli || siUpdated.Charge != exist.Charge)
                    _bus.Publish(new PriceSupplierInvoiceChanged
                    {
                        Payload = siUpdated,
                        UserName = username,
                        BeforeCharge = exist.Charge,
                        BeforeHargaBeli = exist.HargaBeli
                    });
            }
        }
    }
}
