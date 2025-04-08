using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Models.Game;
using Sudoku.Models.GameElements;
using Sudoku.Models.Hint;
using Sudoku.Models.Pause;
using Sudoku.Service;
using Sudoku.Service.Config;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class TrainingViewModel
    {
        private readonly Router _router;
        private readonly ConfigHandler _config;
        private List<Cell> _cellsInCrossHair;

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
                UpdateConfig();
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
                UpdateConfig();
            }
        }

        private bool _toggleCrosshair;
        public bool ToggleCrosshair
        {
            get => _toggleCrosshair;
            set
            {
                _toggleCrosshair = value;
                OnPropertyChanged(nameof(ToggleCrosshair));
                if (!ToggleCrosshair)
                {
                    _cellsInCrossHair.Clear();
                    UpdateMarkedCells();
                }
                UpdateConfig();
            }
        }

        private Training _game;
        public Game Game
        {
            get => _game;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<SudokuTrainingCell> GameCells { get; private set; }
        public ObservableCollection<SudokuPivot> PivotElements { get; private set; }
        public Pause PauseManager { get; private set; }
        public HintManager HintManager { get; private set; }
        public ICommand SelectNumberByKeyTrigger { get; private set; }
        public ICommand ClearHintsTrigger { get; private set; }

        public TrainingViewModel(Router router, Difficulty difficulty)
        {
            _router = router;
            _config = new ConfigHandler();            
            _game = new Training(difficulty);
            _cellsInCrossHair = new List<Cell>();

            GameCells = SudokuGenerator.GenerateTrainingCells(PlaceNumber, HandleCandidate, MarkCrossHairCells, _game.SudokuGameBoard);
            PivotElements = SudokuGenerator.GeneratePivotCells(SelectNumber);
            HintManager = new HintManager(_game.ActualCandidates, UpdateMarkedCells, _config);
            PauseManager = new TrainingPause(router);
            SelectNumberByKeyTrigger = new RelayCommand<string>(SelectNumberByKey);
            ClearHintsTrigger = new RelayCommand(ClearGameboardHints);

            LoadConfig();
        }

        private void LoadConfig()
        {
            bool markSelected = _config.MarkSelectedNumber;
            bool toggleCandidates = _config.AutomaticCandidates;
            bool crosshair = _config.Crosshair;
            
            ToggleCrosshair = crosshair;
            ToggleMarkNumbers = markSelected;
            _toggleCandidates = toggleCandidates;

            if (_toggleCandidates)
            {
                _game.SwitchCandidatesGameBoard();
                HintManager.ChangeCandidates(_game.ActualCandidates);
                RemoveAllCandidates();
                DrawAllCandidates();
                UpdateConfig();
            }
        }

        private void UpdateConfig()
        {
            _config.UpdateSettings(ToggleCandidates, ToggleMarkNumbers, ToggleCrosshair);
        }

        private void SelectNumber(int pivot)
        {
            _game.SelectNumber(pivot);

            if (ToggleMarkNumbers)
            {
                UpdateMarkedCells();
            }
        }

        private void SelectNumberByKey(string number)
        {
            SelectNumber(int.Parse(number));

            PivotElements[int.Parse(number) - 1].IsChecked = true;
        }

        private async void PlaceNumber(SudokuTrainingCell cell)
        {
            _game.PlaceNumber(cell);

            if (_game.IsUpdateNeeded())
            {
                SetCellToDefault(cell);
                UpdateCandidates(cell);
                HintManager.ClearPotentialHint(cell.Row, cell.Column);
                UpdateMarkedCells();
            }
            else if (_game.IsWrongMove())
            {
                cell.Background = cell.WrongMoveBackground;

                await Task.Delay(2000);

                cell.SetDefaultBackground();
            }

            if (_game.Win)
            {
                GameEnd();
            }
        }

        private void MarkCrossHairCells(SudokuTrainingCell cell)
        {
            if (ToggleCrosshair)
            {
                _cellsInCrossHair.Clear();

                int blockRowStart = (cell.Row / 3) * 3;
                int blockColumnStart = (cell.Column / 3) * 3;

                for (int i = 0; i < 9; ++i)
                {
                    _cellsInCrossHair.Add(new Cell(cell.Row, i));
                    _cellsInCrossHair.Add(new Cell(i, cell.Column));
                }

                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        _cellsInCrossHair.Add(new Cell(blockRowStart + i, blockColumnStart + j));
                    }
                }

                UpdateMarkedCells();
            }
        }

        private bool IsMarkedAsCrosshair(SudokuTrainingCell cell)
        {
            foreach (var crosshairCell in _cellsInCrossHair)
            {
                if (cell.Row == crosshairCell.Row && cell.Column == crosshairCell.Column)
                {
                    return true;
                }
            }

            return false;
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
                if (cell.Background != cell.WrongMoveBackground)
                {
                    if (ToggleMarkNumbers && _game.IsMarkedNumber(cell.Row, cell.Column))
                    {
                        if (_game.IsFullyFilled(int.Parse(cell.Content)))
                        {
                            cell.SetSelectedFilledNumberBackgorund();
                        }
                        else
                        {
                            cell.SetSelectedNumberBackground();
                        }
                    }
                    else if (IsMarkedAsHint(cell))
                    {
                        cell.SetHintBackground();
                    }
                    else if (IsMarkedAsCrosshair(cell))
                    {
                        cell.SetCrosshairBackground();
                    }
                    else
                    {
                        cell.SetDefaultBackground();
                    }
                }
            }
        }

        private void ClearGameboardHints()
        {
            HintManager.ClearGameboardHints();

            UpdateMarkedCells();
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
            _router.RedirectTo(new GameEndView(_router, true, true));
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
