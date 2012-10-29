using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.SI.Services;
using AsliMotor.SI.Repository;

namespace AsliMotor.SI.Fixture
{
    [Subject("Update SI")]
    public class when_update_si
    {
        static ISupplierInvoiceService siService;
        static ISupplierInvoiceRepository siRepo;
        static Guid siId;
        Establish context = () =>
        {
            siService = new SupplierInvoiceService();
            siRepo = new SupplierInvoiceRepository();
            siId = new Guid("fb8879db-afc6-46e5-9e97-05d7d29c94f4");
        };
        Because of = () =>
        {

            SupplierInvoice si = new SupplierInvoice()
            {
                BranchId = "dny@gmail.com",
                HargaBeli = 9000000M,
                id = siId,
                Merk = "Yamaha",
                NoBpkb = "BPKB10001",
                NoMesin = "MESIN10001",
                NoPolisi = "BP1111FO",
                NoRangka = "RANGKA10001",
                SupplierBillingAddress = "Komplek Baloi View Blok E1",
                SupplierInvoiceDate = DateTime.Now,
                SupplierName = "Denny Wu",
                Tahun = "2012",
                Type = "MIO CW",
                Warna = "Merah"
            };
            siService.Update(si, "dny");
        };
        It should_be_create_si = () =>
        {
            SupplierInvoice siReport = siRepo.GetById(siId, "dny@gmail.com");
            siReport.ShouldNotBeNull();
        };
    }
}
