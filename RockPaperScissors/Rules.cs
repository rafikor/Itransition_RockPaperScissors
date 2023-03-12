using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public enum Winner { FirstPlayerWin, SecondPlayerWin, Draw };
    public static class Rules
    {
        /// <summary>
        /// Method determines who is winner based on numbers of players' moves and total number of options for moves
        /// All numbers counted from zero (i.e. method can return 0 when the first player wins and 1 when the second player wins)
        /// In case of draw, -1 is returned
        /// </summary>
        /// <param name="moveFirstPlayer">counted from zero</param>
        /// <param name="moveSecondPlayer">counted from zero</param>
        /// <param name="optionsCount">count of possible options</param>
        /// <returns></returns>
        public static Winner DetermineNumberOfWinner(int moveFirstPlayer, int moveSecondPlayer, int optionsCount)
        {
            //check input arguments
            if (optionsCount<1 || optionsCount%2==0)
            {
                throw new ArgumentException("Incorrect value of optionsCount argument");
            }
            if (moveFirstPlayer < 0 || moveFirstPlayer >= optionsCount || moveSecondPlayer < 0 || moveSecondPlayer >= optionsCount)
            {
                throw new ArgumentException("Incorrect value(s) of players' movements");
            }

            //negative value if closest way from move number of the 1st player to the move number of the 2nd player is by decreasing
            int minimalDiff = moveSecondPlayer - moveFirstPlayer;
            int halfOfCircle = optionsCount / 2;
            if (minimalDiff > halfOfCircle)
            {
                minimalDiff -= optionsCount;
            }
            else
            {
                if (minimalDiff < -halfOfCircle)
                {
                    minimalDiff += optionsCount;
                }
            }
            
            Winner NumberOfWinner = Winner.Draw;
            if (minimalDiff > 0)
            {
                NumberOfWinner = Winner.SecondPlayerWin;
            }
            else
            {
                if (minimalDiff < 0)
                {
                    NumberOfWinner = Winner.FirstPlayerWin;
                }
                else
                {
                    NumberOfWinner = Winner.Draw;
                }
            }
            return NumberOfWinner;
        }
    }
}