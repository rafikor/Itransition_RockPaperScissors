using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        //To validate, use https://dinochiesa.github.io/hmachash/index.html
        [TestMethod()]
        public void GenerateHMACFixedKeyTest()
        {
            Crypto hmacGenerator = new();
            string key = "C4AF0EC4DDDC63215C0A0FBC706E3ECE224EBDBCCF0DFC42FF7A995DC2DD0763";
            hmacGenerator.Key = Convert.FromHexString(key);
            string message = "Rock";
            byte[] hmac=hmacGenerator.GenerateHMAC(message);
            string hmacStringHex = Convert.ToHexString(hmac).ToLower();
            Assert.AreEqual(hmacStringHex, "38ffb3e657e52a6d0bcb5b2c42feef57388862e67276ba400bcd77c47fb72a01", "Calculated HMAC with fixed key must coincide with computation according to standard implementation https://dinochiesa.github.io/hmachash/index.html");
        }
    }
}