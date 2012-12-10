using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Customers
{
    public interface ICustomerService
    {
        void Create(Customer cust);
        void Update(Customer cust);
        void ChangeStatus(Guid id, StatusCustomer status);
        void UploadImage(Guid id, byte[] image);
    }
}
