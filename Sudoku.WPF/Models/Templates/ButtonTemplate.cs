using System.ComponentModel;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace Sudoku.WPF.Models
{
    public class ButtonTemplate : INotifyPropertyChanged
    {
        private string? _content;
        private string? _tag;
        private Thickness? _borderThickness;
        private ICommand _command;
        private ButtonTemplate? _parameter;
        private Brush _buttonColor;
        public string? Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
        public string? Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }
        public Thickness? BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value;
                OnPropertyChanged(nameof(BorderThickness));
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
        public ButtonTemplate? Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value;
                OnPropertyChanged(nameof(Parameter));
            }
        }
        public Brush ButtonColor
        {
            get => _buttonColor;
            set
            {
                _buttonColor = value;
                OnPropertyChanged(nameof(ButtonColor));
            }
        }
        public Brush DefaultColor { get; private set; }

        public ButtonTemplate(Brush buttonColor, ICommand command, bool parameter, string? content = null, string? tag = null, Thickness? borderThickness = null)
        {
            Content = content;
            Tag = tag;
            BorderThickness = borderThickness;
            Command = command;
            ButtonColor = buttonColor;
            DefaultColor = buttonColor;

            if (parameter)
            {
                Parameter = this;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
