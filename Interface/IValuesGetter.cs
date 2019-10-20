using System.Collections.Generic;

namespace MarcusJ
{
    public interface IValuesGetter
    {
        List<int> GetValidValues(int cellIndex);
        List<int> GetValuesInColumn(int columnIndex);
        List<int> GetValuesInRow(int rowIndex);
        List<int> GetValuesInSqaure(int squareIndex);
    }
}