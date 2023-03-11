using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{   
    public class Crypto
    {
        public static readonly int keylength = 256 / 8;
        public byte[] Key { get; set; }
        public Crypto() => GenerateKey();
        public byte[] GenerateKey() 
        {
            Key=RandomNumberGenerator.GetBytes(keylength);
            return Key; 
        }
        //TODO:implement
        public uint GenerateHMAC(int move) { return 0; }
    }
}
