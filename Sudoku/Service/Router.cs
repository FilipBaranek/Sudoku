using System.ComponentModel;
using System.Windows.Controls;
using Sudoku.Views;

namespace Sudoku.Service
{
    public class Router : INotifyPropertyChanged
    {
        private UserControl? _lastPage;
        private UserControl? LastPage
        {
            get => _lastPage;
            set
            {
                if (CurrentView.GetType() != typeof(HelpView))
                {
                    DisposeLastView();
                }

                _lastPage = value;
            }
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                LastPage = _currentView;

                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Router()
        {
            var menu = new MenuView(this);

            _currentView = menu;
        }

        public void RedirectTo(UserControl viewModel)
        {
            CurrentView = viewModel;
        }

        public void NavigateBack()
        {
            if (LastPage != null)
            {
                CurrentView = LastPage;
            }
        }

        public void DisposeLastView()
        {
            if (LastPage != null && LastPage is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
