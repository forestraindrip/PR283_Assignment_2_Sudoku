using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarcusJ;

namespace PR283_Assignment_2
{
    public class SudokuCreation6x6 : ISudokuCreationStrategy
    {
        public ISudokuGame CreateSudokuGame()
        {
            return new SudokuGame("..\\grid6x6.csv", "..\\solution6x6.csv");
        }
    }
}