using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarcusJ;

namespace PR283_Assignment_2
{
    public class SudokuCreation4x4 : SudokuCreationStrategy
    {
        public SudokuGame CreateSudokuGame()
        {
            return new SudokuGame("..\\grid4x4.csv", "..\\solution4x4.csv");
        }
    }
}