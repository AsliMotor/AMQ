using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.DataMapper.Impl;
using BonaStoco.Inf.Reporting;
using Spring.Context.Support;
using NServiceBus;
using AsliMotor.Products.Models;
using AsliMotor.Products.Events;

namespace AsliMotor.Products
{
    public class ProductService : IProductService
    {
        public QueryObjectMapper QueryObjectMapper { get; set; }
        public IReportingRepository ReportingRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        IBus _bus;

        public ProductService()
        {
            _bus = NServiceBus.Configure.Instance.Builder.Build<IBus>();
        }

        public void Create(Product product, string username)
        {
            FailIfExistProduct(product);
            product.Status = StatusProduct.AKTIF;
            ReportingRepository.Save<Product>(product);
            SaveTypeIfNecessary(product);
            PublishProductCreated(product, username);
        }

        public void Update(Product p, string username)
        {
            Product product = ProductRepository.GetProductById(p.id, p.BranchId);
            if (product.NoPolisi != p.NoPolisi)
            {
                if (ProductRepository.GetByNoPolisi(p.NoPolisi, p.BranchId) != null)
                    throw new Exception(string.Format("Kendaraan dengan no polisi {0} telah ada.", p.NoPolisi));
            }
            if(product.Status == StatusProduct.TERJUAL)
                throw new ApplicationException(string.Format("Kendaraan dengan no polisi {0} telah terjual.", p.NoPolisi));
            
            product.HargaBeli = p.HargaBeli;
            product.Merk = p.Merk;
            product.NoBpkb = p.NoBpkb;
            product.NoMesin = p.NoMesin;
            product.NoPolisi = p.NoPolisi;
            product.NoRangka = p.NoRangka;
            product.Tahun = p.Tahun;
            product.Type = p.Type;
            product.Warna = p.Warna;
            ReportingRepository.Update<Product>(product, new { Id = product.id });
            SaveTypeIfNecessary(p);
            PublishProductChanged(p, username);
        }

        public void Delete(Product product)
        {
            ReportingRepository.Delete<Product>(product);
        }

        public void ChangeStatus(Guid id, string branchid, string status, string username)
        {
            Product product = ProductRepository.GetProductById(id, branchid);
            if (product == null)
                throw new Exception("Data tidak ditemukan.");
            product.Status = status;
            ReportingRepository.Update<Product>(product, new { Id = product.id });
            PublishProductChanged(product, username);
        }

        private void FailIfExistProduct(Product product)
        {
            if (ProductRepository.GetByNoPolisi(product.NoPolisi, product.BranchId) != null)
                throw new Exception(string.Format("Kendaraan dengan no polisi {0} telah ada.", product.NoPolisi));
            if (ProductRepository.GetByNoRangka(product.NoRangka, product.BranchId) != null)
                throw new Exception(string.Format("Kendaraan dengan no rangka {0} telah ada.", product.NoRangka));
            if (ProductRepository.GetByNoMesin(product.NoMesin, product.BranchId) != null)
                throw new Exception(string.Format("Kendaraan dengan no mesin {0} telah ada.", product.NoMesin));
            if (ProductRepository.GetByNoBpkb(product.NoBpkb, product.BranchId) != null)
                throw new Exception(string.Format("Kendaraan dengan no bpkb {0} telah ada.", product.NoBpkb));
        }

        private void SaveTypeIfNecessary(Product product)
        {
            if (ProductRepository.GetTypeByName(product.Type, product.BranchId) == null)
            {
                TypeProduct type = new TypeProduct { Id = Guid.NewGuid(), Name = product.Type, BranchId = product.BranchId };
                ReportingRepository.Save<TypeProduct>(type);
            }
        }

        private void PublishProductCreated(Product p, string username)
        {
            _bus.Publish(new ProductCreated { Payload = p, UserName = username });
        }

        private void PublishProductChanged(Product p, string username)
        {
            _bus.Publish(new ProductChanged { Payload = p, UserName = username });
        }
    }
}
