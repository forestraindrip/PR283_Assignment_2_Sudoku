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
        protected IGrid myGrid;
        protected int maxValue;
        protected int squaresPerColumn;
        protected int squaresPerRow;
        protected StreamReader myReader;
        protected StreamWriter myWriter;
        protected string gridCSVString;
        protected int[] solution;
        protected IIndexGetter indexGetter;
        protected IValidator validator;


        public SudokuGame(int maxValue, string gridFilePath, string solutionFilePath)
        {
            SetMaxValue(maxValue);
            SetSquareHeight((int)Math.Sqrt(maxValue));
            SetSquareWidth(maxValue / squaresPerColumn);

            indexGetter = new IndexGetter(maxValue, squaresPerColumn, squaresPerRow);
            LoadCSVFileToGrid(gridFilePath);

            validator = new Validator(maxValue, myGrid);

            string solutionString = LoadFile(solutionFilePath);
            solution = ConvertCSVToArray(solutionString);

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
            myGrid = new Grid(maxValue, squaresPerColumn, squaresPerRow);
            for (int index = 0; index < cellValues.Length; index++)
            {
                int value = cellValues[index];
                SetCell(value, index);
            }
        }

        public void SetSquareHeight(int squareHeight)
        {
            this.squaresPerColumn = squareHeight;
        }

        public void SetSquareWidth(int squareWidth)
        {
            this.squaresPerRow = squareWidth;
        }

        // Convert grid to array 
        public int[] ToArray()
        {
            int cellAmount = this.maxValue * this.maxValue;
            int[] gridArray = new int[cellAmount];

            for (int index = 0; index < maxValue * maxValue; index++)
            {
                int columnIndex = indexGetter.GetColumnIndex(index);
                int rowIndex = indexGetter.GetRowIndex(index);
                gridArray[index] = this.myGrid.GetByColumn(columnIndex, rowIndex);
            }

            return gridArray;
        }

        // Convert CSV string to Grid
        public void FromCSV(string csv)
        {
            int[] intArray = ConvertCSVToArray(csv); // Convert CSV to array
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
            int columnIndex = indexGetter.GetColumnIndex(gridIndex);
            int rowIndex = indexGetter.GetRowIndex(gridIndex);
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
            int columnIndex = indexGetter.GetColumnIndex(gridIndex);
            int rowIndex = indexGetter.GetRowIndex(gridIndex);
            return myGrid.GetByColumn(columnIndex, rowIndex);
        }

        // Grid to String
        public string ToPrettyString()
        {
            string result = "";
            for (int index = 0; index < maxValue * maxValue; index++)
            {
                int rowIndex = indexGetter.GetRowIndex(index);
                int columnIndex = indexGetter.GetColumnIndex(index);
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
            FromCSV(gridCSVString);
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
            int rowIndex = indexGetter.GetRowIndex(gridIndex);
            int columnIndex = indexGetter.GetColumnIndex(gridIndex);
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
            int columnIndex = indexGetter.GetColumnIndex(gridIndex);
            return validator.IsValidColumn(columnIndex);
        }

        public bool IsValidRow(int gridIndex)
        {
            int rowIndex = indexGetter.GetRowIndex(gridIndex);
            return validator.IsValidRow(rowIndex);
        }

        public bool IsValidSquare(int gridIndex)
        {
            int squareIndex = indexGetter.GetSquareIndex(gridIndex);
            return validator.IsValidSquare(squareIndex);
        }
    }
}
