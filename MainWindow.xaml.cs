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
        protected int dragedValue;
        protected string language = "en-US"; // zh-TW
        protected List<string> languageArray = new List<string>() { "en-US", "zh-TW" };
        protected Dictionary<string, string> enUSDictionary = new Dictionary<string, string>()
        {
            { "Load", "Load"},
            { "Save", "Save"},
            { "Restart", "Restart"},
            { "English", "English"},
            { "Traditional Chinese", "Traditional Chinese"},
            { "Timer", "Timer"},
            { "Score", "Score"},
            { "One", "1"},
            { "Two", "2"},
            { "Three", "3"},
            { "Four", "4"},
            { "Five", "5"},
            { "Six", "6"},
            { "Seven", "7"},
            { "Eight", "8"},
            { "Nine", "9"},
            { "Zero", "0"},
        };
        protected Dictionary<string, string> zhTWDictionary = new Dictionary<string, string>() { };

        public MainWindow()
        {
            InitializeComponent();
            InitialiseUIElements();
        }

        public void InitialiseUIElements()
        {
            // TODO: InitialiseUIElements
            SetUILanguage();

            SetupSquareGrid();
            PopulateInputButtons();
            PopulateSqaures();

        }

        private void PopulateSqaures()
        {
           // throw new NotImplementedException();
        }

        private void PopulateInputButtons()
        {
            // throw new NotImplementedException();
        }

        private void SetupSquareGrid()
        {
            SetupSquareGridSize();
        }

        private void SetupSquareGridSize()
        {
           // throw new NotImplementedException();
        }

        // TODO: Add buttons dynamically
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

        public Button CreateGridButton()
        {
            // TODO: CreateGridButton
            // TODO: Factory method
            return new Button();
        }

        public void PressButton(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = sender as Button;
                dragedValue = Int32.Parse(button.Content.ToString());
            }
        }
        public void DragButtonOut() { }
        public void DragButtonIn() { }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Load game
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Save game

        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Restart game
        }


        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox languageComboBox = (ComboBox)sender;
            // Traditional Chinese
            languageComboBox;
            SetUILanguage();
        }

        private void SetUILanguage()
        {
            // throw new NotImplementedException();
        }








        // TODO: Timer
        // TODO: Prompt invalid number message
        // TODO: Show row is completed
        // TODO: Show column is completed
        // TODO: Show square is completed
        // TODO: Show game complete.
    }
}
