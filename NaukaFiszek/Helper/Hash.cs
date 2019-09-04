using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NaukaFiszek.Helper
{
    public static class Hash
    {
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
        public static string SHA1Hash(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            return HexStringFromBytes(hashBytes);

        }
    }
}