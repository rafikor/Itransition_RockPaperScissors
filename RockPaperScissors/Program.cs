using RockPaperScissors;
using System.Security.Cryptography;

namespace RockPaperScissors
{
    public class Run
    {
        
        static void Main(string[] args)
        {
            if (args.Length > 2 && args.Length % 2 == 1)
            {
                RockPaperScissorsUI gameApp = new();
                gameApp.Run(args);
            }
            else
            {
                //TODO
                Console.WriteLine("Wrong options!");
            }
        }
    }

    
}