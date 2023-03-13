using System.Data;
using Terminal.Gui;

namespace RockPaperScissors.UI
{
    public abstract class RockPaperScissorsBaseUI
    {
        //strings for UI
        protected static readonly string checkHMACUrl = "You can check HMAC here: https://dinochiesa.github.io/hmachash/index.html";
        protected static readonly string yourMoveString = "Your move: ";
        protected static readonly string computerMoveString = "Computer move: ";
        protected static readonly string incorrectInputString = "Incorrect input, try again";
        protected static readonly string hmacLabelString = "HMAC: ";
        protected static readonly string hmacKeyLabelString = "HMAC key: ";
        protected static readonly string selectUserString = "Select your choise: ";

        protected RockPaperScissorsProcessor gameProcessor = new RockPaperScissorsProcessor();

        /// <summary>
        /// Displays table of win/lose/draw using Terminal.Gui library
        /// </summary>
        protected void ShowHelpTable()
        {
            var helpDialog = new Dialog("Help");

            var helpDialogLabel = new Label();
            helpDialogLabel.Text = "Values in cells give result for the player with moves specified in the head of the table";

            TableView table = new TableView();
            int tableHeight_chars = 18;
            table = new TableView()
            {
                X = 0,
                Y = Pos.Bottom(helpDialogLabel),
                Width = Dim.Fill(),//TODO: couldn't wind a way to prevent expanding of table for small number of options
                Height = tableHeight_chars,
            };

            DataTable dataTable = TableWinLoseGenerator.GenerateTableWinLose(gameProcessor.options);
            table.Table = dataTable;

            helpDialog.Add(helpDialogLabel);
            helpDialog.Add(table);

            var ok = new Button(x: 3, y: tableHeight_chars + 2, text: "Ok");//sizes are specified in chars
            ok.Clicked += () => { Application.RequestStop(); };

            helpDialog.Add(ok);
            Application.Run(helpDialog);
        }

        /// <summary>
        /// It is assumed that the first player is computer
        /// </summary>
        /// <param name="winner">value of the corresponding enum</param>
        /// <returns></returns>
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

        /// <summary>
        /// Running of the UI (start of the repeated game)
        /// </summary>
        /// <param name="_options">options specified by the user. Must be verified by the external code</param>
        public abstract void Run(string[] _options);
    }
}
