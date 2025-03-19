using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Service;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class TrainingViewModel : INotifyPropertyChanged
    {
        private readonly Router _router;
        private const int TOTAL_CORRECT = 81;
        private int _selectedNumber;
        private int _correct;
        private int[,] _solutionGameBoard;
        private int[,] _sudokuGameBoard;
        private List<int>[,] _trainingElements;
        private GameBoard _gameBoard;

        private bool _toggleCandidates;
        public bool ToggleCandidates
        {
            get => _toggleCandidates;
            set
            {
                _toggleCandidates = value;
                OnPropertyChanged(nameof(ToggleCandidates));

                if (ToggleCandidates)
                {
                    UpdateCandidates();
                }
                else
                {
                    DisableAvailableElements();
                }
            }
        }

        private bool _toggleMarkNumbers;
        public bool ToggleMarkNumbers
        {
            get => _toggleMarkNumbers;
            set
            {
                _toggleMarkNumbers = value;
                OnPropertyChanged(nameof(ToggleMarkNumbers));

                if (ToggleMarkNumbers)
                {
                    MarkSelectedNumber();
                }
                else
                {
                    UnMarkSelectedNumber();
                }
            }
        }

        public ObservableCollection<SudokuTrainingCell> GameCells { get; private set; }
        public ObservableCollection<SudokuPivot> PivotElements { get; private set; }
        public PauseManager PauseManager { get; private set; }
        public ICommand PauseTrigger { get; private set; }
        public ICommand Rules {  get; private set; }
        public ICommand BackToMenu { get; private set; }

        public TrainingViewModel(Router router, Difficulty difficulty)
        {
            _router = router;
            _correct = (int)difficulty;
            _gameBoard = new GameBoard();
            _solutionGameBoard = _gameBoard.SolutionGameBoard();
            _sudokuGameBoard = _gameBoard.SudokuGameBoard(_solutionGameBoard, difficulty);
            _trainingElements = _gameBoard.TrainingGameBoard(_sudokuGameBoard);
            this.PauseManager = new PauseManager(router);

            PauseTrigger = new RelayCommand(this.PauseManager.PauseToggle);
            Rules = new RelayCommand(this.PauseManager.RedirectToRules);
            BackToMenu = new RelayCommand(this.PauseManager.RedirectToMenu);

            GenerateSudoku();
        }

        private bool IsCorrectMove(int row, int column)
        {
            return _solutionGameBoard[row, column] == _selectedNumber;
        }

        private async void PlaceNumber(SudokuTrainingCell cell)
        {
            if (_selectedNumber == 0 || _sudokuGameBoard[cell.Row, cell.Column] != 0)
            {
                return;
            }
            else if (IsCorrectMove(cell.Row, cell.Column))
            {
                _sudokuGameBoard[cell.Row, cell.Column] = _selectedNumber;
                ++_correct;

                UpdateAvailableElements(cell);
                if (ToggleCandidates)
                {
                    UpdateCandidates();
                }
                if (ToggleMarkNumbers)
                {
                    MarkSelectedNumber();
                }

                cell.Content = _selectedNumber.ToString();
                cell.Background = cell.DefaultBackground;
                cell.SetDefaultFontSize();
                cell.SetDefaultForeground();
                cell.SetDefaultAlignment();

                
                if (_correct == TOTAL_CORRECT)
                {
                    GameEnd();
                }
            }
            else
            {
                cell.Background = new SolidColorBrush(Colors.Red);

                await Task.Delay(2000);

                cell.Background = cell.DefaultBackground;
            }
        }

        private void SelectNumber(int pivot)
        {
            if (_selectedNumber != pivot)
            {
                _selectedNumber = pivot;

                if (ToggleMarkNumbers)
                {
                    MarkSelectedNumber();
                }
            }
        }

        private void GameEnd()
        {
            _router.RedirectTo(new GameEndView(_router, true));
        }

        private void HandleCandidate(SudokuTrainingCell cell)
        {
            if (_selectedNumber != 0)
            {
                if (_trainingElements[cell.Row, cell.Column].Contains(_selectedNumber))
                {
                    _trainingElements[cell.Row, cell.Column].Remove(_selectedNumber);

                    UpdateCandidates();     //PREROBIT
                }
                else
                {
                    _trainingElements[cell.Row, cell.Column].Add(_selectedNumber);

                    UpdateCandidates();     //PREROBIT
                }
            }
        }

        private void MarkSelectedNumber()
        {
            foreach (var cell in GameCells)
            {
                if (_selectedNumber != 0 && _sudokuGameBoard[cell.Row, cell.Column] == _selectedNumber)
                {
                    cell.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    cell.Background = cell.DefaultBackground;
                }
            }
        }

        private void UnMarkSelectedNumber()
        {
            foreach (var cell in GameCells)
            {
                cell.Background = cell.DefaultBackground;
            }
        }

        private void UpdateSectorCandidates(int row, int column)
        {
            for (int i = row; i < row + 3; ++i)
            {
                for (int j = column; j < column + 3; ++j)
                {
                    if (_trainingElements[i, j].Contains(_selectedNumber))
                    {
                        _trainingElements[i, j].Remove(_selectedNumber);
                    }
                }
            }
        }

        private void UpdateAvailableElements(SudokuTrainingCell cell)
        {
            UpdateSectorCandidates(_gameBoard.SectorIndex(cell.Row), _gameBoard.SectorIndex(cell.Column));

            for (int i = 0; i < 9; ++i)
            {
                if (_trainingElements[cell.Row, i].Contains(_selectedNumber))
                {
                    _trainingElements[cell.Row, i].Remove(_selectedNumber);
                }

                if (_trainingElements[i, cell.Column].Contains(_selectedNumber))
                {
                    _trainingElements[i, cell.Column].Remove(_selectedNumber);
                }
            }
        }

        private void UpdateCandidates()
        {
            foreach (SudokuTrainingCell cell in GameCells)
            {
                if (_sudokuGameBoard[cell.Row, cell.Column] == 0)
                {
                    cell.Content = "";

                    foreach (int availableNumber in _trainingElements[cell.Row, cell.Column])
                    {
                        cell.Content += $"{availableNumber} ";
                    }

                    cell.SetHintFontSize();
                    cell.SetHintForeground();
                    cell.SetHintAlignment();
                }
            }
        }

        private void DisableAvailableElements()
        {
            foreach (SudokuTrainingCell cell in GameCells)
            {
                if (_sudokuGameBoard[cell.Row, cell.Column] == 0)
                {
                    cell.Content = "";
                }
            }
        }

        private void GenerateSudoku()
        {
            GameCells = SudokuGenerator.GenerateTrainingCells(PlaceNumber, HandleCandidate, _sudokuGameBoard);
            PivotElements = SudokuGenerator.GeneratePivotCells(SelectNumber);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
