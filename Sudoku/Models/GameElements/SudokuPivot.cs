using System.ComponentModel;
using System.Windows.Input;

namespace Sudoku.Models.GameElements
{
    public class SudokuPivot : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Value { get; private set; }
        public string Content { get; private set; }
        public ICommand Command { get; private set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        public SudokuPivot(int value, string content, ICommand command)
        {
            Value = value;
            Content = content;
            Command = command;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
