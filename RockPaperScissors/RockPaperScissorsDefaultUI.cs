using Core.Measurement;
using NStack;
using RockPaperScissors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Terminal.Gui;

namespace RockPaperScissors
{
    public class RockPaperScissorsDefaultUI : RockPaperScissorsBaseUI
    {
        private readonly string helpChoiseString = "?";
        public void displayMenu()
        {
            Console.WriteLine(hmacLabelString + gameProcessor.GetHMACHexString());
            Console.WriteLine(selectUserString);
            for (int move = 0; move < gameProcessor.options.Length; move++)
            {
                Console.WriteLine($"{move} - {gameProcessor.options[move]}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine(helpChoiseString + " - help");
            Console.Write("Enter your move: ");
        }

        public override void Run(string[] _options)
        {
            gameProcessor.options = _options;

            while (true)
            {
                displayMenu();
                var input = Console.ReadLine();
                int userChoise;
                if (int.TryParse(input, out userChoise))
                {
                    if (userChoise >= 0 && userChoise < gameProcessor.options.Length)//correct input
                    {
                        if (userChoise == 0)
                        {
                            Console.WriteLine("Exiting application...");
                            return;
                        }
                        Console.WriteLine(yourMoveString + gameProcessor.options[userChoise]);
                        Console.WriteLine(computerMoveString + gameProcessor.options[gameProcessor.ComputerMove]);
                        Winner winner = gameProcessor.ProcessResults(userChoise);

                        string winnerText = GetWinnerString(winner);

                        Console.WriteLine(winnerText);
                        Console.WriteLine(hmacKeyLabelString + gameProcessor.GetKeyHexSting());
                        Console.WriteLine(checkHMACUrl);
                        Console.WriteLine("---------------------------------------------------------------------\n");

                        gameProcessor.InitNewGame();
                    }
                    else
                    {
                        Console.WriteLine(incorrectInputString);
                    }
                }
                else
                {
                    if (input == helpChoiseString)
                    {
                        Application.Init();
                        ShowHelpTable();
                        Application.Shutdown();
                    }
                    else
                    {
                        Console.WriteLine(incorrectInputString);
                    }
                }
            }
        }
    }
}
