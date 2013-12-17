using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Soap;

namespace Ru.Imagio.ViewModel.Crypto
{
    internal static class CryptoSignStore
    {
        private static string fileName = "data.xml";

        private static readonly CryptoSign NullCryptoSign = new CryptoSign
        {
            RememberMe = false,
            UserName = "",
            Password = ""
        };

        public static void Save(CryptoSign data)
        {
            var stream = File.Open(fileName, FileMode.Create);
            var formatter = new SoapFormatter();
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static CryptoSign Load()
        {
            try
            {
                Stream stream = File.Open(fileName, FileMode.Open);
                var formatter = new SoapFormatter();
                var data = formatter.Deserialize(stream);
                stream.Close();
                return data as CryptoSign;
            }
            catch (Exception)
            {
                return NullCryptoSign;
            }
        }
    }
}
