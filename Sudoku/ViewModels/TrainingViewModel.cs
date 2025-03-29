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
                
                if (ToggleCandidates)
                {
                    DrawAllCandidates();
                }
                else
                {
                    RemoveAllCandidates();
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
                    UpdateMarkedNumbers();
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
            HintManager = new HintManager(_game.TrainingElements, GameCells);
            PauseManager = new TrainingPause(router);
        }

        private void SelectNumber(int pivot)
        {
            _game.SelectNumber(pivot);

            if (ToggleMarkNumbers)
            {
                UpdateMarkedNumbers();
            }
        }

        private void PlaceNumber(SudokuTrainingCell cell)
        {
            _game.PlaceNumber(cell);

            if (_game.IsUpdateNeeded())
            {
                UpdateCandidates(cell);
            }

            if (_game.Win)
            {
                GameEnd();
            }
        }

        private void HandleCandidate(SudokuTrainingCell trainingCell)
        {
            if (_game.HandleCandidate(trainingCell.Row, trainingCell.Column))
            {
                ShowAllAvailableCandidates(trainingCell);
            }
            else
            {
                DrawCandidateCell(trainingCell);
                ShowAllAvailableCandidates(trainingCell);
            }
        }

        private void UpdateMarkedNumbers()
        {
            foreach (var cell in GameCells)
            {
                if (_game.IsMarkedNumber(cell.Row, cell.Column))
                {
                    cell.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    cell.Background = cell.DefaultBackground;
                }
            }
        }

        private void RemoveMarkedNumbers()
        {
            foreach (var cell in GameCells)
            {
                cell.Background = cell.DefaultBackground;
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

        private void DrawCandidateCell(SudokuTrainingCell cell)
        {
            cell.SetHintFontSize();
            cell.SetHintForeground();
            cell.SetHintAlignment();
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

        private void RemoveCandidateCell(SudokuTrainingCell cell)
        {
            cell.SetDefaultAlignment();
            cell.SetDefaultFontSize();
            cell.SetDefaultForeground();
            cell.Content = "";
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
