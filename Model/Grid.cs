using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MarcusJ
{
    public class Grid : IGrid
    {
        protected int maxValue;
        protected int squareHeight;
        protected int squareWidth;
        protected int[] myCells;
        protected IIndexGetter indexGetter;

        public Grid(int maxValue, int squareHeight, int squareWidth)
        {
            this.maxValue = maxValue;
            this.squareHeight = squareHeight;
            this.squareWidth = squareWidth;
            myCells = new int[maxValue * maxValue];

            indexGetter = new IndexGetter(maxValue, squareHeight, squareWidth);
        }
        public int GetByColumn(int columnIndex, int rowIndex)
        {
            int index = rowIndex * maxValue + columnIndex;
            return myCells[index];
        }

        public int GetByRow(int rowIndex, int columnIndex)
        {
            int index = rowIndex * maxValue + columnIndex;
            return myCells[index];
        }

        public int GetBySquare(int squareNumber, int positionIndex)
        {
            int gridIndex = indexGetter.GetIndexBySquarePosition(squareNumber, positionIndex);
            return myCells[gridIndex];
        }
        public void SetByColumn(int value, int columnIndex, int rowIndex)
        {
            int gridIndex = indexGetter.GetIndexByRowColumn(rowIndex, columnIndex);

            if (!IsOutOfRange(value))
            {
                myCells[gridIndex] = value;
            }
            else throw new IsNotValidValueException();
        }

        private bool IsOutOfRange(int value)
        {
            return value < 0 || maxValue < value;
        }

        public void SetByRow(int value, int rowIndex, int columnIndex)
        {
            int gridIndex = indexGetter.GetIndexByRowColumn(rowIndex, columnIndex);

            if (!IsOutOfRange(value))
            {
                myCells[gridIndex] = value;
            }
            else throw new IsNotValidValueException();
        }

        public void SetBySquare(int value, int squareIndex, int positionIndex)
        {
            int gridIndex = indexGetter.GetIndexBySquarePosition(squareIndex, positionIndex);

            if (!IsOutOfRange(value))
            {
                myCells[gridIndex] = value;
            }
            else throw new IsNotValidValueException();
        }


        // 10.	The game can get the possible values of a cell
        public List<int> GetValidValues(int cellIndex)
        {
            int rowIndex = indexGetter.GetRowIndex(cellIndex);
            int columnIndex = indexGetter.GetColumnIndex(cellIndex);
            int squareIndex = indexGetter.GetSquareIndex(cellIndex);

            List<int> valuesInRow = GetValuesInRow(rowIndex);
            List<int> valuesInColumn = GetValuesInColumn(columnIndex);
            List<int> valuesInSqaure = GetValuesInSqaure(squareIndex);

            List<int> valuesUnion = valuesInRow.Union(valuesInColumn.Union(valuesInSqaure)).ToList();

            List<int> validValues = Enumerable.Range(1, maxValue).ToList();
            List<int> result = validValues.Except(valuesUnion).ToList();

            return result;
        }

        // 11.	The game can get all values in a row
        public List<int> GetValuesInRow(int rowIndex)
        {
            List<int> result = new List<int>();
            int startIndex = rowIndex * maxValue;
            int endIndex = startIndex + maxValue;
            for (int i = startIndex; i < endIndex; i++)
            {
                int cellValue = myCells[i];
                result.Add(myCells[i]);
            }
            return result;
        }

        // 12.	The game can get all values in a column
        public List<int> GetValuesInColumn(int columnIndex)
        {
            List<int> result = new List<int>();
            int startIndex = columnIndex % maxValue;
            int endIndex = maxValue * maxValue;

            for (int i = startIndex; i < endIndex; i += maxValue)
            {
                int cellValue = myCells[i];

                result.Add(myCells[i]);

            }
            return result;
        }

        // 13.	The game can get all values in a square
        public List<int> GetValuesInSqaure(int squareIndex)
        {
            int squaresPerRow = maxValue / squareWidth;
            int columnIndex = (squareIndex % squaresPerRow) * squareWidth;
            int rowIndex = (squareIndex / squaresPerRow) * squareHeight;
            List<int> valuesInSquare = new List<int>();
            for (int y = rowIndex; y < rowIndex + squareHeight; y++)
            {
                for (int x = columnIndex; x < columnIndex + squareWidth; x++)
                {
                    int index = y * maxValue + x;
                    int cellValue = myCells[index];

                    valuesInSquare.Add(cellValue);
                }
            }
            return valuesInSquare;
        }

        // 18.	The game can check whether a row is completed
        public bool IsRowCompleted(int rowIndex)
        {
            return !GetValuesInRow(rowIndex).Contains(0);
        }

        // 19.	The game can check whether a column is completed
        public bool IsColumnCompleted(int columnIndex)
        {
            return !GetValuesInColumn(columnIndex).Contains(0);
        }
        // 20.	The game can check whether a square is completed
        public bool IsSquareCompleted(int squareIndex)
        {
            return !GetValuesInSqaure(squareIndex).Contains(0);
        }

    }
}