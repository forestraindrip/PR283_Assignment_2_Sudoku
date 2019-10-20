using System.Collections.Generic;

namespace MarcusJ
{
    public interface IValidator
    {
        bool HasDuplicatedValues(List<int> values);
        bool HasValueOutOfRange(List<int> values);
        bool IsValidColumn(int columnIndex);
        bool IsValidRow(int rowIndex);
        bool IsValidSquare(int squareIndex);

    }
}