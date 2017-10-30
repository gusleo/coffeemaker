using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace dna.core.libs.Extension
{
    public static class StringExtension
    {
        public static string ToMd5(this string stringToHash)
        {
            using ( var md5 = MD5.Create() )
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(stringToHash));
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}