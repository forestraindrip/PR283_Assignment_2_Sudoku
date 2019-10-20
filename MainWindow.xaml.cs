using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR283_Assignment_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected int gridWidth;
        protected int gridHeight;
        protected int windowWidth;
        protected int windowHeight;
        protected int squareHeight;
        protected int squareWidth;
        protected int squaresPerRow;
        protected int squaresPerColumn;
        protected Grid myGrid;


        public MainWindow()
        {
            InitializeComponent();
        }

        // Add buttons dynamically
        public void AddGridButtons()
        {
            // Create a button
            // Name the button
            // Find the cell
            // Add the button to the cell
        }

        // Save
        public void SaveGame() { }

        // Load
        // Do not override default value
        public void LoadGmae() { }

        // Show row is completed
        public void ShowRowIsCompleted() { }
        // Show column is completed
        public void ShowColumnIsCompleted() { }

        // Show square is completed
        public void ShowSquareIsCompleted() { }

        // Show the game is completed
        public void ShowGameIsCompleted() { }

        public void SetWindowWidth() { }

        public void SetWindowHeight() { }

        public void DefineGridSize(int squaresPerRow, int squaresPerColumn)
        {
            // Define width of column
            RowDefinition rowDefinition = new RowDefinition();
            // Define height of row
        }
    }
}
