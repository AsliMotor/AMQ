using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.SI.Repository
{
    [NamedSqlQuery("findAllByOffset", @"SELECT id, 
                                        supplierinvoicedate, 
                                        supplierinvoiceno,
                                        suppliername,
                                        charge,
                                        type,
                                        nopolisi,
                                        hargabeli
                                        FROM supplierinvoice where branchid = @branchid ORDER BY supplierinvoiceno DESC limit 10 offset @offset")]
    [NamedSqlQuery("searchByKey", @"SELECT id, 
                                        supplierinvoicedate, 
                                        supplierinvoiceno,
                                        suppliername,
                                        charge,
                                        type,
                                        nopolisi,
                                        hargabeli
                                        FROM supplierinvoice where branchid = @branchid and 
                                        (LOWER(supplierinvoiceno) like @key or LOWER(suppliername) like @key or LOWER(type) like @key or LOWER(nopolisi) like @key)
                                        ORDER BY supplierinvoiceno DESC limit 10 offset @offset")]
    public class SupplierInvoiceReport : IViewModel
    {
        public Guid id { get; set; }
        public DateTime SupplierInvoiceDate { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public string SupplierName { get; set; }
        public string Type { get; set; }
        public string NoPolisi { get; set; }
        public decimal HargaBeli { get; set; }
        public decimal Charge { get; set; }
        public decimal Total { get { return HargaBeli + Charge; } }
    }
}
