using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.AuditLog.Repository
{
    [NamedSqlQuery("findById", @"select
                                merk,
	                            type,
	                            nopolisi,
	                            nomesin,
	                            norangka
	                            from product
	                            where id=@id")]
    public class Product:IViewModel
    {
        public string Merk { get; set; }
        public string Type { get; set; }
        public string NoPolisi { get; set; }
        public string NoMesin { get; set; }
        public string NoRangka { get; set; }
    }
}
