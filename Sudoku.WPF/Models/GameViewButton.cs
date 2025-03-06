using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Sudoku.WPF.Commands;

namespace Sudoku.WPF.Models
{
    public class GameViewButton : INotifyPropertyChanged
    {
        private string? _tag;
        private string? _content;
        private int _width;
        private int _height;
        private Brush _color;
        private GameViewButton _parameter;
        private ICommand _command;
        private Thickness _borderThickness;
        public string? Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }
        public Brush ButtonColor
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(ButtonColor));
            }
        }
        public GameViewButton Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value;
                OnPropertyChanged(nameof(Parameter));
            }
        }
        public Thickness BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value;
                OnPropertyChanged(nameof(BorderThickness));
            }
        }
        public string? Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public ICommand Command
        {
            get => _command;
            set
            {
                _command = value;
                OnPropertyChanged(nameof(Command));
            }
        }
        public Brush DefaultColor { get; private set; }

        public GameViewButton(int height, int width, Brush buttonColor, Thickness borderThickness, Action<GameViewButton> command, string? content = null, string? tag = null)
        {
            DefaultColor = buttonColor;
            ButtonColor = buttonColor;
            Height = height;
            Width = width;
            BorderThickness = borderThickness;
            Content = content;
            Tag = tag;
            Command = new RelayCommand<GameViewButton>(command);
            Parameter = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
