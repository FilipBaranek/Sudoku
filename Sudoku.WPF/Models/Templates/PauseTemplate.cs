using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Sudoku.WPF.Commands;

namespace Sudoku.WPF.Models.Templates
{
    public class PauseTemplate : INotifyPropertyChanged
    {
        private Visibility _visibility;
        public Visibility Visible
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
        public ICommand ToggleVisibility { get; private set; }
        public PauseTemplate()
        {
            Visible = Visibility.Hidden;
            ToggleVisibility = new RelayCommand(SwitchVisibility);
        }

        public void SwitchVisibility()
        {
            Visible = Visible == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
