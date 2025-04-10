using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Sudoku.Commands;
using Sudoku.Service;
using Sudoku.Views;

namespace Sudoku.Models.Pause
{
    public class GamePause : Pause
    {
        private readonly DispatcherTimer _timer;
        private int[,] _solutionGameBoard;
        private int[,] _sudokuGameBoard;

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

        public ICommand SwitchToTraining { get; private set; }

        public GamePause(Router router, DispatcherTimer timer, int[,] solutionGameBoard, int[,] sudokuGameBoard) : base(router)
        {
            _timer = timer;
            _solutionGameBoard = solutionGameBoard;
            _sudokuGameBoard = sudokuGameBoard;

            SwitchToTraining = new RelayCommand(RedirectToTraining);
        }

        private void RedirectToTraining()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to switch to training mode?", "Switch to training", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                int correctCount = 0;

                foreach (int cell in _sudokuGameBoard)
                {
                    if (cell != 0)
                    {
                        ++correctCount;
                    }
                }

                _router.RedirectTo(new TrainingView(_router, _solutionGameBoard, _sudokuGameBoard, correctCount));
            }
        }

        public override void PauseToggle()
        {
            if (Visible == Visibility.Visible)
            {
                Visible = Visibility.Hidden;
                _timer.Start();
            }
            else if (Visible == Visibility.Hidden)
            {
                Visible = Visibility.Visible;
                _timer.Stop();
            }
        }

    }
}
