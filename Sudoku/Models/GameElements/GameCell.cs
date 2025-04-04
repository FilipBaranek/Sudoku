using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku.Models.GameElements
{
    public class GameCell : Cell, INotifyPropertyChanged
    {
        private Brush _background;
        public Brush Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public Brush DefaultBackground { get; private set; }
        public Thickness BorderThickness { get; private set; }
        public ICommand Command { get; private set; }
        public GameCell Parameter { get; private set; }

        public GameCell(int row, int column, string content, Thickness bordrThickness, Brush background, ICommand command) : base(row, column)
        {
            _content = content;
            _background = background;
            DefaultBackground = background;
            BorderThickness = bordrThickness;
            Command = command;
            Parameter = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
