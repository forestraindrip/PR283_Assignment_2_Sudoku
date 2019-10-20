using System;
using System.Collections.Generic;
using System.Text;

namespace MarcusJ
{
    public interface IGet
    {
        int GetByColumn(int columnIndex, int rowIndex);
        int GetByRow(int rowIndex, int columnIndex);
        int GetBySquare(int squareIndex, int positionIndex);
    }
}
