using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.SI.Services;
using AsliMotor.SI.Repository;

namespace AsliMotor.SI.Fixture
{
    [Subject("Create SI")]
    public class when_create_si
    {
        static ISupplierInvoiceService siService;
        Establish context = () =>
        {
            siService = new SupplierInvoiceService();
        };
        Because of = () =>
        {
            SupplierInvoice si = new SupplierInvoice()
            {
                BranchId = "dny@gmail.com",
                HargaBeli = 953000M,
                id = Guid.NewGuid(),
                NoBpkb = "BPKB10001",
                NoMesin = "MESIN10001",
                NoPolisi = "BP 9021 FO",
                NoRangka = "RANGKA10001",
                SupplierBillingAddress = "Komplek Baloi View No 19",
                SupplierInvoiceDate = DateTime.Now,
                SupplierName = "Apin Wu",
                Tahun = "2012",
                Type = "MIO CW",
                Warna = "Hijau"
            };
            siService.Create(si,"dny");
        };
        It should_be_create_si = () =>
        {
        };
    }
}
