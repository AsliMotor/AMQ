using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Products.Models
{
    [NamedSqlQuery("findByNoPolisi",@"SELECT * FROM product WHERE nopolisi = @nopolisi and branchid=@branchid and status != 'Terjual_Lunas'")]
    [NamedSqlQuery("findByNoRangka", @"SELECT * FROM product WHERE norangka = @norangka and branchid=@branchid and status != 'Terjual_Lunas'")]
    [NamedSqlQuery("findByNoMesin", @"SELECT * FROM product WHERE nomesin = @nomesin and branchid=@branchid and status != 'Terjual_Lunas'")]
    [NamedSqlQuery("findByNoBpkb", @"SELECT * FROM product WHERE nobpkb = @nobpkb and branchid=@branchid and status != 'Terjual_Lunas'")]
    [NamedSqlQuery("findById", @"SELECT * FROM product WHERE id = @id and branchid=@branchid")]
    [Serializable]
    public class Product : IViewModel
    {
        public Guid id { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Tahun { get; set; }
        public string Warna { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string NoBpkb { get; set; }
        public string NoPolisi { get; set; }
        public decimal HargaBeli { get; set; }
        public string Status { get; set; }
        public string BranchId { get; set; }
    }
}
