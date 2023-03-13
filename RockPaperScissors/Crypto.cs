using System.Security.Cryptography;
using Core;

namespace RockPaperScissors
{   
    /// <summary>
    /// Class to work with cryptography in the implementation of the Rock-Paper-Scissors game
    /// </summary>
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
            }
        }
        private int keyLength;

        public byte[] Key { get; set; }
        public Crypto()
        {
            KeyLength = 256 / 8;//256 bits by default
            GenerateKey();
        }
        public byte[] GenerateKey() 
        {
            Key = RandomNumberGenerator.GetBytes(KeyLength);
            return Key; 
        }
        
        public byte[] GenerateHMAC(string move) 
        {
            var HMACprovider = new HMACSHA256(Key);
            byte[] moveAsBytes = move.ToByteArray();
            byte[] resultHMAC = HMACprovider.ComputeHash(moveAsBytes);
            return resultHMAC;
        }
    }
}
