using MarcusJ;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PR283_Assignment_2
{
    /// <summary>
    /// Event Handler logic for MainWindow.xaml
    /// Interaction logics are in MainWindow.xaml.cs
    /// </summary>
    public partial class MainWindow : Window
    {

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
                    string msg = Properties.Resources.NotAValidValue;
                    AddMessageToMessageBoard(msg);
                }
                MoveCount++;
                UpdateButtonContent(0, sourceButton);
                UpdateGameStatus();

            }
        }

        private void GridButton_Drop(object sender, DragEventArgs e)
        {
            // https://codedocu.com/Details_Mobile?d=2434&a=9&f=331&l=0&v=m&t=WPF:-Drag-Drop-Example
            Button sourceButton = e.Data.GetData("sender") as Button;
            if (Regex.IsMatch(sourceButton.Name, @"^InputButton\d"))
            {
                Button button = sender as Button;
                string indexString = Regex.Replace(button.Name, @"^GridButton", "");
                int index = Int32.Parse(indexString);

                int value = Convert.ToInt32(e.Data.GetData("value").ToString());

                if ((0 <= value) && (value <= maxSquareValue))
                {
                    try
                    {
                        sudokuGame.SetCell(value, index);
                    }
                    catch (IsNotValidValueException exception)
                    {
                        AddMessageToMessageBoard(Properties.Resources.NotAValidValue);
                    }
                    MoveCount++;
                    UpdateButtonContent(value, button);
                }
                else { AddMessageToMessageBoard(Properties.Resources.NotAValidValue);  }

                UpdateGameStatus();

            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadGame();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveGame();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button senderButton = sender as Button;
            UpdateButtonContent(0, senderButton);

            trackedButton = null;
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

            timeSpan = timeSpan.Add(TimeSpan.FromSeconds(1));
            TimerFigureLabel.Content = string.Format("(H:M:S): {0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem comboBoxItem = GridSizeComboxBox.SelectedItem as ComboBoxItem;
            string content = comboBoxItem.Content.ToString();
            int maxValue = Int32.Parse(content[0].ToString());

            StartNewGame(maxValue);
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
            SetUILanguage(comboBoxItem.Tag.ToString());
        }

        private void GetHintTextBlock_Drop(object sender, DragEventArgs e)
        {
            Button sourceButton = e.Data.GetData("sender") as Button;

            if (Regex.IsMatch(sourceButton.Name, @"^GridButton"))
            {
                string indexString = Regex.Replace(sourceButton.Name, @"^GridButton", "");
                int index = Int32.Parse(indexString);

                List<int> validValues = sudokuGame.GetValidValues(index);
                string message = Properties.Resources.ThereIsNoPossibleValue;
                if (validValues.Count > 0)
                {
                    message = Properties.Resources.PotentialValues;
                    validValues.ForEach(v => message += " " + v);
                }
                AddMessageToMessageBoard(message);
            }
        }
    }
}
