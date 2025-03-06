using System.ComponentModel;

namespace Sudoku.WPF.Services
{
    public class Router : INotifyPropertyChanged
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public void NavigateTo(object viewModel)
        {
            CurrentView = viewModel;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
