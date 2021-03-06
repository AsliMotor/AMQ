﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Invoices.AutoNumberGenerator
{
    [NamedSqlQuery("findByIdAndBranchId", @"SELECT * FROM invoiceautonumberdefault WHERE id = @id AND branchid = @branchid")]
    public class InvoiceAutoNumberDefault : IViewModel
    {
        public string id { get; set; }
        public string BranchId { get; set; }
        public double Value { get; set; }

        public InvoiceAutoNumberDefault Next()
        {
            Value++;
            return this;
        }
        public void Reset()
        {
            Value = 0;
        }
        public string InvoiceNumberInStringFormat(string prefix)
        {
            return string.Format("{0}-{1}",
                prefix,
                Value.ToString().PadLeft(3, '0'));
        }
    }
}
