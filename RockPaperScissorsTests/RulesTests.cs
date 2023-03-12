using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests
{
    [TestClass()]
    public class RulesTests
    {
        [TestMethod()]
        public void DetermineNumberOfWinnerTestBasic()
        {
            int firstPlayerMove = 1;
            int secondlayerMove = 2;
            int numberOfMoveOptions = 3;

            Winner winner = Rules.DetermineNumberOfWinner(firstPlayerMove, secondlayerMove, numberOfMoveOptions);
            Assert.AreEqual(Winner.SecondPlayerWin,winner);
        }

        public void DetermineNumberOfWinnerTestBasic5()
        {
            int firstPlayerMove = 1;
            int secondlayerMove = 3;
            int numberOfMoveOptions = 5;

            Winner winner = Rules.DetermineNumberOfWinner(firstPlayerMove, secondlayerMove, numberOfMoveOptions);
            Assert.AreEqual(Winner.SecondPlayerWin, winner);
        }

        [TestMethod()]
        public void DetermineNumberOfWinnerTestGoThoughZero()
        {
            int firstPlayerMove = 2;
            int secondlayerMove = 0;
            int numberOfMoveOptions = 3;

            Winner winner = Rules.DetermineNumberOfWinner(firstPlayerMove, secondlayerMove, numberOfMoveOptions);
            Assert.AreEqual(Winner.SecondPlayerWin, winner);
        }

        [TestMethod()]
        public void DetermineNumberOfWinnerTestGoThoughZero5()
        {
            int firstPlayerMove = 4;
            int secondlayerMove = 0;
            int numberOfMoveOptions = 5;

            Winner winner = Rules.DetermineNumberOfWinner(firstPlayerMove, secondlayerMove, numberOfMoveOptions);
            Assert.AreEqual(Winner.SecondPlayerWin, winner);
        }

        [TestMethod()]
        public void DetermineNumberOfWinnerTestDraw()
        {
            int firstPlayerMove = 2;
            int secondlayerMove = 2;
            int numberOfMoveOptions = 5;

            Winner winner = Rules.DetermineNumberOfWinner(firstPlayerMove, secondlayerMove, numberOfMoveOptions);
            Assert.AreEqual(Winner.Draw, winner);
        }
        [TestMethod()]
        public void DetermineNumberOfWinnerTestGoThoughZeroRevers()
        {
            int firstPlayerMove = 0;
            int secondlayerMove = 2;
            int numberOfMoveOptions = 3;

            Winner winner = Rules.DetermineNumberOfWinner(firstPlayerMove, secondlayerMove, numberOfMoveOptions);
            Assert.AreEqual(Winner.FirstPlayerWin, winner);
        }
    }
}