using MarcusJ;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        //protected int squareHeight;
        //protected int squareWidth;

        //protected int squaresPerRow;
        //protected int squaresPerColumn;

        protected int gridButtonHeight = 70;
        protected int gridButtonWidth = 70;

        // https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.brushes?view=netframework-4.8
        protected Brush defaultButtonColor = Brushes.LightYellow;
        protected Brush variedButtonColor = Brushes.LightSkyBlue;


        protected Button trackedButton;
        protected Point mouseStartPoint;

        protected SudokuGame sudokuGame;
        protected int dragedValue;
        protected string currentLanguage;
        protected int maxSquareValue = -1;
        protected int maxSquareAmount;
        protected DispatcherTimer dispatcherTimer;
        protected Stopwatch stopwatch;
        protected DateTime startTime;
        protected List<Button> myGridButtons = new List<Button>();


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
            if (currentLanguage == null) { currentLanguage = "en-US"; }
            InitializeComponent();
            currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();

            // TEST
        }

        public void InitialiseUIElements()
        {
            //SetUILanguage(currentLanguage);

            DynamicGrid.Children.Clear();
            DynamicGrid.RowDefinitions.Clear();
            DynamicGrid.ColumnDefinitions.Clear();

            SetupSquareGrid();

            PopulateInputButtons();
            UpdateGameStatus();
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
            AdjustWindowSize();
            DefineDynamicGrid();

        }

        private void AdjustWindowSize()
        {
            this.Height = 250 + DynamicGrid.Height;
            this.Width = 300 + DynamicGrid.Width < 660 ? 660 : 300 + DynamicGrid.Width;
        }

        private void SetupSquareGridSize()
        {
            gridHeight = gridButtonHeight * maxSquareValue;
            gridWidth = gridButtonWidth * maxSquareValue;

            DynamicGrid.Height = gridHeight;
            DynamicGrid.Width = gridWidth;
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
            button.FontSize = 20;
            button.Cursor = Cursors.Hand;
            button.Background = defaultButtonColor;
            button.AllowDrop = true;
            UpdateButtonContent(cellValue, button);

            if (cellValue == 0)
            {
                button.PreviewMouseLeftButtonDown += Button_MouseButtonDown;

                button.PreviewMouseLeftButtonUp += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) =>
                {
                    Button senderButton = sender as Button;
                    UpdateButtonContent(0, senderButton);

                    trackedButton = null;
                });

                button.Drop += GridButton_Drop;
            }


            //button.Drop += new DragEventHandler (GridButton_Drop);
            return button;
        }

        private void UpdateGameStatus()
        {
            // Update button colour
            for (int index = 0; index < maxSquareAmount; index++)
            {

                Button button = myGridButtons[index];
                if (sudokuGame.IsValidColumn(index) || sudokuGame.IsValidRow(index) || sudokuGame.IsValidSquare(index))
                {
                    button.Background = variedButtonColor;
                }
                else
                {
                    button.Background = defaultButtonColor;
                }
            }


            if (sudokuGame.HasWon())
            {
                dispatcherTimer.Stop();
                ShowGameCompleted();
            }
        }

        private void UpdateButtonContent(int cellValue, Button button)
        {
            button.CommandParameter = cellValue;
            string buttonContent = numberDictionary[cellValue];
            button.Content = Properties.Resources.ResourceManager.GetString(buttonContent);
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
                    if (sudokuGame == null)
                    {
                        sudokuGame = new SudokuGame("..\\grid4x4.csv", "..\\solution4x4.csv");
                    }
                    sudokuGame.LoadCSVFileToGrid(filePath);
                }
                catch (Exception e)
                {
                    AddMessageToMessageBoard(e.Message);
                    AddMessageToMessageBoard(e.Message);
                    throw e;
                }
                maxSquareValue = sudokuGame.GetMaxValue();
                maxSquareAmount = maxSquareValue * maxSquareValue;
                InitialiseUIElements();
            }
        }
        protected void StartNewGame()
        {
            sudokuGame = new SudokuGame("..\\grid4x4.csv", "..\\solution4x4.csv");
            maxSquareValue = sudokuGame.GetMaxValue();
            maxSquareAmount = maxSquareValue * maxSquareValue;
            InitialiseUIElements();

        }

        protected void RestartGame() { }

        public void SetWindowWidth() { }

        public void SetWindowHeight() { }

        public void DefineDynamicGrid()
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

        protected void AddMessageToMessageBoard<T>(T message)
        {
            TextScrollViewer.Content += ("\r\n" + message.ToString());
            TextScrollViewer.ScrollToEnd();
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


        protected void ShowGameCompleted()
        {
            AddMessageToMessageBoard("YOU WIN!");
        }

        // Event Handler
        /***************************/
        private void InputButtonStackPanel_Drop(object sender, DragEventArgs e)
        {
            Button sourceButton = e.Data.GetData("sender") as Button;


            if (Regex.IsMatch(sourceButton.Name, @"^GridButton"))
            {
                string indexString = Regex.Replace(sourceButton.Name, @"^GridButton", "");
                int index = Int32.Parse(indexString);
                try
                {
                    sudokuGame.SetCell(0, index);
                }
                catch (IsNotValidValueException exception)
                {
                    AddMessageToMessageBoard("Is Not A Valid Value");
                }
                UpdateButtonContent(0, sourceButton);
                UpdateGameStatus();

            }
        }

        private void GridButton_Drop(object sender, DragEventArgs e)
        {
            Button sourceButton = e.Data.GetData("sender") as Button;
            if (Regex.IsMatch(sourceButton.Name, @"^InputButton\d"))
            {
                Button button = sender as Button;
                string indexString = Regex.Replace(button.Name, @"^GridButton", "");
                int index = Int32.Parse(indexString);

                int value = Convert.ToInt32(e.Data.GetData("value").ToString());

                if (0 <= value && value <= maxSquareValue)
                {
                    try
                    {
                        sudokuGame.SetCell(value, index);
                    }
                    catch (IsNotValidValueException exception)
                    {
                        AddMessageToMessageBoard("Is Not A Valid Value");
                    }
                    UpdateButtonContent(value, button);
                }
                else { AddMessageToMessageBoard("Is Not A Valid Value"); }

                UpdateGameStatus();

                // TEST
            }
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

        private void Button_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            // https://codedocu.com/Details_Mobile?d=2434&a=9&f=331&l=0&v=m&t=WPF:-Drag-Drop-Example
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Button button = sender as Button;
                int value = Convert.ToInt32(button.CommandParameter.ToString());
                DataObject dataObject = new DataObject();
                dataObject.SetData("value", value);
                dataObject.SetData("sender", sender);

                DragDrop.DoDragDrop(button, dataObject, DragDropEffects.Copy);
            }
        }

        private void dipatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(startTime);
            TimerFigureLabel.Content = string.Format("(H:M:S): {0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
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

        private void GetHintTextBlock_Drop(object sender, DragEventArgs e)
        {
            Button sourceButton = e.Data.GetData("sender") as Button;

            if (Regex.IsMatch(sourceButton.Name, @"^GridButton"))
            {
                string indexString = Regex.Replace(sourceButton.Name, @"^GridButton", "");
                int index = Int32.Parse(indexString);

                List<int> validValues = sudokuGame.GetValidValues(index);
                string message = "Potential values: ";
                validValues.ForEach(v => message += " "+ v);
                AddMessageToMessageBoard(message);
            }
        }
        /**************************************************************/
    }
}
