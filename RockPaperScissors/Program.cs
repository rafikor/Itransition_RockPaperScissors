using RockPaperScissors;
using System.Security.Cryptography;

namespace RockPaperScissors
{
    public class Run
    {
        
        static void Main(string[] args)
        {
            const string errorMessageBeginning = "Error in input data: ";
            const string exampleMessage = "Example of correct usage: RockPaperScissors.exe Rock Paper Scissors";
            if (args.Length > 2)
            {
                if (args.Length % 2 == 1)
                {
                    //check for uniqueness of input data
                    var setToTestUniqueness = new HashSet<string>();
                    for (int i = 0; i < args.Length; i++)
                    {
                        setToTestUniqueness.Add(args[i]);
                        if (setToTestUniqueness.Count != i + 1)
                        {
                            Console.WriteLine(errorMessageBeginning + "Options must be unique, option " + args[i] + " is repeated.");
                            Console.WriteLine(exampleMessage);
                            return;
                        }
                    }

                    RockPaperScissorsBaseUI gameApp;
                    if (args.Length <= 15)//UI based on Terminal.Gui must be scaled manually for big number of options, it is uncomfortable
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
                    Console.WriteLine(errorMessageBeginning + "Number of options must be odd.");
                    Console.WriteLine(exampleMessage);
                }
            }
            else
            {
                Console.WriteLine(errorMessageBeginning + "Number of options must be more than 2.");
                Console.WriteLine(exampleMessage);
            }
        }
    }

    
}