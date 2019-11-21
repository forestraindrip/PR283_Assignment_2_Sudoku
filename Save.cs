using System;

namespace PR283_Assignment_2
{
    internal class Save
    {
        public int MaxValue;
        public int SquareHeight;
        public int SquareWidth;
        public int[] MyCells;
        public int[] Solution;
        public int SquaresPerColumn;
        public int SquaresPerRow;
        public string GridCSVString;
        public int MoveCount;
        public TimeSpan TimeSpan;
        public DateTime EndTime;

        public void SaveState(MainWindow main)
        {
            MaxValue = main.SudokuGame.GetMaxValue();
            SquareHeight = main.SudokuGame.MyGrid.SquareHeight;
            SquareWidth = main.SudokuGame.MyGrid.SquareWidth;
            MyCells = main.SudokuGame.MyGrid.MyCells;
            Solution = main.SudokuGame.Solution;
            SquaresPerColumn = main.SudokuGame.SquaresPerColumn;
            SquaresPerRow = main.SudokuGame.SquaresPerRow;
            GridCSVString = main.SudokuGame.GridCSVString;
            MoveCount = main.MoveCount;
            TimeSpan = main.TimeSpan;
            EndTime = DateTime.Now;
        }

    }
}