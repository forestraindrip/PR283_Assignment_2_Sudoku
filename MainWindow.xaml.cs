using MarcusJ;
using Microsoft.Win32;
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
        protected List<Button> mySquareButtons = new List<Button>();

        protected Dictionary<string, Dictionary<string, string>> languageDictionary = new Dictionary<string, Dictionary<string, string>>();

        protected Dictionary<int, string> numberDictionary = new Dictionary<int, string>()
        {
            {1,"One" },
            {2, "Two" },
            {3, "Three"},
            {4, "Four"},
            {5, "Five"},
            {6, "Six"},
            {7, "Seven"},
            { 8,"Eight"},
            { 9,"Nine"},
            {0, "Zero"},
        };



        public MainWindow()
        {
            InitializeComponent();

            // Binding dictionary to combobox


            // Initialise game
            if (maxSquareValue == -1)
            {
                maxSquareValue = 6;
                sudokuGame = new SudokuGame("..\\grid6x6.csv", "..\\solution6x6.csv");
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
            // fubar
            for (int i = 0; i < maxSquareAmount; i++)
            {
                int cellValue = sudokuGame.GetCell(i);

                Button button = new Button();
                button.Name = "SquareButton" + string.Format("{0:00}", i);
                button.CommandParameter = cellValue;

                string temp = Properties.Resources.ResourceManager.GetString("One");

                button.Drop += SquareButtonDrop;
                button.DragLeave += SquareButtonDragLeave;

                mySquareButtons.Add(button);
            }

            return new Button();
        }

        private void SquareButtonDragLeave(object sender, DragEventArgs e)
        {
            Button senderBtn = (Button)sender;
            senderBtn.Tag = 0;
        }

        private void SquareButtonDrop(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }




        // Save
        protected void SaveGame() { }

        // Load
        protected void LoadGame()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var filePath = openFileDialog.FileName;
                    sudokuGame.LoadCSVFileToGrid(filePath);
                }
                catch (Exception e)
                {
                    MessageTextBox.Text += e.Message;
                    throw e;
                }
            }
        }

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
            // https://www.tutorialspoint.com/wpf/wpf_localization.htm
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-TW");
        }

        private void ChangeSelectedLanguage(object sender, SelectionChangedEventArgs e)
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
