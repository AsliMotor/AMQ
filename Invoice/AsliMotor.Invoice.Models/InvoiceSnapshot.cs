﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;
using AsliMotor.PaymentTerms;

namespace AsliMotor.Invoices.Snapshots
{
    [NamedSqlQuery("findById", @"SELECT *,
        (select value from paymentterm where id = inv.termid) as TermValue 
        FROM invoicesnapshot inv
        where inv.id = @id")]
    [Serializable]
    public class InvoiceSnapshot : IViewModel
    {
        public Guid id { get; set; }
        public string InvoiceNo { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid TermId { get; set; }
        public int TermType { get; set; }
        public int TermValue { get; set; }
        public string BranchId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Status { get; set; }
        public decimal Price { get; set; }
        public decimal TotalKredit { get; set; }
        public int LamaAngsuran { get; set; }
        public int BanyakCicilan { get; set; }
        public decimal SukuBunga { get; set; }
        public decimal AngsuranBulanan { get; set; }
        public DateTime StartDueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Outstanding { get; set; }
        public decimal Discount { get; set; }
    }
}
