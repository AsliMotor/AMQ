using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Customers
{
    public interface ICustomerRepository
    {
        Customer GetByEmail(string email, string branchid);
        Customer GetByKTPNo(string ktpno, string branchid);
        Customer GetById(Guid id);
        IList<CustomerSearch> Search(string key, string branchid);
        IList<CustomerReport> GetListView(string branchid, int offset);
        IList<CustomerReport> SearchListView(string branchid, int offset, string key);
        TotalCustomer GetTotalList(string branchid);
    }
}
