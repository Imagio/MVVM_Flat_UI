using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ru.Imagio.ViewModel.Crypto
{
    [Serializable]
    internal class CryptoSign
    {
        public bool RememberMe { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
