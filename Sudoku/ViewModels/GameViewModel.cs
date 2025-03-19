using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private const int TOTAL_CORRECT = 81;
        private const int TOTAL_INCORRECT = 3;
        private int _correct;
        private int _selectedNumber;
        private int[,] _solutionGameBoard;
        private int[,] _sudokuGameBoard;
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

        public ICommand PauseTrigger { get; private set; }
        public ICommand BackToMenu { get; private set; }
        public ICommand Rules { get; private set; }
        public PauseManager PauseManager { get; private set; }
        public ObservableCollection<SudokuCell> GameCells { get; private set; }
        public ObservableCollection<SudokuPivot> PivotElements { get; private set; }

        public GameViewModel(Router router, Difficulty difficulty)
        {
            _router = router;
            _gameBoard = new GameBoard();
            _solutionGameBoard = _gameBoard.SolutionGameBoard();
            _sudokuGameBoard = _gameBoard.SudokuGameBoard(_solutionGameBoard, difficulty);
            _correct = (int)difficulty;
            Incorrect = TOTAL_INCORRECT;

            TimerInit(_gameBoard.InitialTime(difficulty));
            this.PauseManager = new PauseManager(router, _timer);

            PauseTrigger = new RelayCommand(this.PauseManager.PauseToggle);
            BackToMenu = new RelayCommand(this.PauseManager.RedirectToMenu);
            Rules = new RelayCommand(this.PauseManager.RedirectToRules);

            GenerateSudoku();
        }

        private bool IsCorrectMove(int row, int column)
        {
            return _solutionGameBoard[row, column] == _selectedNumber;
        }

        private async void PlaceNumber(SudokuCell cell)
        {
            if (_selectedNumber == 0 || _sudokuGameBoard[cell.Row, cell.Column] != 0)
            {
                return;
            }
            else if (IsCorrectMove(cell.Row, cell.Column))
            {
                _sudokuGameBoard[cell.Row, cell.Column] = _selectedNumber;
                ++_correct;

                cell.Content = _selectedNumber.ToString();
                cell.Background = cell.DefaultBackground;

                if (_correct == TOTAL_CORRECT)
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

                await Task.Delay(2000);

                cell.Background = cell.DefaultBackground;
            }
        }

        private void SelectNumber(int pivot)
        {
            if (_selectedNumber != pivot)
            {
                _selectedNumber = pivot;
            }
        }

        private void GameEnd(bool win)
        {
            _router.RedirectTo(new GameEndView(_router, win));
        }

        private void GenerateSudoku()
        {
            GameCells = SudokuGenerator.GenerateCells(PlaceNumber, _sudokuGameBoard);
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