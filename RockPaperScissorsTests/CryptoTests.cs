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
            Crypto mock=new ();
            Assert.AreEqual(mock.Key.Length, Crypto.keylength,message:"Key must have length equal to the specified");
        }

        [TestMethod()]
        public void GenerateKeyNotEqualSequentialTest()
        {
            Crypto mock = new();
            byte[] key1=mock.Key;
            byte[] key2 = mock.GenerateKey();
            Assert.AreNotEqual(key1, key2, message: "Keys must differ for two sequential key generations");
        }
    }
}