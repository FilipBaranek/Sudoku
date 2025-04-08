using Sudoku.Service;
using System.Windows;
using System.ComponentModel;
using Sudoku.Views;
using System.Windows.Input;
using Sudoku.Commands;

namespace Sudoku.Models.Pause
{
    public abstract class Pause : INotifyPropertyChanged
    {
        protected readonly Router _router;

        private Visibility _visible;
        public Visibility Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand PauseTrigger { get; private set; }
        public ICommand Help { get; private set; }
        public ICommand BackToMenu { get; private set; }

        public Pause(Router router)
        {
            _router = router;

            Visible = Visibility.Hidden;

            PauseTrigger = new RelayCommand(PauseToggle);
            BackToMenu = new RelayCommand(RedirectToMenu);
            Help = new RelayCommand(RedirectToHelp);
        }

        public void RedirectToHelp()
        {
            _router.RedirectTo(new HelpView(_router));
        }

        public void RedirectToMenu()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _router.RedirectTo(new MenuView(_router));
            }
        }

        public abstract void PauseToggle();

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
