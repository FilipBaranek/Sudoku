using System.ComponentModel;
using System.Windows.Controls;

namespace Sudoku.Service
{
    public class Router : INotifyPropertyChanged
    {
        private UserControl _lastPage;

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _lastPage = _currentView;

                if (_currentView is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public void RedirectTo(UserControl viewModel)
        {
            CurrentView = viewModel;
        }

        public void NavigateBack()
        {
            CurrentView = _lastPage;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
