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
        RockPaperScissorsProcessor gameProcessor = new RockPaperScissorsProcessor();

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

        private void InitNewGame()
        {

            HMACLabel.Text = gameProcessor.GetHMACHexString();

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
            if (radOptions.SelectedItem > -1 && radOptions.SelectedItem < gameProcessor.options.Length)
            {
                int userMove = radOptions.SelectedItem;
                playerMoveTextLabel.Text = "Your move is:" + gameProcessor.options[userMove];

                Winner winner = gameProcessor.ProcessResults(userMove);

                winnerText = winner switch
                {
                    Winner.FirstPlayerWin => "You lose!",
                    Winner.SecondPlayerWin => "You win!",
                    _ => "Draw!"
                };
            }
            else
            {
                if (radOptions.SelectedItem == gameProcessor.options.Length)
                {
                    Application.RequestStop();
                }
                else
                {
                    if (radOptions.SelectedItem == gameProcessor.options.Length + 1)
                    {
                        var dialog = new Dialog("Help");

                        var helpDialogLabel = new Label();
                        helpDialogLabel.Text = "Values in cells give result for the player with moves specified in head of table";

                        DataTable dataTable = TableWinLoseGenerator.GenerateTableWinLose(gameProcessor.options);

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
            keyLabel.Text = gameProcessor.GetKeyHexSting();
            computerMoveTextLabel.Text = "Computer move is: " + gameProcessor.options[gameProcessor.ComputerMove];

            winnerLabel.Text = winnerText;
            btnProcessResults.IsDefault = false;
            btnProcessResults.Enabled = false;
            btnNewGame.IsDefault = true;
        }

        public void Run(string[] _options)
        {
            gameProcessor.options = _options;

            Application.Init();

            HMACLabel.Text = "";
            usernameLabel.Y = Pos.Bottom(HMACLabel);

            listOptsLabels = new ustring[gameProcessor.options.Length + 2];
            for (int optIndex = 0; optIndex < gameProcessor.options.Length; optIndex++)
            {
                string numberStr = (optIndex + 1).ToString();
                if (optIndex < 9)
                {
                    numberStr = "_" + numberStr;//_ marks for hotkeys
                }

                string optionText = numberStr + " - " + gameProcessor.options[optIndex];
                listOptsLabels[optIndex] = optionText;
            }
            listOptsLabels[gameProcessor.options.Length] = "_0 - exit";
            listOptsLabels[gameProcessor.options.Length + 1] = "_h - help";

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
