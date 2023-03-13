using RockPaperScissors;
using System.Security.Cryptography;

namespace RockPaperScissors
{
    public class Run
    {
        
        static void Main(string[] args)
        {
            
            var options = new List<string>();
            options.Add("Stone");
            options.Add("Scissors");
            options.Add("Paper");

            GameClass gameApp = new();
            gameApp.Run(options);
        }
    }

    
}