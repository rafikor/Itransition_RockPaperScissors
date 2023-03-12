using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests
{
    [TestClass()]
    public class CryptoTests
    {
        [TestMethod()]
        public void GenerateKeyLengthTest()
        {
            Crypto hmacGenerator=new ();
            Assert.AreEqual(hmacGenerator.Key.Length, hmacGenerator.KeyLength,message:"Key must have length equal to the specified");
        }

        [TestMethod()]
        public void GenerateKeyNotEqualSequentialTest()
        {
            Crypto hmacGenerator = new();
            byte[] key1= hmacGenerator.Key;
            byte[] key2 = hmacGenerator.GenerateKey();
            Assert.AreNotEqual(key1, key2, message: "Keys must differ for two sequential key generations");
        }
    }
}