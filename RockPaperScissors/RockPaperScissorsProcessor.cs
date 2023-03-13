using System.Security.Cryptography;

namespace RockPaperScissors
{
    public class RockPaperScissorsProcessor
    {
        private Crypto crypto = new Crypto();
        public string[] options = { "Rock", "Paper", "Scissors" };
        public int ComputerMove { get; private set; }
        private int userMove = -1;

        /// <summary>
        /// Updates private key and calculates move of the computer
        /// </summary>
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

        public Winner DetermineWinner(int userMoveExt)
        {
            userMove = userMoveExt;
            Winner winner = Rules.DetermineNumberOfWinner(ComputerMove, userMove, options.Length);
            return winner;
        }
    }
}
