using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class RockPaperScissorsProcessor
    {
        private Crypto crypto = new Crypto();
        public string[] options = { "Rock", "Paper", "Scissors" };
        public int ComputerMove { get; private set; }
        private int userMove = -1;

        public void InitNewGame()
        {
            crypto.GenerateKey();
            ComputerMove = RandomNumberGenerator.GetInt32(0, options.Length);
        }

        public string GetHMACHexString()
        {
            return Convert.ToHexString(crypto.GenerateHMAC(options[ComputerMove]));
        }

        public string GetKeyHexSting()
        {
            return Convert.ToHexString(crypto.Key);
        }

        public Winner ProcessResults(int userMoveExt)
        {
            userMove = userMoveExt;
            Winner winner = Rules.DetermineNumberOfWinner(ComputerMove, userMove, options.Length);
            return winner;
        }
    }
}
