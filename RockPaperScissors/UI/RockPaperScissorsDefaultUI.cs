using Terminal.Gui;

namespace RockPaperScissors.UI
{
    public class RockPaperScissorsDefaultUI : RockPaperScissorsBaseUI
    {
        private readonly string helpChoiceString = "?"; //key to start help
        public void displayMenu()
        {
            Console.WriteLine(hmacLabelString + gameProcessor.GetHMACHexString());
            Console.WriteLine(selectUserString);
            for (int move = 0; move < gameProcessor.options.Length; move++)
            {
                Console.WriteLine($"{move + 1} - {gameProcessor.options[move]}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine(helpChoiceString + " - help");
            Console.Write("Enter your move: ");
        }

        public override void Run(string[] _options)
        {
            gameProcessor.options = _options;

            while (true)
            {
                displayMenu();

                //get and process user's input
                var input = Console.ReadLine();
                int userChoice;
                if (int.TryParse(input, out userChoice))
                {
                    if (userChoice >= 0 && userChoice < gameProcessor.options.Length)//correct input
                    {
                        if (userChoice == 0)
                        {
                            Console.WriteLine("Exiting application...");
                            return;
                        }
                        userChoice -= 1; //options in code are numbered starting from 0
                        Console.WriteLine(yourMoveString + gameProcessor.options[userChoice]);
                        Console.WriteLine(computerMoveString + gameProcessor.options[gameProcessor.ComputerMove]);
                        Winner winner = gameProcessor.DetermineWinner(userChoice);

                        string winnerText = GetWinnerString(winner);

                        Console.WriteLine(winnerText);
                        Console.WriteLine(hmacKeyLabelString + gameProcessor.GetKeyHexSting());
                        Console.WriteLine(checkHMACUrl);
                        Console.WriteLine("Press any key to continue");
                        var notUsed = Console.ReadKey(true);
                        Console.WriteLine("---------------------------------------------------------------------\n");

                        gameProcessor.InitNewGame();//game must restart only after successfull move of the user
                    }
                    else
                    {
                        Console.WriteLine(incorrectInputString);
                    }
                }
                else//input is not number
                {
                    if (input == helpChoiceString)
                    {
                        Application.Init();
                        ShowHelpTable();
                        Application.Shutdown();
                    }
                    else//input is not number and not help key
                    {
                        Console.WriteLine(incorrectInputString);
                    }
                }
            }
        }
    }
}
