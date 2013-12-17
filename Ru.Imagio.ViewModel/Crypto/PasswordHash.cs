using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ru.Imagio.ViewModel.Crypto
{
    public static class PasswordHash
    {
        public static string CalcPasswordHash(string password)
        {
            var hash = MD5.Create().ComputeHash(
                Encoding.ASCII.GetBytes(password));

            var sb = new StringBuilder("*");
            foreach (var b in hash)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString().ToUpper();
        }
    }
}
