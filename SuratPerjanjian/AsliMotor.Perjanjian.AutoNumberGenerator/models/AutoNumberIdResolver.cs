using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Perjanjian.AutoNumberGenerator
{
    public static class AutoNumberIdResolver
    {
        public static string AutoNumberConfigId(this string branchId)
        {
            return string.Format("{0}-{1}", branchId, typeof(PerjanjianAutoNumberConfig).Name);
        }
        public static string PerjanjianAutoNumberMonthlyId(this DateTime transactionDate, string branchId)
        {
            return string.Format("{0}-{1}{2}", branchId, transactionDate.Year.ToString(), transactionDate.Month.ToString().PadLeft(2, '0'));
        }
        public static string PerjanjianAutoNumberYearlyId(this DateTime transactionDate, string branchId)
        {
            return string.Format("{0}-{1}", branchId, transactionDate.Year.ToString());
        }
    }
}
