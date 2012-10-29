using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Products;
using AsliMotor.Products.Models;

namespace AsliMotor.Fixture
{
    [Subject("create product")]
    public class when_create_product
    {
        static IProductService productService;
        static IProductRepository productRepo;
        static Guid productId;
        Establish context = () =>
        {
            productService = new ProductService();
            productRepo = new ProductRepository();
            productId = Guid.NewGuid();
        };
        Because of = () =>
        {
            productService.Create(new Product()
            {
                id = productId,
                BranchId = "dny@gmail.com",
                HargaBeli = 8000000M,
                Merk = "Yamaha",
                NoBpkb = "Bpkb10001",
                NoMesin = "Mesin1012",
                NoPolisi = "BP5038FO",
                NoRangka = "Rangka10212",
                Tahun = "2011",
                Type = "MIO CW",
                Warna = "Merah"
            },"dny");
        };
        It should_be_create_product = () =>
        {
            Product product = productRepo.GetProductById(productId, "dny@gmail.com");
            product.NoPolisi.ShouldEqual("BP5038FO");
        };
    }
}
