using MarcusJ;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
        protected int windowWidth;
        protected int windowHeight;

        protected int gridWidth;
        protected int gridHeight;

        protected int squareHeight;
        protected int squareWidth;

        protected int squaresPerRow;
        protected int squaresPerColumn;

        protected int gridButtonHeight = 50;
        protected int gridButtonWidth = 50;

        protected Button trackedButton;
        protected bool mouseIsLeft = false;

        protected SudokuGame sudokuGame;
        protected int dragedValue;
        protected string currentLanguage;
        protected int maxSquareValue = -1;
        protected int maxSquareAmount;
        protected DispatcherTimer dispatcherTimer;
        protected Stopwatch stopwatch;
        protected DateTime startTime;
        protected List<Button> myGridButtons = new List<Button>();


        //protected Dictionary<string, Dictionary<string, string>> languageDictionary = new Dictionary<string, Dictionary<string, string>>();

        protected Dictionary<int, string> numberDictionary = new Dictionary<int, string>()
        {
            {1,"One" },
            {2, "Two" },
            {3, "Three"},
            {4, "Four"},
            {5, "Five"},
            {6, "Six"},
            {7, "Seven"},
            {8,"Eight"},
            {9,"Nine"},
            {0, "Zero"},
        };



        public MainWindow()
        {
            InitializeComponent();

            // Initialise game
            if (sudokuGame == null)
            {
                StartNewGame();

            }
            maxSquareValue = sudokuGame.GetMaxValue();
            maxSquareAmount = maxSquareValue * maxSquareValue;

            currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();
            InitialiseUIElements();

            // TEST


        }

        public void InitialiseUIElements()
        {
            // TODO: InitialiseUIElements
            //SetUILanguage(currentLanguage);

            SetupSquareGrid();

            PopulateInputButtons();

        }

        private void PopulateInputButtons()
        {
            // Create buttons
            CreateGridButtons();
            // Add the button to the cell
            for (int y = 0; y < maxSquareValue; y++)
            {
                for (int x = 0; x < maxSquareValue; x++)
                {
                    int index = y * maxSquareValue + x;
                    Button aButton = myGridButtons[index];
                    System.Windows.Controls.Grid.SetRow(aButton, y);
                    System.Windows.Controls.Grid.SetColumn(aButton, x);
                    DynamicGrid.Children.Add(aButton);
                }
            }
        }

        private void SetupSquareGrid()
        {
            SetupSquareGridSize();
            DefineGrid();

        }

        private void SetupSquareGridSize()
        {
            gridHeight = gridButtonHeight * maxSquareValue;

            gridWidth = gridButtonWidth * maxSquareValue;
        }



        protected void CreateGridButtons()
        {
            for (int i = 0; i < maxSquareAmount; i++)
            {
                Button button = CreateGridButton(i);

                myGridButtons.Add(button);
            }
        }

        private Button CreateGridButton(int gridIndex)
        {
            int cellValue = sudokuGame.GetCell(gridIndex);

            Button button = new Button();
            button.Name = "GridButton" + string.Format("{0:00}", gridIndex);
            button.Cursor = Cursors.Hand;
            button.CommandParameter = cellValue;
            if (cellValue != 0) { button.IsEnabled = false; }


            string buttonContent = numberDictionary[cellValue];
            button.Content = Properties.Resources.ResourceManager.GetString(buttonContent);

            button.PreviewMouseLeftButtonDown += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) =>
            {
                trackedButton = (Button)sender;
            });

            button.PreviewMouseLeftButtonUp += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) =>
            {
                //AddMessageToMessageBoard(mouseIsLeft.ToString());
                if (Object.ReferenceEquals(sender, trackedButton))
                {
                    AddMessageToMessageBoard(trackedButton.CommandParameter.ToString());
                    trackedButton = null;
                }
            });

            //button.PreviewMouseMove += new MouseEventHandler((object sender, MouseEventArgs e) =>
            //{
            //    //https://www.wpftutorial.net/draganddrop.html
            //    if (e.LeftButton == MouseButtonState.Pressed && trackedButton.IsMouseOver)
            //    {
            //        //AddMessageToMessageBoard(trackedButton.Name);
            //        Button senderBtn = (Button)sender;

            //    }
            //});

            button.MouseLeave += new MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                mouseIsLeft = true;
                //AddMessageToMessageBoard(mouseIsLeft.ToString());
            });

            button.MouseEnter += new MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                if (Mouse.LeftButton == MouseButtonState.Released)
                {
                    mouseIsLeft = false;
                    //AddMessageToMessageBoard(mouseIsLeft.ToString());
                }
            });

            //button.Drop += new DragEventHandler (GridButton_Drop);
            return button;
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
        protected void StartNewGame()
        {
            sudokuGame = new SudokuGame("..\\grid6x6.csv", "..\\solution6x6.csv");
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

        public void DefineGrid()
        {
            // Define row
            int rowPercentage = 100 / maxSquareValue;
            for (int i = 0; i < maxSquareValue; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(rowPercentage, GridUnitType.Star);
                DynamicGrid.RowDefinitions.Add(rowDefinition);
            }
            // Define column
            int columnPercentage = 100 / maxSquareValue;

            for (int j = 0; j < maxSquareValue; j++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(columnPercentage, GridUnitType.Star);
                DynamicGrid.ColumnDefinitions.Add(columnDefinition);
            }
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

        // Event Handler
        /***************************/


        private void GridButton_Drop(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void GridButtonMouseDown(object sender, MouseButtonEventArgs e)
        {

        }


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




        private void dipatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(startTime);
            TimerLabel.Content = string.Format("Timer(H:M:S): {0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimer();
            StartNewGame();
        }


        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: change language
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
            SetUILanguage(comboBoxItem.Tag.ToString());
            //AddMessageToMessageBoard(comboBox.SelectedItem.ToString());
        }
        /**************************************************************/

        private void SetUILanguage(string languageCode)
        {
            // https://www.tutorialspoint.com/wpf/wpf_localization.htm

            // System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-TW");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(languageCode);
            if (languageCode != currentLanguage)
            {
                ChangeCulture(new CultureInfo(languageCode));
            }

        }

        protected static void ChangeCulture(CultureInfo newCulture)
        {
            // https://jeremybytes.blogspot.com/2013/07/changing-culture-in-wpf.html
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            var oldWindow = Application.Current.MainWindow;
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            oldWindow.Close();
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




        // TODO: Prompt invalid number message
        public void CheckIsInvalidNumber(int gridIndex)
        {
            if (!sudokuGame.IsValidColumn(gridIndex))
            {
                AddMessageToMessageBoard("The column is invalid");
            }
            if (!sudokuGame.IsValidRow(gridIndex))
            {
                AddMessageToMessageBoard("The row is invalid");
            };
            if (!sudokuGame.IsValidSquare(gridIndex))
            {
                AddMessageToMessageBoard("The square is invalid");
            };
        }

        // TODO: Show row is completed

        // TODO: Show column is completed
        // TODO: Show square is completed
        // TODO: Show game complete.
    }
}
