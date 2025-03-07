using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Models;
using Sudoku.WPF.Services;
using Sudoku.WPF.Services.ContentHandlers;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class GameViewModel : IContentLoad
    {
        private readonly int TOTAL_GAME_POINTS = 81;
        private readonly int TOTAL_INCORRECT_POINTS = 3;
        private readonly int GAMETYPE_GAME = 0;
        private readonly int GAMETYPE_TRAINING = 1;
        private int _correct;
        private int _incorrect;
        private int _selectedNumber;
        private int[,] _sudokuElements;
        private Router _router;
        private GameBoard _gameBoard;
        private Dictionary<string, Action> _navigationCommands;
        public ImageSource Background { get; private set; }
        public ButtonTemplate[] Buttons { get; private set; }
        public ButtonTemplate[] PivotButtons { get; private set; }
        public ObservableCollection<ButtonTemplate[]> GameButtons { get; private set; }

        public GameViewModel(Router router, Difficulty difficulty)
        {
            _router = router;
            _gameBoard = new GameBoard(difficulty);
            _sudokuElements = _gameBoard.GetGameBoard();
            _selectedNumber = 0;
            _correct = (int)difficulty;

            _navigationCommands = new Dictionary<string, Action>();
            _navigationCommands.Add("Back to menu", BackToMenu);
            _navigationCommands.Add("Switch to training", SwitchToTraining);

            LoadContent();
        }

        private void SelectNumber(ButtonTemplate pivotButton)
        {
            int value = int.Parse(pivotButton.Content);

            if (_selectedNumber == value)
            {
                _selectedNumber = 0;
                pivotButton.ButtonColor = pivotButton.DefaultColor;
            }
            else
            {
                _selectedNumber = value;
                pivotButton.ButtonColor = new SolidColorBrush(Colors.LightBlue);

                foreach (var button in PivotButtons)
                {
                    if (button != pivotButton)
                    {
                        button.ButtonColor = button.DefaultColor;
                    }
                }
            }
        }

        private void SetNumber(ButtonTemplate button)
        {
            string tag = button.Tag.ToString();
            int i = int.Parse(tag[0].ToString());
            int j = int.Parse(tag[1].ToString());

            if (_selectedNumber == 0 || _sudokuElements[i, j] != 0)
            {
                return;
            }
            else if (_gameBoard.CheckPossibility(i, j, _selectedNumber, _sudokuElements))
            {
                _sudokuElements[i, j] = _selectedNumber;
                ++_correct;
                button.Content = _selectedNumber.ToString();
                button.ButtonColor = button.DefaultColor;

                if (_correct == TOTAL_GAME_POINTS)
                {
                    EndGame(true);
                }
            }
            else
            {
                ++_incorrect;
                button.ButtonColor = new SolidColorBrush(Colors.Red);

                if (_incorrect == TOTAL_INCORRECT_POINTS)
                {
                    EndGame(false);
                }
            }
        }

        private void BackToMenu()
        {
            ConfirmationWindow("Are you sure you want to go back to menu?", "Back to menu", new MenuView(_router));
        }

        private void SwitchToTraining()
        {
            ConfirmationWindow("Are you sure want to switch to training mode?", "Switch to training", new DifficultyView(_router, GAMETYPE_TRAINING));
        }

        private void EndGame(bool win)
        {
            _router.NavigateTo(new GameEndView(_router, win));
        }

        private void ConfirmationWindow(string question, string title, object page)
        {
            MessageBoxResult result = MessageBox.Show(question, title, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _router.NavigateTo(page);
            }
        }

        public void LoadContent()
        {
            var contentHandler = new GameContentHandler();
            Background = contentHandler.GetBackground();
            Buttons = contentHandler.CreateButtons(_navigationCommands);
            PivotButtons = contentHandler.CreateButtons(SelectNumber);
            GameButtons = contentHandler.CreateButtons(_sudokuElements, SetNumber);
        }
    }
}
