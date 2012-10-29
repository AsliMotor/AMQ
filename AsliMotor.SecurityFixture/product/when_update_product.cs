using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Products;
using AsliMotor.Products.Models;

namespace AsliMotor.Fixture
{
    [Subject("update product")]
    public class when_update_product
    {
        static IProductService productService;
        static IProductRepository productRepo;
        static Guid productId;
        Establish context = () =>
        {
            productService = new ProductService();
            productRepo = new ProductRepository();
            productId = new Guid("cc81d39c-927a-4735-9eae-662315513ae6");
        };
        Because of = () =>
        {
            productService.Update(new Product()
            {
                id = productId,
                BranchId = "dny@gmail.com",
                HargaBeli = 8000000M,
                Merk = "Yamaha",
                NoBpkb = "Bpkb11112",
                NoMesin = "Mesin1012",
                NoPolisi = "BP5037FO",
                NoRangka = "Rangka10212",
                Tahun = "2011",
                Type = "MIO C",
                Warna = "Merah"
            }, "dny");
        };
        It should_be_create_product = () =>
        {
            Product product = productRepo.GetProductById(productId, "dny@gmail.com");
            product.NoPolisi.ShouldEqual("BP5037FO");
        };
    }
}
