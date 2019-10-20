using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarcusJ
{
    public interface ISudokuGame : IGame, ISerialize, IGridFile
    {
        bool HasWon();

        bool IsCorrectValue(int gridIndex);

        void Reset();
        void EndTheGame();

        bool IsValidColumn(int gridIndex);
        bool IsValidRow(int gridIndex);
        bool IsValidSquare(int gridIndex);
    }
}