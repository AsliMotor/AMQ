using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SI.Repository
{
    [NamedSqlQuery("findById", @"SELECT * FROM supplierinvoice where id = @id and branchid = @branchid")]
    [Serializable]
    public class SupplierInvoice : IViewModel
    {
        public Guid id { get; set; }
        public string BranchId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime SupplierInvoiceDate { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public string SupplierName { get; set; }
        public string NoTelp { get; set; }
        public string SupplierBillingAddress { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Tahun { get; set; }
        public string Warna { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string NoBpkb { get; set; }
        public string NoPolisi { get; set; }
        public decimal HargaBeli { get; set; }
        public string Note { get; set; }
        public Guid ProductId { get; set; }
    }
}