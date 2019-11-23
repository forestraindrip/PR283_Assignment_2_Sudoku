using System;

namespace PR283_Assignment_2
{
    internal class Save
    {
        public int maxValue;
        public int squareHeight;
        public int squareWidth;
        public int[] myCells;
        public int[] solution;
        public int squaresPerColumn;
        public int squaresPerRow;
        public string gridCSVString;
        public int moveCount;
        public TimeSpan timeSpan;
        public DateTime endTime;

        public void SaveState(MainWindow main)
        {
            maxValue = main.SudokuGame.GetMaxValue();

            solution = main.SudokuGame.GetSolution();
            squaresPerColumn = main.SudokuGame.GetSquaresPerColumn();
            squaresPerRow = main.SudokuGame.GetSquaresPerRow();
            gridCSVString = main.SudokuGame.GetGridCSVString();
            squareHeight = main.SudokuGame.GetSquareHeight();
            squareWidth = main.SudokuGame.GetSquareWidth();
            myCells = main.SudokuGame.GetCells();
            moveCount = main.MoveCount;
            timeSpan = main.TimeSpan;
            endTime = DateTime.Now;
        }

    }
}