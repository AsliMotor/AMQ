﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Invoices.Domain;
using AsliMotor.Invoices.Snapshots;

namespace AsliMotor.Invoices.Repository
{
    public interface IInvoiceRepository
    {
        void Save(Invoice inv);
        void Update(Invoice inv);
        Invoice Get(Guid id);
    }
}