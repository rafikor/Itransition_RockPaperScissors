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
        private ustring[] listOptsLabels = new ustring[1];
        private Button btnProcessResults = new Button("I made my choise");
        private Button btnNewGame = new Button("New game");
        private Label playerMoveTextLabel = new Label();
        private Label computerMoveTextLabel = new Label();
        private Label winnerLabel = new Label();
        private Label helpLinkToCheckLabel = new Label("You can check HMAC here: https://dinochiesa.github.io/hmachash/index.html. Selection of the text at screen is performed by the mouse when shift is held");
        private Label keyLabel = new Label();
        private RadioGroup radOptions = new RadioGroup();

        private int computerMove = -1;
        private int userMove = -1;
        private void InitNewGame()
        {
            crypto.GenerateKey();
            computerMove = RandomNumberGenerator.GetInt32(0, options.Length);
            HMACLabel.Text = Convert.ToHexString(crypto.GenerateHMAC(options[computerMove]));

            keyLabel.Text = "";
            winnerLabel.Text = "";
            computerMoveTextLabel.Text = "";
            playerMoveTextLabel.Text = "";
            btnProcessResults.IsDefault = true;
            btnProcessResults.Enabled = true;
            btnNewGame.IsDefault = false;
            radOptions.SetFocus();
        }

        private void processResults()
        {
            string winnerText = "";
            if (radOptions.SelectedItem > -1 && radOptions.SelectedItem < options.Length)
            {
                userMove = radOptions.SelectedItem;
                playerMoveTextLabel.Text = "Your move is:" + options[userMove];

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
                if (radOptions.SelectedItem == options.Length)
                {
                    Application.RequestStop();
                }
                else
                {
                    if (radOptions.SelectedItem == options.Length + 1)
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
            keyLabel.Text = Convert.ToHexString(crypto.Key);
            computerMoveTextLabel.Text = "Computer move is: " + options[computerMove];

            winnerLabel.Text = winnerText;
            btnProcessResults.IsDefault = false;
            btnProcessResults.Enabled = false;
            btnNewGame.IsDefault = true;
        }

public void Run(string[] _options)
        {
            options = _options;

            Application.Init();

            HMACLabel.Text = "";
            usernameLabel.Y = Pos.Bottom(HMACLabel);

            listOptsLabels = new ustring[options.Length + 2];
            for (int optIndex = 0; optIndex < options.Length; optIndex++)
            {
                string numberStr = (optIndex + 1).ToString();
                if (optIndex < 9)
                {
                    numberStr = "_" + numberStr;//_ marks for hotkeys
                }

                string optionText = numberStr + " - " + options[optIndex];
                listOptsLabels[optIndex] = optionText;
            }
            listOptsLabels[options.Length] = "_0 - exit";
            listOptsLabels[options.Length+1] = "_h - help";

            radOptions = new RadioGroup(listOptsLabels)
            {
                Y = Pos.Bottom(usernameLabel)
            };

            btnProcessResults.Y = Pos.Bottom(radOptions) + 1;
            btnProcessResults.IsDefault = true;

            btnNewGame.Y = Pos.Bottom(radOptions) + 1;
            btnNewGame.X = Pos.Right(btnProcessResults) + 1;

            playerMoveTextLabel.Y = Pos.Bottom(btnNewGame) + 1;

            computerMoveTextLabel.Y = Pos.Bottom(playerMoveTextLabel) + 1;

            winnerLabel.Y = Pos.Bottom(computerMoveTextLabel) + 1;

            keyLabel.Y = Pos.Bottom(winnerLabel) + 1;

            helpLinkToCheckLabel.Y = Pos.Bottom(keyLabel) + 1;

            btnProcessResults.Clicked += processResults;

            btnNewGame.Clicked += InitNewGame;

            Application.Top.Add(HMACLabel, usernameLabel, radOptions, btnProcessResults, btnNewGame, playerMoveTextLabel, computerMoveTextLabel, winnerLabel, keyLabel, helpLinkToCheckLabel);
            InitNewGame();
            Application.Run();
            Application.Shutdown();
        }
    }
}
