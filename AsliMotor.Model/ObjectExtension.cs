using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.Model
{
    public static class ObjectExtension
    {
        public static bool IsNotNullAndWhiteSpace(this string str)
        {
            return !str.IsNullOrWhiteSpace();
        }
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }
    }
}
