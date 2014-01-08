using System;
using System.Text;

namespace Ru.Imagio.ViewModel.Crypto
{
    public static class PasswordEncoder
    {
        public static string Encode(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public static string Decode(string encodedString)
        {
            try
            {
                var bytes = Convert.FromBase64String(encodedString);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}
