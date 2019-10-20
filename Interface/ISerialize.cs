using System;
using System.Collections.Generic;
using System.Text;

namespace MarcusJ
{
    public interface ISerialize
    {
        void FromCSV(string csv);
        string ToCSV();            // To actual CSV format string
        void SetCell(int value, int gridIndex);
        int GetCell(int gridIndex);
        string ToPrettyString();   // To user friendly string
    }
}