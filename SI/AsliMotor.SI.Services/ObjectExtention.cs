using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.SI.Services
{
    public static class ObjectExtention
    {
        public static DateTime UtcDate(this DateTime date)
        {
            DateTime dt = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
            return dt;
        }
    }
}
