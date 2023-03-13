using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace RockPaperScissors
{
    public abstract class RockPaperScissorsBaseUI
    {
        protected static readonly string checkHMACUrl = "You can check HMAC here: https://dinochiesa.github.io/hmachash/index.html";
        protected static readonly string yourMoveString = "Your move: ";
        protected static readonly string computerMoveString = "Computer move: ";
        protected static readonly string incorrectInputString = "Incorrect input, try again";
        protected static readonly string hmacLabelString = "HMAC: ";
        protected static readonly string hmacKeyLabelString = "HMAC key: ";
        protected static readonly string selectUserString = "Select your choise: ";

        protected RockPaperScissorsProcessor gameProcessor = new RockPaperScissorsProcessor();
        protected void ShowHelpTable()
        {
            var dialog = new Dialog("Help");

            var helpDialogLabel = new Label();
            helpDialogLabel.Text = "Values in cells give result for the player with moves specified in the head of the table";

            DataTable dataTable = TableWinLoseGenerator.GenerateTableWinLose(gameProcessor.options);

            TableView table = new TableView();
            table = new TableView()
            {
                X = 0,
                Y = Pos.Bottom(helpDialogLabel),
                Width = Dim.Fill(),
                Height = 18,
            };

            table.Table = dataTable;
            dialog.Add(helpDialogLabel);
            dialog.Add(table);


            var ok = new Button(3, 20, "Ok");
            ok.Clicked += () => { Application.RequestStop(); };
            ok.X = 2;
            ok.Y = Pos.Bottom(table) + 60;

            dialog.Add(ok);
            Application.Run(dialog);
        }

        protected string GetWinnerString(Winner winner)
        {
            string winnerText = winner switch
            {
                Winner.FirstPlayerWin => "You lose!",
                Winner.SecondPlayerWin => "You win!",
                _ => "Draw!"
            };
            return winnerText;
        }

        public abstract void Run(string[] _options);
    }
}
