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
            squareHeight = main.SudokuGame.MyGrid.SquareHeight;
            squareWidth = main.SudokuGame.MyGrid.SquareWidth;
            myCells = main.SudokuGame.MyGrid.MyCells;
            solution = main.SudokuGame.Solution;
            squaresPerColumn = main.SudokuGame.SquaresPerColumn;
            squaresPerRow = main.SudokuGame.SquaresPerRow;
            gridCSVString = main.SudokuGame.GridCSVString;
            moveCount = main.MoveCount;
            timeSpan = main.TimeSpan;
            endTime = DateTime.Now;
        }

    }
}