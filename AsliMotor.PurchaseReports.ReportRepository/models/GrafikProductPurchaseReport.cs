﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.PurchaseReports.ReportRepository
{
    [NamedSqlQuery("findGrafikProductPurchaseReport", @"
        select 
            p.type, 
            count(*) as Total 
        from supplierinvoice inv inner join product p on inv.productid = p.id 
        where (supplierinvoicedate between @fromDate and @toDate) and inv.branchid = @branchid
        group by p.type")]
    public class GrafikProductPurchaseReport : IViewModel
    {
        public string Type { get; set; }
        public long Total { get; set; }
    }
}
