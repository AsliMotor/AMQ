using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Products.Models;

namespace AsliMotor.Products
{
    public interface IProductService
    {
        void Create(Product product, string username);
        void Update(Product product, string username);
        void Delete(Product product);
        void ChangeStatus(Guid id, string branchid, string status, string username);
    }
}
