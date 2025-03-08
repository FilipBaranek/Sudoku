using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Models;
using Sudoku.WPF.Models.Templates;
using Sudoku.WPF.Services;
using Sudoku.WPF.Services.ContentHandlers;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged, IContentLoad
    {
        private readonly int TOTAL_GAME_POINTS = 81;
        private readonly int TOTAL_INCORRECT_POINTS = 3;
        private readonly int GAMETYPE_GAME = 0;
        private readonly int GAMETYPE_TRAINING = 1;
        private int _correct;
        private int _incorrect;
        private int _totalCorrect;
        private int _selectedNumber;
        private int[,] _sudokuElements;
        private Router _router;
        private GameBoard _gameBoard;
        private Dictionary<string, Action> _pauseCommands;
        public ICommand SelectNumberByKeyCommand { get; private set; }
        public PauseTemplate Pause { get; private set; }
        public ImageSource Background { get; private set; }
        public ButtonTemplate[] PauseButtons { get; private set; }
        public ButtonTemplate[] PivotButtons { get; private set; }
        public ObservableCollection<ButtonTemplate[]> GameButtons { get; private set; }
        public string CorrectCount
        {
            get => $"{_correct}/{_totalCorrect}";
            set
            {
                _correct = int.Parse(value);
                OnPropertyChanged(nameof(CorrectCount));
            }
        }
        public string IncorrectCount
        {
            get => _incorrect.ToString();
            set
            {
                _incorrect = int.Parse(value);
                OnPropertyChanged(nameof(IncorrectCount));
            }
        }

        public GameViewModel(Router router, Difficulty difficulty)
        {
            _router = router;
            _gameBoard = new GameBoard(difficulty);
            _sudokuElements = _gameBoard.GetGameBoard();
            _selectedNumber = 0;
            _totalCorrect = TOTAL_GAME_POINTS - (int)difficulty;
            IncorrectCount = TOTAL_INCORRECT_POINTS.ToString();
            SelectNumberByKeyCommand = new RelayCommand<string>(SelectNumberByKey);

            CommandsInit();
            LoadContent();
        }

        private void CommandsInit()
        {
            _pauseCommands = new Dictionary<string, Action>();
            _pauseCommands.Add("Resume", Resume);
            _pauseCommands.Add("Switch to training", SwitchToTraining);
            _pauseCommands.Add("Back to menu", BackToMenu);
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

        private void SelectNumberByKey(string number)
        {
            SelectNumber(PivotButtons[int.Parse(number) - 1]);
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
                CorrectCount = (_correct + 1).ToString();
                button.Content = _selectedNumber.ToString();
                button.ButtonColor = button.DefaultColor;

                if (_correct == _totalCorrect)
                {
                    EndGame(true);
                }
            }
            else
            {
                IncorrectCount = (_incorrect - 1).ToString();
                button.ButtonColor = new SolidColorBrush(Colors.Red);

                if (_incorrect == 0)
                {
                    EndGame(false);
                }
            }
        }

        private void Resume()
        {
            Pause.SwitchVisibility();
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
            PivotButtons = contentHandler.CreateButtons(SelectNumber);
            GameButtons = contentHandler.CreateButtons(_sudokuElements, SetNumber);
            Pause = contentHandler.CreatePauseTrigger();
            PauseButtons = contentHandler.CreateButtons(_pauseCommands);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
