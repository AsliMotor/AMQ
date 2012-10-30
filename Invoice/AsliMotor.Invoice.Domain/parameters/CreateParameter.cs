﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Invoices.Snapshots;

namespace AsliMotor.Invoices.Domain
{
    public class CreateParameter
    {
        public string BranchId { get; set; }
        public Guid id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public StatusInvoice Status { get; set; }
        public decimal Price { get; set; }
        public int LamaAngsuran { get; set; }
        public decimal UangMuka { get; set; }
        public decimal SukuBunga { get; set; }
        public DateTime DueDate { get; set; }
    }
}