using System;
using System.Collections.Generic;
using System.Text;

namespace MarcusJ
{
   public interface IGame
    {
        void SetMaxValue(int maximum);
        int GetMaxValue();
        int[] ToArray();      
        void Set(int[] cellValues);
        void SetSquareWidth(int squareWidth);
        void SetSquareHeight(int squareHeight);
        void Restart();
    }
}