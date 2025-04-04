using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Models.Game;
using Sudoku.Models.GameElements;
using Sudoku.Models.Pause;
using Sudoku.Service;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged, IDisposable
    {
        private const int INITIAL_TIME = 300;
        private readonly Router _router;
        private DispatcherTimer _timer;

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

        public ICommand PauseTrigger { get; private set; }
        public ICommand SelectNumber {  get; private set; }
        public Pause PauseManager { get; private set; }
        public ObservableCollection<GameCell> GameCells { get; private set; }
        public ObservableCollection<SudokuPivot> PivotElements { get; private set; }

        public GameViewModel(Router router, Difficulty difficulty)
        {
            _router = router;
            _game = new RegularGame(difficulty);
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            TimerInit();

            PauseManager = new GamePause(router, _timer);
            GameCells = SudokuGenerator.GenerateCells(PlaceNumber, _game.SudokuGameBoard);
            PivotElements = SudokuGenerator.GeneratePivotCells(_game.SelectNumber);

            PauseTrigger = new RelayCommand(PauseManager.PauseToggle);
            SelectNumber = new RelayCommand<string>(SelectNumberByKey);
        }

        private void SelectNumberByKey(string number)
        {
            _game.SelectNumber(int.Parse(number));

            PivotElements[int.Parse(number) - 1].IsChecked = true;
        }

        private async void PlaceNumber(GameCell cell)
        {
            _game.PlaceNumber(cell);

            if (_game.Win)
            {
                GameEnd(true);
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
        }

        private void GameEnd(bool win)
        {
            _router.RedirectTo(new GameEndView(_router, win));
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

        public event PropertyChangedEventHandler? PropertyChanged;

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