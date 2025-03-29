using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Sudoku.Commands;
using Sudoku.Service;

namespace Sudoku.Models.Pause
{
    public class GamePause : Pause
    {
        private readonly DispatcherTimer _timer;

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

        public ICommand Switch { get; private set; }

        public GamePause(Router router, DispatcherTimer timer) : base(router)
        {
            _timer = timer;

            Switch = new RelayCommand(SwitchToTraining);
        }

        public void SwitchToTraining()
        {
            //TODO
            //TU ISTU MATICU POSIELAT (DOLEZITE!!!)
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
