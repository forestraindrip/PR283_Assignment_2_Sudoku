using MarcusJ;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

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
        protected SudokuGame sudokuGame;
        protected int dragedValue;
        protected string currentLanguage = "English";
        protected int maxSquareValue = -1;
        protected int maxSquareAmount;
        protected DispatcherTimer dispatcherTimer;
        protected Stopwatch stopwatch;
        protected DateTime startTime;

        protected Dictionary<string, Dictionary<string, string>> languageDictionary = new Dictionary<string, Dictionary<string, string>>();

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

            // Binding dictionary to combobox
            languageDictionary.Add("English", enUSDictionary);
            languageDictionary.Add("Traditional Chinese", zhTWDictionary);
            LanguageComboBox.ItemsSource = languageDictionary.Keys;

            // Initialise game
            if (maxSquareValue == -1)
            {
                maxSquareValue = 6;
                sudokuGame = new SudokuGame(maxSquareValue, "..\\grid6x6.csv", "..\\solution6x6.csv");
            }
            maxSquareAmount = maxSquareValue * maxSquareValue;



            InitialiseUIElements();

            // TEST
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

            AddGridButtons();
        }

        private void SetupSquareGridSize()
        {
            // throw new NotImplementedException();
        }

        // TODO: Add buttons dynamically
        public void AddGridButtons()
        {
            // Create a button
            CreateGridButton();
            // Find the cell
            // Add the button to the cell
        }

        protected Button CreateGridButton()
        {
            // TODO: CreateGridButton Factory method

            for (int i = 0; i < maxSquareAmount; i++)
            {
                int cellValue = sudokuGame.GetCell(i);

                Button button = new Button();
                button.Name = "SquareButton" + string.Format("{0:00}", i);
                button.Drop += SquareButtonDrop;
                button.DragLeave += SquareButtonDragLeave;
            }

            return new Button();
        }

        private void SquareButtonDragLeave(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SquareButtonDrop(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }




        // Save
        protected void SaveGame() { }

        // Load
        // Do not override default value
        protected void LoadGame() { }

        protected void RestartGame() { }

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
            LoadGame();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Save game
            SaveGame();

        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Restart game
            RestartGame();
        }


        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox languageComboBox = (ComboBox)sender;
            // Traditional Chinese
            SetUILanguage();
        }

        private void SetUILanguage()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
        }

        private void SetUILanguage(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            AddMessageToMessageBoard(comboBox.SelectedItem.ToString());
        }

        protected void AddMessageToMessageBoard(string message)
        {
            MessageTextBox.Text += "\r\n" + message;
        }



        // Timer
        public void SetTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += dipatcherTimer_Tick;

            startTime = DateTime.Now;
            dispatcherTimer.Start();

        }

        private void dipatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(startTime);
            TimerLabel.Content = string.Format("Timer(H:M:S): {0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimer();

        }

        private void Button_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void SquareButton_Drop(object sender, DragEventArgs e)
        {

        }

        private void SquareButtonMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        // TODO: Prompt invalid number message
        // TODO: Show row is completed
        // TODO: Show column is completed
        // TODO: Show square is completed
        // TODO: Show game complete.
    }
}
