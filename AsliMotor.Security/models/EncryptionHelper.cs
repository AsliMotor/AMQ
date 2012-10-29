//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Security;
//using System.Security.Cryptography;

//namespace AsliMotor.Security
//{
//    public static class EncryptionHelper
//    {
//        public static byte[] EncodePassword(this Account acc)
//        {
//            string salt = Membership.GeneratePassword(24, 12);
//            return Encoding.Unicode.GetBytes(acc.Password + salt);
//        }
//        public static string DecodePassword(this Account acc)
//        {
//            return Encoding.Unicode.GetString(ASCIIEncoding.ASCII.GetBytes(acc.Password + acc.PasswordSalt));
//        }
//    }
//}
