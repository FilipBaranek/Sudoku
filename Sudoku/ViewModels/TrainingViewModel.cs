using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Models.GameLib;
using Sudoku.Models.GameElements;
using Sudoku.Models.Hint;
using Sudoku.Models.Pause;
using Sudoku.Service;
using Sudoku.Service.Config;
using Sudoku.Views;
using Sudoku.Models.Tools.Candidates;
using Sudoku.Models.Tools;

namespace Sudoku.ViewModels
{
    public class TrainingViewModel : GameViewModel
    {
        private readonly ConfigHandler _config;
        private TrainingCandidates _candidates;
        private Crosshair _crossHair;

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
                _candidates.RemoveAllCandidates(GameCells);
                _candidates.DrawAllCandidates(GameCells);
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
                    UpdateGameboard();
                }
                else
                {
                    UnmarkNumbers();
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
                _crossHair.ToggleCrosshair();
                if (!ToggleCrosshair)
                {
                    _crossHair.ClearCrossHair();
                    UpdateGameboard();
                }
                UpdateConfig();
            }
        }

        private Training _game;
        public Game Game
        {
            get => _game;
        }

        public ObservableCollection<SudokuTrainingCell> GameCells { get; private set; }
        public Pause PauseManager { get; private set; }
        public HintManager HintManager { get; private set; }
        public ICommand ClearHintsTrigger { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TrainingViewModel(Router router, Difficulty difficulty) : base(router)
        {
            _config = new ConfigHandler();            
            _game = new Training(difficulty);
            _crossHair = new Crosshair();
            _candidates = new TrainingCandidates(_game);

            GameCells = SudokuGenerator.GenerateTrainingCells(PlaceNumber, _candidates.HandleCandidate, HightlightCrosshair, _game.SudokuGameBoard);
            HintManager = new HintManager(_game.ActualCandidates, UpdateGameboard, _config);
            PauseManager = new TrainingPause(router);
            ClearHintsTrigger = new RelayCommand(ClearGameboardHints);

            LoadConfig();
        }

        public TrainingViewModel(Router router, int[,] solutionGameBoard, int[,] sudokuGameBoard, int correct) : base(router)
        {
            _config = new ConfigHandler();
            _game = new Training(solutionGameBoard, sudokuGameBoard, correct);
            _crossHair = new Crosshair();
            _candidates = new TrainingCandidates(_game);

            GameCells = SudokuGenerator.GenerateTrainingCells(PlaceNumber, _candidates.HandleCandidate, HightlightCrosshair, _game.SudokuGameBoard);
            HintManager = new HintManager(_game.ActualCandidates, UpdateGameboard, _config);
            PauseManager = new TrainingPause(router);
            ClearHintsTrigger = new RelayCommand(ClearGameboardHints);

            LoadConfig();
        }

        public override void SelectNumber(int pivot)
        {
            _game.SelectNumber(pivot);

            if (ToggleMarkNumbers)
            {
                UpdateGameboard();
            }
        }

        public override async void PlaceNumber(GameCell cell)
        {
            if (cell is SudokuTrainingCell trainingCell)
            {
                _game.PlaceNumber(trainingCell);

                if (_game.IsUpdateNeeded())
                {
                    _candidates.SetCellToDefault(trainingCell);
                    _candidates.UpdateCandidates(trainingCell);

                    if (ToggleCandidates)
                    {
                        _candidates.DrawAllCandidates(GameCells);
                    }

                    HintManager.ClearPotentialHint(trainingCell.Row, trainingCell.Column);
                    UpdateGameboard();
                }
                else if (_game.IsWrongMove())
                {
                    trainingCell.Background = trainingCell.WrongMoveBackground;

                    await Task.Delay(2000);

                    trainingCell.SetDefaultBackground();
                }

                if (_game.Win)
                {
                    GameEnd(true);
                }
            }
        }

        public void UpdateGameboard()
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
                    else if (HintManager.IsMarkedAsHint(cell.Row, cell.Column))
                    {
                        cell.SetHintBackground();
                    }
                    else if (_crossHair.IsMarkedAsCrosshair(cell))
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

        private void UnmarkNumbers()
        {
            foreach (var cell in GameCells)
            {
                if (!HintManager.IsMarkedAsHint(cell.Row, cell.Column))
                {
                    cell.SetDefaultBackground();
                }
            }
        }

        private void HightlightCrosshair(SudokuTrainingCell cell)
        {
            _crossHair.MarkCrossHairCells(cell);

            UpdateGameboard();
        }

        private void ClearGameboardHints()
        {
            HintManager.ClearGameboardHints();

            UpdateGameboard();
        }

        public override void GameEnd(bool isWin)
        {
            _router.RedirectTo(new GameEndView(_router, isWin, true));
        }

        private void LoadConfig()
        {
            bool markSelected = _config.MarkSelectedNumber;
            bool toggleCandidates = _config.AutomaticCandidates;
            bool crosshair = _config.Crosshair;

            ToggleMarkNumbers = markSelected;

            if (crosshair)
            {
                ToggleCrosshair = crosshair;
            }
            if (toggleCandidates)
            {
                ToggleCandidates = toggleCandidates;
            }
        }

        private void UpdateConfig()
        {
            _config.UpdateSettings(ToggleCandidates, ToggleMarkNumbers, ToggleCrosshair);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
