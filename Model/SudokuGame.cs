using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarcusJ
{
    public class SudokuGame : ISudokuGame
    {
        protected Grid myGrid;
        protected int maxValue;
        private int squaresPerColumn;
        private int squaresPerRow;
        protected StreamReader myReader;
        protected StreamWriter myWriter;
        private string gridCSVString;
        protected int[] solution;
        private IndexGetter indexGetter;
        private Validator validator;

        public int SquaresPerColumn { get => squaresPerColumn; }
        public int SquaresPerRow { get => squaresPerRow; }
        public IndexGetter IndexGetter { get => indexGetter; }
        public Validator Validator { get => validator; }
        public string GridCSVString { get => gridCSVString; set => gridCSVString = value; }
        public Grid MyGrid { get => myGrid; set => myGrid = value; }

        public void SetSolution(int[] solution)
        {
            this.solution = solution;
        }




        public SudokuGame(string gridFilePath, string solutionFilePath)
        {
            LoadCSVFileToGrid(gridFilePath);

            this.validator = new Validator(maxValue, myGrid);

            string solutionString = LoadFile(solutionFilePath);
            solution = ConvertCSVToArray(solutionString);

        }
        public SudokuGame(Validator validator, IndexGetter indexGetter)
        {
            this.validator = validator;
            this.indexGetter = indexGetter;
        }

        public int GetMaxValue()
        {
            return this.maxValue;
        }

        public void SetMaxValue(int maximum)
        {
            this.maxValue = maximum;
        }

        //  Reset all cells to 0
        public void Restart()
        {
            Set(new int[maxValue * maxValue]);
        }

        // Set array values to Grid
        public void Set(int[] cellValues)
        {
            if (myGrid == null)
            {
                myGrid = new Grid(maxValue, SquaresPerColumn, SquaresPerRow);
            }
            for (int index = 0; index < cellValues.Length; index++)
            {
                int value = cellValues[index];
                SetCell(value, index);
            }
        }

        public void SetSquaresPerColumn(int squaresPerColumn)
        {
            this.squaresPerColumn = squaresPerColumn;
        }

        public void SetSquarePerRow(int squarePerRow)
        {
            this.squaresPerRow = squarePerRow;
        }

        // Convert grid to array 
        public int[] ToArray()
        {
            int cellAmount = this.maxValue * this.maxValue;
            int[] gridArray = new int[cellAmount];

            for (int index = 0; index < maxValue * maxValue; index++)
            {
                int columnIndex = IndexGetter.GetColumnIndex(index);
                int rowIndex = IndexGetter.GetRowIndex(index);
                gridArray[index] = this.myGrid.GetByColumn(columnIndex, rowIndex);
            }

            return gridArray;
        }

        // Convert CSV string to Grid
        public void FromCSV(string csv)
        {
            int[] intArray = ConvertCSVToArray(csv); // Convert CSV to array

            maxValue = (int)Math.Sqrt(intArray.Length);
            SetMaxValue(maxValue);
            SetSquaresPerColumn((int)Math.Sqrt(maxValue));
            SetSquarePerRow(maxValue / SquaresPerColumn);
            Set(intArray); // Set values to Grid
        }

        private int[] ConvertCSVToArray(string csv)
        {
            List<String> listString = csv.Split(',').ToList();
            List<int> listInt = new List<int>();
            foreach (string aString in listString)
            {
                listInt.Add(int.Parse(aString));
            }

            return listInt.ToArray();
        }

        // 17.	The game can clean dirty string input
        public string CleanInputString(string csv)
        {
            string refinedCSV = Regex.Replace(csv, @"^\D*,\D*|\D*,\D*$", "");
            refinedCSV = Regex.Replace(refinedCSV, @"\D*,\D*", ",");
            return refinedCSV;
        }

        public void SetCell(int value, int gridIndex)
        {
            if (indexGetter == null)
            {
                indexGetter = new IndexGetter(maxValue, SquaresPerColumn, SquaresPerRow);
            }
            int columnIndex = IndexGetter.GetColumnIndex(gridIndex);
            int rowIndex = IndexGetter.GetRowIndex(gridIndex);
            try
            {
                myGrid.SetByColumn(value, columnIndex, rowIndex);
            }
            catch (IsNotValidValueException e)
            {
                throw e;
            }
        }

        public int GetCell(int gridIndex)
        {
            int columnIndex = IndexGetter.GetColumnIndex(gridIndex);
            int rowIndex = IndexGetter.GetRowIndex(gridIndex);
            return myGrid.GetByColumn(columnIndex, rowIndex);
        }

        // Grid to String
        public string ToPrettyString()
        {
            string result = "";
            for (int index = 0; index < maxValue * maxValue; index++)
            {
                int rowIndex = IndexGetter.GetRowIndex(index);
                int columnIndex = IndexGetter.GetColumnIndex(index);
                int value = myGrid.GetByRow(rowIndex, columnIndex);
                result += (columnIndex == maxValue - 1) ? value + "\r\n" : value + ",";
            }
            return result;
        }

        // 1. The game can load the grid from a CSV format file
        public void LoadCSVFileToGrid(string filePath)
        {
            string fileContent = LoadFile(filePath);
            gridCSVString = CleanInputString(fileContent);
            FromCSV(gridCSVString);
        }

        // 2. The game can save the grid to a string
        public string ToCSV()
        {
            List<int> list = new List<int>();
            for (int gridIndex = 0; gridIndex < maxValue * maxValue; gridIndex++)
            {
                list.Add(GetCell(gridIndex));
            }
            return String.Join(",", list.Select(s => s));
        }


        private string LoadFile(string filePath)
        {
            try
            {
                myReader = new StreamReader(filePath);
            }
            catch (Exception e) { throw e; }
            return myReader.ReadToEnd();
        }

        //  3. The game can save the grid to a CSV format file
        public void SaveGridToCSVFile(string filePath)
        {
            try
            {
                myWriter = new StreamWriter(filePath);
            }
            catch (Exception e) { throw e; }
            myWriter.WriteLine(ToCSV());
            myWriter.Close();
        }

        // 4.	The game can reset the game to the initial state
        public void Reset()
        {
            FromCSV(GridCSVString);
        }

        // 21.	The game can check whether the player has won the game
        public bool HasWon()
        {
            bool result = true;
            for (int i = 0; i < maxValue * maxValue; i++)
            {
                if (!IsCorrectValue(i)) { result = false; }
            }
            return result;
        }

        // 22.	The game can check whether the value in the cell is correct
        public bool IsCorrectValue(int gridIndex)
        {
            int rowIndex = IndexGetter.GetRowIndex(gridIndex);
            int columnIndex = IndexGetter.GetColumnIndex(gridIndex);
            int value = myGrid.GetByRow(rowIndex, columnIndex);

            int solutionValue = solution[gridIndex];

            return solutionValue == value;
        }

        [ExcludeFromCodeCoverage]
        public void EndTheGame()
        {
            myReader.Close();
            myWriter.Close();
        }

        public bool IsValidColumn(int gridIndex)
        {
            int columnIndex = IndexGetter.GetColumnIndex(gridIndex);
            return Validator.IsValidColumn(columnIndex);
        }

        public bool IsValidRow(int gridIndex)
        {
            int rowIndex = IndexGetter.GetRowIndex(gridIndex);
            return Validator.IsValidRow(rowIndex);
        }

        public bool IsValidSquare(int gridIndex)
        {
            int squareIndex = IndexGetter.GetSquareIndex(gridIndex);
            return Validator.IsValidSquare(squareIndex);
        }
        public List<int> GetValidValues(int cellIndex)
        {
            return myGrid.GetValidValues(cellIndex);
        }

        public int[] GetSolution()
        {
            return solution;
        }

        public int GetSquaresPerColumn()
        {
            return SquaresPerColumn;
        }

        public int GetSquaresPerRow()
        {
            return SquaresPerRow;
        }

        public string GetGridCSVString()
        {
            return GridCSVString;
        }

        public int GetSquareHeight()
        {
           return this.MyGrid.SquareHeight;
        }

        public int GetSquareWidth()
        {
            return this.MyGrid.SquareWidth;
        }

        public int[] GetCells()
        {
            return this.MyGrid.MyCells;
        }
    }
}
