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
                RockPaperScissorsBaseUI gameApp;
                if (args.Length <= 15)
                {
                    gameApp = new RockPaperScissorsTeminalGUI();
                }
                else 
                {
                    gameApp = new RockPaperScissorsDefaultUI();
                }
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