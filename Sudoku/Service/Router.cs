using System.ComponentModel;
using System.Windows.Controls;
using Sudoku.View;

namespace Sudoku.Service
{
    public class Router : INotifyPropertyChanged
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                //Skontrolovat memory leaky možno to nebude treba
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

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
