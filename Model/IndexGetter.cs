using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcusJ
{
    public class IndexGetter
    {
        protected int maxValue;
        protected int squareHeight;
        protected int squareWidth;
        public IndexGetter(int maxValue, int squareHeight, int squareWidth)
        {
            this.maxValue = maxValue;
            this.squareHeight = squareHeight;
            this.squareWidth = squareWidth;
        }

        // 14.	The game can get the square number from a grid index
        public int GetSquareIndex(int cellIndex)
        {
            int squaresPerRow = maxValue / squareWidth;
            int columnIndex = (cellIndex % maxValue) / squareWidth;
            int rowIndex = (cellIndex / maxValue) / squareHeight;
            return rowIndex * squaresPerRow + columnIndex;
        }

        // 15.	The game can get the row number from a grid index 
        public int GetRowIndex(int cellIndex)
        {
            return cellIndex / maxValue;
        }

        // 16.	The game can get the column number from a grid index
        public int GetColumnIndex(int cellIndex)
        {
            return cellIndex % maxValue;
        }

        public int GetIndexByRowColumn(int rowIndex, int columnIndex)
        {
            return rowIndex * maxValue + columnIndex;
        }

        public int GetIndexBySquarePosition(int squareIndex, int position)
        {
            int squarsPerRow = maxValue / squareWidth;
            int columnIndex = (squareIndex % squarsPerRow) * squareWidth + (position % squareWidth);
            int rowIndex = (squareIndex / squarsPerRow) * squareHeight + (position / squareWidth);

            return rowIndex * maxValue + columnIndex;
        }
    }
}
