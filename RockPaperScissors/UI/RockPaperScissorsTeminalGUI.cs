﻿using NStack;
using Terminal.Gui;

namespace RockPaperScissors.UI
{
    public class RockPaperScissorsTeminalGUI : RockPaperScissorsBaseUI
    {

        //UI elements for Terminal.Gui
        private Label HMACLabel = new Label(hmacLabelString);
        private Label selectPromptLabel = new Label(selectUserString);
        private ustring[] listOptsLabels = new ustring[1];
        private Button btnProcessResults = new Button("I made my choise");
        private Button btnNewGame = new Button("New game");
        private Label playerMoveTextLabel = new Label();
        private Label computerMoveTextLabel = new Label();
        private Label winnerLabel = new Label();
        private Label helpLinkToCheckLabel = new Label(checkHMACUrl);
        private Label keyLabel = new Label(hmacKeyLabelString);
        private RadioGroup radOptions = new RadioGroup();

        private void InitNewGame()
        {
            gameProcessor.InitNewGame();
            HMACLabel.Text = hmacLabelString + gameProcessor.GetHMACHexString();

            //clear labels with information from previous game, hide some elements
            keyLabel.Text = "";
            winnerLabel.Text = "";
            computerMoveTextLabel.Text = "";
            playerMoveTextLabel.Text = "";
            btnProcessResults.IsDefault = true;
            btnProcessResults.Enabled = true;
            btnNewGame.IsDefault = false;
            helpLinkToCheckLabel.Visible = false;

            radOptions.SetFocus();//keyboard focus must be at radiobutton control to enable hotkeys for the user immediately
        }

        /// <summary>
        /// Update of UI after user move
        /// </summary>
        private void processResults()
        {
            string winnerText = "";
            if (radOptions.SelectedItem > -1 && radOptions.SelectedItem < gameProcessor.options.Length)
            {
                int userMove = radOptions.SelectedItem;
                playerMoveTextLabel.Text = yourMoveString + gameProcessor.options[userMove];

                Winner winner = gameProcessor.DetermineWinner(userMove);

                winnerText = GetWinnerString(winner);
            }
            else
            {
                if (radOptions.SelectedItem == gameProcessor.options.Length)//exit (item "0 - exit" is located after usual options)
                {
                    Application.RequestStop();
                }
                else
                {
                    if (radOptions.SelectedItem == gameProcessor.options.Length + 1)//help
                    {
                        ShowHelpTable();
                        return;
                    }
                    else // can't happen, but it is better to reinsure
                    {
                        winnerText = incorrectInputString;
                    }
                }
            }
            keyLabel.Text = hmacKeyLabelString + gameProcessor.GetKeyHexSting();
            computerMoveTextLabel.Text = computerMoveString + gameProcessor.options[gameProcessor.ComputerMove];
            winnerLabel.Text = winnerText;

            //Change default button (the button which is activated by pressing enter by default), change visibility
            helpLinkToCheckLabel.Visible = true;
            btnProcessResults.IsDefault = false;
            btnProcessResults.Enabled = false;
            btnNewGame.IsDefault = true;
        }

        public override void Run(string[] _options)
        {
            gameProcessor.options = _options;

            Application.Init();

            HMACLabel.Text = hmacLabelString;
            selectPromptLabel.Y = Pos.Bottom(HMACLabel);

            listOptsLabels = new ustring[gameProcessor.options.Length + 2];
            for (int optIndex = 0; optIndex < gameProcessor.options.Length; optIndex++)
            {
                string numberStr = (optIndex + 1).ToString();
                if (optIndex < 9)
                {
                    numberStr = "_" + numberStr;//"_" symbol is mark for hotkeys in Terminal.Gui
                }

                string optionText = numberStr + " - " + gameProcessor.options[optIndex];
                listOptsLabels[optIndex] = optionText;
            }
            listOptsLabels[gameProcessor.options.Length] = "_0 - exit";
            listOptsLabels[gameProcessor.options.Length + 1] = "_h - help";

            radOptions = new RadioGroup(listOptsLabels)
            {
                Y = Pos.Bottom(selectPromptLabel)
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

            Application.Top.Add(HMACLabel, selectPromptLabel, radOptions, btnProcessResults, btnNewGame, playerMoveTextLabel, computerMoveTextLabel, winnerLabel, keyLabel, helpLinkToCheckLabel);
            InitNewGame();

            Application.Run();
            Application.Shutdown();
        }
    }
}
