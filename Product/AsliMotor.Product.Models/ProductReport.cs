using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Products.Models
{
    [NamedSqlQuery("findAllByOffset", @"select id,
	                                    type,
	                                    nopolisi,
	                                    nomesin,
	                                    norangka,
	                                    nobpkb,
	                                    status 
	                                    from product
	                                    where branchid=@branchid and status=@status
	                                    ORDER BY type ASC limit 10 offset @offset")]
    public class ProductReport : IViewModel
    {
        public Guid id { get; set; }
        public string Type { get; set; }
        public string NoPolisi { get; set; }
        public string NoMesin { get; set; }
        public string NoRangka { get; set; }
        public string NoBpkb { get; set; }
        public string Status { get; set; }
    }
}
