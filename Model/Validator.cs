using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcusJ
{
    public class Validator 
    {
        protected IGrid myGrid;
        protected int maxValue;

        public Validator(int maxValue, IGrid grid)
        {
            this.maxValue = maxValue;
            this.myGrid = grid;
        }

        // 5.	The game can check whether a row is valid
        public bool IsValidRow(int rowIndex)
        {
            List<int> valuesInRow = myGrid.GetValuesInRow(rowIndex);
            bool isOutOfRange = HasValueOutOfRange(valuesInRow);
            bool hasDuplicatedRowValue = HasDuplicatedValues(valuesInRow);
            bool isRowCompleted = myGrid.IsRowCompleted(rowIndex);
            return !isOutOfRange && !hasDuplicatedRowValue && isRowCompleted;
        }

        // 6.	The game can check whether a column is valid
        public bool IsValidColumn(int columnIndex)
        {
            List<int> valuesInColumn = myGrid.GetValuesInColumn(columnIndex);
            bool isOutOfRange = HasValueOutOfRange(valuesInColumn);
            bool hasDuplicatedColumnValue = HasDuplicatedValues(valuesInColumn);
            bool isColumnCompleted = myGrid.IsColumnCompleted(columnIndex);
            return !isOutOfRange && !hasDuplicatedColumnValue && isColumnCompleted;
        }
        // 7.	The game can check whether a square is valid
        public bool IsValidSquare(int squareIndex)
        {
            List<int> valuesInSquare = myGrid.GetValuesInSqaure(squareIndex);
            bool isOutOfRange = HasValueOutOfRange(valuesInSquare);
            bool hasDuplicatedSquareValue = HasDuplicatedValues(valuesInSquare);
            bool isSquareCompleted = myGrid.IsSquareCompleted(squareIndex);
            return !isOutOfRange && !hasDuplicatedSquareValue && isSquareCompleted;
        }

        // 8.	The game can check whether a row/column/square has duplicated value
        public bool HasDuplicatedValues(List<int> values)
        {
            // https://stackoverflow.com/questions/18547354/c-sharp-linq-find-duplicates-in-list

            List<int> refinedValues = values.Where(s => s != 0).ToList();
            List<int> listDuplicatedValues = refinedValues.GroupBy(x => x)
                                                  .Where(g => g.Count() > 1)
                                                  .Select(y => y.Key)
                                                  .ToList();

            return listDuplicatedValues.Count > 0;
        }

        // 9.	The game can check whether a row/column/square has value out of range
        public bool HasValueOutOfRange(List<int> values)
        {

            return values.Min() < 0 || maxValue < values.Max();
        }


    }
}
