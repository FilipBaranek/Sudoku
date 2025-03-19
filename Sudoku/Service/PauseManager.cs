using Sudoku.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Sudoku.Service
{
    public class PauseManager : INotifyPropertyChanged
    {
        private readonly Router _router;
        private readonly DispatcherTimer? _timer;

        private Visibility _pause;
        public Visibility Pause
        {
            get => _pause;
            set
            {
                _pause = value;
                OnPropertyChanged(nameof(Pause));
            }
        }

        public PauseManager(Router router, DispatcherTimer? timer = null)
        {
            _router = router;
            _timer = timer;

            Pause = Visibility.Hidden;
        }

        public void PauseToggle()
        {
            if (_timer != null)
            {
                GamePauseToggle();
            }
            else
            {
                TrainingPauseToggle();
            }
        }

        private void TrainingPauseToggle()
        {
            if (Pause == Visibility.Visible)
            {
                Pause = Visibility.Hidden;
            }
            else if (Pause == Visibility.Hidden)
            {
                Pause = Visibility.Visible;
            }
        }

        private void GamePauseToggle()
        {
            if (Pause == Visibility.Visible)
            {
                Pause = Visibility.Hidden;
                _timer.Start();
            }
            else if (Pause == Visibility.Hidden)
            {
                Pause = Visibility.Visible;
                _timer.Stop();
            }
        }

        public void RedirectToRules()
        {
            //TODO
        }

        public void RedirectToMenu()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _router.RedirectTo(new MenuView(_router));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
