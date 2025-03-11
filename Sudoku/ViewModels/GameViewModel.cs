using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Service;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly Router _router;
        private const int _totalCorrect = 81;
        private const int _totalIncorrect = 3;
        private int _correct;
        private int _selectedNumber;
        private int[,] _sudokuElements;
        private GameBoard _gameBoard;
        private DispatcherTimer _timer;

        private int _incorrect;
        public int Incorrect
        {
            get => _incorrect;
            set
            {
                _incorrect = value;
                OnPropertyChanged(nameof(Incorrect));
            }
        }

        private int _timeLeft;
        public int TimeLeft
        {
            get => _timeLeft;
            set
            {
                _timeLeft = value;
                OnPropertyChanged(nameof(TimeLeft));
            }
        }

        private Visibility _pause;
        public Visibility Pause
        {
            get => _pause;
            set
            {
                _pause = value;
                OnPropertyChanged(nameof(Pause));
            }
        }

        public ICommand PauseTrigger {  get; private set; }
        public ICommand BackToMenu { get; private set; }
        public ICommand SelectByKey { get; private set; }
        public ICommand Rules { get; private set; }
        public ObservableCollection<SudokuCell> GameCells { get; private set; }
        public ObservableCollection<SudokuPivot> PivotElements { get; private set; }

        public GameViewModel(Router router, Difficulty difficulty)
        {
            _router = router;
            _gameBoard = new GameBoard(difficulty);
            _sudokuElements = _gameBoard.GetGameBoard();
            _correct = (int)difficulty;
            Incorrect = _totalIncorrect;
            Pause = Visibility.Hidden;
            PauseTrigger = new RelayCommand(PauseToggle);
            BackToMenu = new RelayCommand(RedirectToMenu);
            Rules = new RelayCommand(RedirectToRules);

            GenerateSudoku();
            TimerInit(_gameBoard.InitialTime(difficulty));
        }

        private void PlaceNumber(SudokuCell cell)
        {
            if (_selectedNumber == 0 || _sudokuElements[cell.Row, cell.Column] != 0)
            {
                return;
            }
            else if (_gameBoard.CheckPossibility(cell.Row, cell.Column, _selectedNumber, _sudokuElements))
            {
                _sudokuElements[cell.Row, cell.Column] = _selectedNumber;
                ++_correct;
                
                cell.Content = _selectedNumber.ToString();
                cell.Background = cell.DefaultBackground;

                if (_correct == _totalCorrect)
                {
                    _timer.Stop();
                    GameEnd(true);
                }
            }
            else
            {
                --Incorrect;
                cell.Background = new SolidColorBrush(Colors.Red);

                if (_incorrect == -1)
                {
                    _timer.Stop();
                    GameEnd(false);
                }
            }
        }

        private void SelectNumber(int pivot)
        {
            if (_selectedNumber != pivot)
            {
                _selectedNumber = pivot;
            }
        }

        private void PauseToggle()
        {
            if (Pause == Visibility.Visible)
            {
                Pause = Visibility.Hidden;
                _timer.Start();
            }
            else if (Pause == Visibility.Hidden)
            {
                Pause = Visibility.Visible;
                _timer.Stop();
            }
        }

        private void RedirectToRules()
        {
            //TODO
        }

        private void GameEnd(bool win)
        {
            _router.RedirectTo(new GameEndView(_router, win));
        }

        private void RedirectToMenu()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _router.RedirectTo(new MenuView(_router));
            }
        }

        private void GenerateSudoku()
        {
            GameCells = SudokuGenerator.GenerateCells(PlaceNumber, _sudokuElements);
            PivotElements = SudokuGenerator.GeneratePivotCells(SelectNumber);
        }

        private void TimerInit(int initialTime)
        {
            TimeLeft = initialTime;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            --TimeLeft;

            if (TimeLeft <= 0)
            {
                _timer.Stop();
                GameEnd(false);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
