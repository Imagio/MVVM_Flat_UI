using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ru.Imagio.ViewModel.Crypto;

namespace ViewModelTest
{
    [TestClass]
    public class EncodeTest
    {
        [TestMethod]
        public void TestEncoding()
        {
            var result = PasswordEncoder.Encode("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
            result = PasswordEncoder.Decode(result);
            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", result);
        }

        [TestMethod]
        public void TestEncodingRu()
        {
            var result = PasswordEncoder.Encode("АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзиклмнопрстуфхцчшщъыьэюя");
            result = PasswordEncoder.Decode(result);
            Assert.AreEqual("АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзиклмнопрстуфхцчшщъыьэюя", result);
        }

    }
}
