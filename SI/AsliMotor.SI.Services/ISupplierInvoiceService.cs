using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.SI.Repository;

namespace AsliMotor.SI.Services
{
    public interface ISupplierInvoiceService
    {
        void Create(SupplierInvoice si, string username);
        void Update(SupplierInvoice si, string username);
        void ForceUpdate(SupplierInvoice si, string p);
    }
}
