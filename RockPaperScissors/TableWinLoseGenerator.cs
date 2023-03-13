using System.Data;

namespace RockPaperScissors
{
    public class TableWinLoseGenerator
    {
        /// <summary>
        /// Values in cells are relative to the moves in header of the table (i.e. cell with value "Win" means that move specified in head of given column wins agains move given in first cell of given row)
        /// </summary>
        /// <param name="options">names of possible options for moves</param>
        /// <returns></returns>
        public static DataTable GenerateTableWinLose(string[] options)
        {
            DataTable dataTable = new DataTable("Help");

            dataTable.Columns.Add(new DataColumn("Moves", typeof(string)));

            for (int i = 0; i < options.Length; i++)
            {
                dataTable.Columns.Add(new DataColumn(options[i], typeof(string)));
            }

            for (int i = 0; i < options.Length; i++)//to the down
            {
                object[] row = new object[options.Length + 1];
                row[0] = options[i];
                for (int j = 0; j < options.Length; j++)//to the right
                {
                    Winner winner = Rules.DetermineNumberOfWinner(i, j, options.Length);
                    row[j + 1] = winner switch
                    {
                        Winner.FirstPlayerWin => "Lose",
                        Winner.SecondPlayerWin => "Win",
                        _ => "Draw"
                    };
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
