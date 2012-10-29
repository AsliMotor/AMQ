using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.SI.AutoNumberGenerator
{
    public static class AutoNumberIdResolver
    {
        public static string AutoNumberConfigId(this string branchId)
        {
            return string.Format("{0}-{1}", branchId, typeof(SIAutoNumberConfig).Name);
        }
        public static string SIAutoNumberMonthlyId(this DateTime transactionDate, string branchId)
        {
            return string.Format("{0}-{1}{2}", branchId, transactionDate.Year.ToString(), transactionDate.Month.ToString().PadLeft(2, '0'));
        }
        public static string SIAutoNumberYearlyId(this DateTime transactionDate, string branchId)
        {
            return string.Format("{0}-{1}", branchId, transactionDate.Year.ToString());
        }
    }
}
