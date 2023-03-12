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
        /// <summary>
        /// Length of the key. Default value is 32 bytes (256 bits).
        /// Minimum value is 32 bytes (256 bits), maximum is 128 bytes (1024 bits)
        /// Key is updated when length is changed
        /// </summary>
        public int KeyLength
        {
            get
            {
                return keyLength;
            }
            set
            {
                if (value >= 256 / 8 && value <= 1024 / 8)
                {   
                    keyLength = value;
                    GenerateKey();
                }
                //else TODO: throw exception?
            }
        }
        private int keyLength;
        public byte[] Key { get; private set; }
        public Crypto()
        {
            KeyLength = 256 / 8;//256 bits by default
            GenerateKey();
        }
        public byte[] GenerateKey() 
        {
            Key=RandomNumberGenerator.GetBytes(KeyLength);
            return Key; 
        }
        //TODO:implement
        public uint GenerateHMAC(int move) { return 0; }
    }
}
