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
    public class GameClass
    {
        public void GameProcess(string userMove, int optionsCount)
        {
            int computerMove = RandomNumberGenerator.GetInt32(0, optionsCount + 1);

            byte[] hmac = crypto.GenerateHMAC(userMove);
            Console.WriteLine(Convert.ToHexString(hmac));
        }


        private Crypto crypto = new Crypto();
        private string[] options = { "Rock", "Paper", "Scissors" };

        private Label HMACLabel = new Label();

        private Label usernameLabel = new Label("Select your choise: ");

        private ustring[] listOpts = new ustring[1];
        private Button btnLogin = new Button("I made my choise");
        private Button btnNewGame = new Button("New game");
        private Label playerMoveText = new Label();
        private Label computerMoveText = new Label();
        private Label winner = new Label();
        private Label helpLinkToCheck = new Label("You can check HMAC here: https://dinochiesa.github.io/hmachash/index.html. Selection of the text at screen is performed by the mouse when shift is held");
        private Label key = new Label();
        private RadioGroup rad = new RadioGroup();

        private int computerMove = -1;
        private int userMove = -1;
        private void InitNewGame()
        {
            crypto.GenerateKey();
            computerMove = RandomNumberGenerator.GetInt32(0, options.Length);
            HMACLabel.Text = Convert.ToHexString(crypto.GenerateHMAC(options[computerMove]));

            key.Text = "";
            winner.Text = "";
            computerMoveText.Text = "";
            playerMoveText.Text = "";
            btnLogin.IsDefault = true;
            btnLogin.Enabled = true;
            btnNewGame.IsDefault = false;
            rad.SetFocus();
        }

        private void processResults()
        {
            string winnerText = "";
            if (rad.SelectedItem > -1 && rad.SelectedItem < options.Length)
            {
                userMove = rad.SelectedItem;
                playerMoveText.Text = "Your move is:" + options[userMove];

                Winner winner = Rules.DetermineNumberOfWinner(computerMove, userMove, options.Length);
                winnerText = winner switch
                {
                    Winner.FirstPlayerWin => "You lose!",
                    Winner.SecondPlayerWin => "You win!",
                    _ => "Draw!"
                };
            }
            else
            {
                if (rad.SelectedItem == options.Length)
                {
                    Application.RequestStop();
                }
                else
                {
                    if (rad.SelectedItem == options.Length + 1)
                    {
                        var dialog = new Dialog("Help");

                        var helpDialogLabel = new Label();
                        helpDialogLabel.Text = "Values in cells give result for the player with moves specified in head of table";

                        DataTable dataTable = new DataTable("Help");

                        dataTable.Columns.Add(new DataColumn("Moves", typeof(string)));

                        for (int i = 0;i < options.Length;i++)
                        {
                            dataTable.Columns.Add(new DataColumn(options[i], typeof(string)));
                        }

                        for (int i = 0; i < options.Length; i++)//to the down
                        {
                            object[] row = new object[options.Length+1];
                            row[0] = options[i];
                            for (int j = 0; j < options.Length; j++)//to the right
                            {
                                Winner winner=Rules.DetermineNumberOfWinner(i, j, options.Length);
                                row[j + 1] = winner switch
                                {
                                    Winner.FirstPlayerWin => "Lose",
                                    Winner.SecondPlayerWin => "Win",
                                    _ => "Draw"
                                };
                            }
                            dataTable.Rows.Add(row);
                        }


                        TableView table = new TableView();
                        table = new TableView()
                        {
                            X = 0,
                            Y = Pos.Bottom(helpDialogLabel),
                            Width = Dim.Fill(),
                            Height = Dim.Fill(),
                        };
                        table.Table = dataTable;

                        dialog.Add(helpDialogLabel);
                        dialog.Add(table);

                        var ok = new Button(3, 14, "Ok");
                        ok.Clicked += () => { Application.RequestStop(); };
                        ok.Y = Pos.Bottom(table);

                        dialog.Add(ok);
                        Application.Run(dialog);
                        return;
                    }
                    else
                    {
                        winnerText = "Incorrect selection of move!";
                    }
                }
            }
            key.Text = Convert.ToHexString(crypto.Key);
            computerMoveText.Text = "Computer move is: " + options[computerMove];

            winner.Text = winnerText;
            btnLogin.IsDefault = false;
            btnLogin.Enabled = false;
            btnNewGame.IsDefault = true;
        }

public void Run(string[] _options)
        {
            options = _options;

            Application.Init();

            HMACLabel.Text = "";
            usernameLabel.Y = Pos.Bottom(HMACLabel);

            listOpts = new ustring[options.Length + 2];
            for (int optIndex = 0; optIndex < options.Length; optIndex++)
            {
                string numberStr = (optIndex + 1).ToString();
                if (optIndex < 9)
                {
                    numberStr = "_" + numberStr;//_ marks for hotkeys
                }

                string optionText = numberStr + " - " + options[optIndex];
                listOpts[optIndex] = optionText;
            }
            listOpts[options.Length] = "_0 - exit";
            listOpts[options.Length+1] = "_h - help";

            rad = new RadioGroup(listOpts)
            {
                Y = Pos.Bottom(usernameLabel)
            };

            btnLogin.Y = Pos.Bottom(rad) + 1;
            btnLogin.IsDefault = true;

            btnNewGame.Y = Pos.Bottom(rad) + 1;
            btnNewGame.X = Pos.Right(btnLogin) + 1;

            playerMoveText.Y = Pos.Bottom(btnNewGame) + 1;

            computerMoveText.Y = Pos.Bottom(playerMoveText) + 1;

            winner.Y = Pos.Bottom(computerMoveText) + 1;

            key.Y = Pos.Bottom(winner) + 1;

            helpLinkToCheck.Y = Pos.Bottom(key) + 1;

            btnLogin.Clicked += processResults;

            btnNewGame.Clicked += InitNewGame;

            Application.Top.Add(HMACLabel, usernameLabel, rad, btnLogin, btnNewGame, playerMoveText, computerMoveText, winner, key, helpLinkToCheck);
            InitNewGame();
            Application.Run();
            Application.Shutdown();
        }
    }
}
