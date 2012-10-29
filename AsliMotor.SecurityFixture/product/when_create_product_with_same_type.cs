using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using AsliMotor.Products;
using AsliMotor.Products.Models;

namespace AsliMotor.Fixture
{
    [Subject("create product with same type")]
    public class when_create_product_with_same_type
    {
        static IProductService productService;
        static IProductRepository productRepo;
        static Product product;
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
                NoBpkb = "Bpkb10004",
                NoMesin = "Mesin1015",
                NoPolisi = "BP5037FO",
                NoRangka = "Rangka10217",
                Tahun = "2011",
                Type = "MIO Soul",
                Warna = "Merah"
            }, "dny");
        };
        It should_be_create_product = () =>
        {
            product = productRepo.GetProductById(productId, "dny@gmail.com");
            product.NoPolisi.ShouldEqual("BP5037FO");
        };
    }
}
