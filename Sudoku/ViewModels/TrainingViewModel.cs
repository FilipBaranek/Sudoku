using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using Sudoku.Models;
using Sudoku.Models.Game;
using Sudoku.Models.GameElements;
using Sudoku.Models.Hint;
using Sudoku.Models.Pause;
using Sudoku.Service;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class TrainingViewModel
    {
        private readonly Router _router;

        private bool _toggleCandidates;
        public bool ToggleCandidates
        {
            get => _toggleCandidates;
            set
            {
                _toggleCandidates = value;
                OnPropertyChanged(nameof(ToggleCandidates));

                _game.SwitchCandidatesGameBoard();
                HintManager.ChangeCandidates(_game.ActualCandidates);
                RemoveAllCandidates();
                DrawAllCandidates();
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
                    UpdateMarkedCells();
                }
                else
                {
                    RemoveMarkedNumbers();
                }
            }
        }

        private Training _game;
        public Game Game
        {
            get => _game;
        }

        public ObservableCollection<SudokuTrainingCell> GameCells { get; private set; }
        public ObservableCollection<SudokuPivot> PivotElements { get; private set; }
        public Pause PauseManager { get; private set; }
        public HintManager HintManager { get; private set; }

        public TrainingViewModel(Router router, Difficulty difficulty)
        {
            _router = router;
            _game = new Training(difficulty);

            GameCells = SudokuGenerator.GenerateTrainingCells(PlaceNumber, HandleCandidate, _game.SudokuGameBoard);
            PivotElements = SudokuGenerator.GeneratePivotCells(SelectNumber);
            HintManager = new HintManager(_game.ActualCandidates, UpdateMarkedCells);
            PauseManager = new TrainingPause(router);
        }

        private void SelectNumber(int pivot)
        {
            _game.SelectNumber(pivot);

            if (ToggleMarkNumbers)
            {
                UpdateMarkedCells();
            }
        }

        private async void PlaceNumber(SudokuTrainingCell cell)
        {
            _game.PlaceNumber(cell);

            if (_game.IsUpdateNeeded())
            {
                SetCellToDefault(cell);
                UpdateCandidates(cell);
                HintManager.ClearPotentialHint(cell.Row, cell.Column);
            }
            else if (_game.IsWrongMove())
            {
                cell.Background = new SolidColorBrush(Colors.Red);

                await Task.Delay(2000);

                cell.Background = cell.DefaultBackground;
            }

            if (_game.Win)
            {
                GameEnd();
            }
        }

        private bool IsMarkedAsHint(SudokuTrainingCell cell)
        {
            if (HintManager.HintCells != null)
            {
                foreach (var hintCell in HintManager.HintCells)
                {
                    if (hintCell.Row == cell.Row && hintCell.Column == cell.Column)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void UpdateMarkedCells()
        {
            foreach (var cell in GameCells)
            {
                if (IsMarkedAsHint(cell))
                {
                    cell.SetHintBackground();
                }
                else if (ToggleMarkNumbers && _game.IsMarkedNumber(cell.Row, cell.Column))
                {
                    cell.SetSelectedNumberBackground();
                }
                else
                {
                    cell.SetDefaultBackground();
                }
            }
        }

        private void RemoveMarkedNumbers()
        {
            foreach (var cell in GameCells)
            {
                if (!IsMarkedAsHint(cell))
                {
                    cell.SetDefaultBackground();
                }
            }
        }

        private void HandleCandidate(SudokuTrainingCell trainingCell)
        {
            if (_game.IsCandidateCell(trainingCell.Row, trainingCell.Column))
            {
                if (!_game.HandleCandidate(trainingCell.Row, trainingCell.Column))
                {
                    DrawCandidateCell(trainingCell);
                }
                ShowAllAvailableCandidates(trainingCell);
            }
        }

        private void UpdateCandidates(SudokuTrainingCell cell)
        {
            _game.UpdateSectorCandidates(cell.Row, cell.Column);
            _game.UpdateRowAndColumn(cell.Row, cell.Column);

            if (ToggleCandidates)
            {
                DrawAllCandidates();
            }
        }

        private void ShowAllAvailableCandidates(SudokuTrainingCell cell)
        {
            cell.Content = "";

            var candidates = _game.Candidates(cell.Row, cell.Column);

            if (candidates != null)
            {
                foreach (int candidate in candidates)
                {
                    cell.Content += $"{candidate} ";
                }
            }
        }

        private void DrawAllCandidates()
        {
            foreach (SudokuTrainingCell cell in GameCells)
            {
                if (_game.IsCandidateCell(cell.Row, cell.Column))
                {
                    DrawCandidateCell(cell);
                    ShowAllAvailableCandidates(cell);
                }
            }
        }

        private void RemoveAllCandidates()
        {
            foreach (SudokuTrainingCell cell in GameCells)
            {
                if (_game.IsCandidateCell(cell.Row, cell.Column))
                {
                    RemoveCandidateCell(cell);
                }
            }
        }

        private void SetCellToDefault(SudokuTrainingCell trainingCell)
        {
            trainingCell.Content = _game.SelectedNumber.ToString();
            trainingCell.SetDefaultBackground();
            trainingCell.SetDefaultFontSize();
            trainingCell.SetDefaultForeground();
            trainingCell.SetDefaultAlignment();
        }

        private void DrawCandidateCell(SudokuTrainingCell cell)
        {
            cell.SetHintFontSize();
            cell.SetHintForeground();
            cell.SetHintAlignment();
        }

        private void RemoveCandidateCell(SudokuTrainingCell cell)
        {
            cell.SetDefaultAlignment();
            cell.SetDefaultFontSize();
            cell.SetDefaultForeground();
            cell.Content = "";
        }

        private void GameEnd()
        {
            _router.RedirectTo(new GameEndView(_router, true));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
