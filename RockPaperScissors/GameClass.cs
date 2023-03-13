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


        private Crypto crypto = new();
        private string[] options;

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
            computerMove = RandomNumberGenerator.GetInt32(0, options.Length);
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
                    Winner.FirstPlayerWin => "You loose!",
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

                        //Help
                        /*

                        var cancel = new Button(10, 14, "Cancel");
                        cancel.Clicked += () => Application.RequestStop();

                        var dialog = new Dialog("Login", 60, 18, ok, cancel);
                        var entry = new TextField()
                        {
                            X = 1,
                            Y = 1,
                            Width = Dim.Fill(),
                            Height = 1
                        };*/
                        
                        var dialog = new Dialog("Help");

                        DataTable dataTable= new DataTable("Help");

                        dataTable.Columns.Add(new DataColumn("", typeof(string)));

                        for (int i = 0;i < options.Length;i++)
                        {
                            dataTable.Columns.Add(new DataColumn(options[i], typeof(string)));
                        }

                        for (int i = 0; i < options.Length; i++)
                        {
                            object[] row = new object[options.Length+1];
                            row[0] = options[i];
                            for (int j = 0; j < options.Length; j++)
                            {
                                Winner winner=Rules.DetermineNumberOfWinner(i, j, options.Length);
                                row[j + 1] = winner switch
                                {
                                    Winner.FirstPlayerWin => options[i],
                                    Winner.SecondPlayerWin => options[j],
                                    _ => "Draw"
                                };
                            }
                            dataTable.Rows.Add(row);
                        }


                        TableView table = new TableView();
                        table = new TableView()
                        {
                            X = 0,
                            Y = 0,
                            Width = Dim.Fill(),
                            Height = Dim.Fill(),
                        };
                        table.Table = dataTable;

                        dialog.Add(table);

                        var ok = new Button(3, 14, "Ok");
                        ok.Clicked += () => { Application.RequestStop(); };
                        //ok.IsDefault = true;
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
            computerMoveText.Text = "Computer move is:" + options[computerMove];

            winner.Text = winnerText;
            btnLogin.IsDefault = false;
            btnLogin.Enabled = false;
            btnNewGame.IsDefault = true;
        }

public void Run(string[] _options)
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
                Text = "You can check HMAC here: https://dinochiesa.github.io/hmachash/index.html. Selection of the text at screen is performed by the mouse when shift is held",
                Y = Pos.Bottom(key) + 1
            };

            btnLogin.Clicked += processResults;


            btnNewGame.Clicked += InitNewGame;

            Application.Top.Add(HMAC, usernameLabel, rad, btnLogin, btnNewGame, playerMoveText, computerMoveText, winner, key, helpLinkToCheck);
            InitNewGame();
            Application.Run();
            Application.Shutdown();
        }
    }
}
