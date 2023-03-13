using NStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class TableWinLoseGenerator
    {
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
