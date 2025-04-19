using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Threading;
using Sudoku.Models;
using Sudoku.Models.GameLib;
using Sudoku.Models.GameElements;
using Sudoku.Models.Pause;
using Sudoku.Service;
using Sudoku.Views;
using Sudoku.Models.Tools.Candidates;

namespace Sudoku.ViewModels
{
    public class RegularGameViewModel : GameViewModel, INotifyPropertyChanged, IDisposable
    {
        private const int INITIAL_TIME = 500;
        private DispatcherTimer _timer;
        private Candidates _candidates;

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

        private RegularGame _game;
        public Game Game
        {
            get => _game;
        }

        public Pause PauseManager { get; private set; }
        public ObservableCollection<GameCell> GameCells { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public RegularGameViewModel(Router router, Difficulty difficulty) : base(router)
        {
            _game = new RegularGame(difficulty);
            _candidates = new Candidates(_game);
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            TimerInit();

            PauseManager = new GamePause(router, _timer, _game.SolutionGameBoard, _game.SudokuGameBoard);
            GameCells = SudokuGenerator.GenerateCells(PlaceNumber, _candidates.HandleCandidate, _game.SudokuGameBoard);
        }

        public override void SelectNumber(int pivot)
        {
            _game.SelectNumber(pivot);
        }

        public override async void PlaceNumber(GameCell cell)
        {
            _game.PlaceNumber(cell);

            if (_game.IsUpdateNeeded())
            {
                _candidates.SetCellToDefault(cell);
            }
            else if (_game.IsWrongMove())
            {
                if (_game.Lose)
                {
                    GameEnd(false);
                }

                cell.Background = new SolidColorBrush(Colors.Red);

                await Task.Delay(2000);

                cell.Background = cell.DefaultBackground;
            }

            if (_game.Win)
            {
                GameEnd(true);
            }
        }

        public override void GameEnd(bool win)
        {
            _router.RedirectTo(new GameEndView(_router, win, false));
        }

        private void TimerInit()
        {
            TimeLeft = INITIAL_TIME;

            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            --TimeLeft;

            if (TimeLeft <= 0)
            {
                _timer.Stop();

                GameEnd(false);
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Tick -= TimerTick;
        }
    }
}