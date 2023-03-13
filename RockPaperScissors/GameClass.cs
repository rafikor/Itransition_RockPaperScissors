using NStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace RockPaperScissors
{
    public class GameClass
    {
        public void GameProcess(string userMove, int optionsCount)
        {
            int computerMove = RandomNumberGenerator.GetInt32(0, optionsCount + 1);

            byte[] hmac = crypto.GenerateHMAC(userMove);
            Console.WriteLine(Convert.ToHexString(hmac));
        }


        private Crypto crypto = new();
        private List<string> options;

        private Label HMAC;

        private Label usernameLabel;

        private ustring[] listOpts;
        private Button btnLogin;
        private Button btnNewGame;
        private Label playerMoveText;
        private Label computerMoveText;
        private Label winner;
        private Label helpLinkToCheck;
        private Label key;
        private RadioGroup rad;

        private int computerMove = -1;
        private int userMove = -1;
        private void InitNewGame()
        {
            crypto.GenerateKey();
            computerMove = RandomNumberGenerator.GetInt32(0, options.Count);
            HMAC.Text = Convert.ToHexString(crypto.GenerateHMAC(options[computerMove]));

            key.Text = "";
            winner.Text = "";
            computerMoveText.Text = "";
            playerMoveText.Text = "";
            btnLogin.IsDefault = true;
            btnLogin.Enabled = true;
            btnNewGame.IsDefault = false;
            rad.SetFocus();
        }

        public void Run(List<string> _options)
        {
            options = _options;

            Application.Init();

            HMAC = new Label()
            {
                Text = ""
            };

            // Create input components and labels
            usernameLabel = new Label()
            {
                Text = "Enter your choise:",
                Y = Pos.Bottom(HMAC)
            };

            listOpts = new ustring[options.Count];
            for (int optIndex = 0; optIndex < options.Count; optIndex++)
            {
                string numberStr = (optIndex + 1).ToString();
                if (optIndex < 9)
                {
                    numberStr = "_" + numberStr;//_ marks for hotkeys
                }
                if (optIndex == 9)
                {
                    numberStr = "1_0";//0 is hotkey for 10
                }

                string optionText = numberStr + " " + options[optIndex];
                listOpts[optIndex] = optionText;
            }

            rad = new RadioGroup(listOpts)
            {
                Y = Pos.Bottom(usernameLabel)
            };


            // Create login button
            btnLogin = new Button()
            {
                Text = "I made my choise",
                Y = Pos.Bottom(rad) + 1,
                // center the login button horizontally
                //X = Pos.Center(),
                IsDefault = true,
            };

            // Create login button
            btnNewGame = new Button()
            {
                Text = "New game",
                Y = Pos.Bottom(rad) + 1,
                // center the login button horizontally
                X = Pos.Right(btnLogin) + 1
            };

            playerMoveText = new Label()
            {
                Text = "",
                Y = Pos.Bottom(btnNewGame) + 1
            };

            computerMoveText = new Label()
            {
                Text = "",
                Y = Pos.Bottom(playerMoveText) + 1
            };


            winner = new Label()
            {
                Text = "",
                Y = Pos.Bottom(computerMoveText) + 1
            };

            key = new Label()
            {
                Text = "",
                Y = Pos.Bottom(winner) + 1
            };

            helpLinkToCheck = new Label()
            {
                Text = "You can check HMAC here: https://dinochiesa.github.io/hmachash/index.html",
                Y = Pos.Bottom(key) + 1
            };

            // When login button is clicked display a message popup
            btnLogin.Clicked += () => {
                key.Text = Convert.ToHexString(crypto.Key);
                string winnerText;
                computerMoveText.Text = "Computer move is:" + options[computerMove];
                if (rad.SelectedItem > -1 || rad.SelectedItem < options.Count)
                {
                    userMove = rad.SelectedItem;
                    playerMoveText.Text = "Your move is:" + options[userMove];

                    Winner winner = Rules.DetermineNumberOfWinner(computerMove, userMove, options.Count);
                    winnerText = winner switch
                    {
                        Winner.FirstPlayerWin => "You loose!",
                        Winner.SecondPlayerWin => "You win!",
                        _ => "Draw!"
                    };
                }
                else
                {
                    winnerText = "Incorrect selection of move!";
                }
                winner.Text = winnerText;
                btnLogin.IsDefault = false;
                btnLogin.Enabled = false;
                btnNewGame.IsDefault = true;
            };


            // When login button is clicked display a message popup
            btnNewGame.Clicked += InitNewGame;

            Application.Top.Add(HMAC, usernameLabel, rad, btnLogin, btnNewGame, playerMoveText, computerMoveText, winner, key);
            InitNewGame();
            Application.Run();
            Application.Shutdown();
        }
    }
}
