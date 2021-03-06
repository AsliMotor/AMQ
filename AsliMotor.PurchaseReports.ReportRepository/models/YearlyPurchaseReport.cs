﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.PurchaseReports.ReportRepository
{
    [NamedSqlQuery("findYearlyPurchaseReport", @"
                    SELECT
                        sum(hargabeli) AS Total,
                        date_trunc('year', supplierinvoicedate) AS PurchaseDate
                    FROM supplierinvoice
                    where branchid = @branchid and 
	                   (extract(year from supplierinvoicedate) between  @fromyear and @toyear)
                    GROUP BY PurchaseDate
                ")]
    public class YearlyPurchaseReport : IViewModel
    {
        public DateTime PurchaseDate { get; set; }
        public decimal Total { get; set; }
        public long PurchaseDateTick { get { return (long)PurchaseDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds; } }
    }
}
