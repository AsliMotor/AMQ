using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Products.Models
{
    [NamedSqlQuery("findByKey", @"select id,
       merk,
       type,
       tahun,
       warna,
       norangka,
       nomesin,
       nopolisi
       from product
       where (LOWER(nopolisi) like @key or LOWER(nomesin) like @key or LOWER(norangka) like @key)
       and branchid = @branchid 
       and status = 'Aktif'
       limit 5")]
    public class ProductSearch : IViewModel
    {
        public Guid id { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Tahun { get; set; }
        public string Warna { get; set; }
        public string NoRangka { get; set; }
        public string NoMesin { get; set; }
        public string NoPolisi { get; set; }
    }
}
